using dotnetAPI.Data;
using dotnetAPI.Dtos;
using dotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  DataContextDapper _dapper;

 public UserController(IConfiguration config){
    _dapper =new DataContextDapper(config);
    // Console.WriteLine(config.GetConnectionString("DefaultConnection"));
  }

  [HttpGet("testconnection")]
  public DateTime testconnection()
  {
    return _dapper.LoadDataSingle <DateTime>( "SELECT GETDATE()");
  }

    [HttpGet("GetUsers")]
    //  public IActionResult Test()
    public IEnumerable<User> GetUsers()
    {
        string sql =@"SELECT [UserId]
                        , [FirstName]
                        , [LastName]
                        , [Email]
                        , [Gender]
                        , [Active]
                    FROM  TutorialAppSchema.Users;";
        IEnumerable<User> users =_dapper.LoadData<User>(sql);
        return users;
    }

        [HttpGet("GetSingleUsers/{UserId}")]
    //  public IActionResult Test()
    public User GetSingleUsers(int UserId)
    {
        string sql =@"SELECT [UserId]
                        , [FirstName]
                        , [LastName]
                        , [Email]
                        , [Gender]
                        , [Active]
                    FROM  TutorialAppSchema.Users
                    WHERE UserID ="+ UserId.ToString();
        User user =_dapper.LoadDataSingle<User>(sql);

        return user;
    }

    [HttpPut("EditUser")]

    public IActionResult EditUser( User user)
    {
        string sql =@"
        UPDATE TutorialAppSchema.Users
        SET              [FirstName] ='"+user.FirstName+
                        "', [LastName]='"+user.LastName+
                        "', [Email] ='"+user.Email+
                        "', [Gender] ='"+user.Gender+
                        "', [Active] ='"+user.Active+ 
                        "' WHERE UserId =" + user.UserId;
        if (_dapper.ExecuteSql(sql))
        {
        return Ok();
            
        }  

        throw new Exception("Faild to upload the user");              
    }

    [HttpPost]

    public IActionResult AddUser(UserDto user)
    {
                string sql =@"
        INSERT INTO TutorialAppSchema.Users(
                         [FirstName]
                        , [LastName]
                        , [Email]
                        , [Gender]
                        , [Active]
        )
        VALUES (" +" '"+user.FirstName+
                        "','"+user.LastName+
                        "', '"+user.Email+
                        "', '"+user.Gender+
                        "', '"+user.Active+ 
                        "')";
              if (_dapper.ExecuteSql(sql))
        {
        return Ok();
            
        }  

        throw new Exception("Faild to add the user");   
    }

    [HttpDelete("DeleteUser/{UserID}")]
    public IActionResult DeleteUser(int UserID)
    {
        string sql =@"DELETE FROM TutorialAppSchema.Users
                        WHERE UserID =" +UserID.ToString();

        if(_dapper.ExecuteSql(sql) ){
            return Ok();
        }

        throw new Exception("Faild to delete");
    }
}
