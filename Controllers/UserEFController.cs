using AutoMapper;
using dotnetAPI.Data;
using dotnetAPI.Dtos;
using dotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
  DataContextEF _entityFramework;
IMapper _mapper;
 public UserEFController(IConfiguration config){
    _entityFramework =new DataContextEF(config);
    _mapper= new Mapper(new MapperConfiguration (cfg =>{
        cfg.CreateMap<UserDto, User>();
    }));
  }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {

        IEnumerable<User> users =_entityFramework.Users.ToList<User>();
        return users;
    }

    [HttpGet("GetSingleUsers/{UserId}")]
    //  public IActionResult Test()
    public User GetSingleUsers(int UserId)
    {
       
        User? user =_entityFramework.Users.Where(u =>u.UserId == UserId).FirstOrDefault<User>();

        if(user != null)
        {

        return user;
        }
        throw new Exception("faild to get the user");
    }

    [HttpPut("EditUser")]

    public IActionResult EditUser( User user)
    {
        User? userDb =_entityFramework.Users.Where(u =>u.UserId == user.UserId).FirstOrDefault<User>();
        if(userDb != null)
        {
            userDb.Active=user.Active;
            userDb.FirstName=user.FirstName;
            userDb.LastName=user.LastName;
            userDb.Email=user.Email;
            userDb.Gender=user.Gender;

            if (_entityFramework.SaveChanges()>0)
            {
                return Ok();
            }
        throw new Exception("faild to update the user");

        }
        throw new Exception("faild to get the user");


    }

    [HttpPost]

    public IActionResult AddUser(UserDto user)
    {

        User userDb = _mapper.Map<User>(user);
        _entityFramework.Add(userDb);

        if (_entityFramework.SaveChanges()>0)
        {
            return Ok();
        }
        throw new Exception("faild to add the user");

        
          
    }

    [HttpDelete("DeleteUser/{UserID}")]
    public IActionResult DeleteUser(int UserID)
    {
        
        User? userDb =_entityFramework.Users.Where(u =>u.UserId == UserID).FirstOrDefault<User>();
        if(userDb != null)
        {
            _entityFramework.Users.Remove(userDb);

            if (_entityFramework.SaveChanges()>0)
            {
                return Ok();
            }
        throw new Exception("faild to delete the user");

        }
        throw new Exception("faild to get the user");
    }
}
