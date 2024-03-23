namespace dotnetAPI.Models
{
 

    public partial class UserJobInfo
    {
        public int UserId { get; set; }

        public string Department {get; set;}
        public string JobTitle{get;set;}
        
        public UserJobInfo()
        {
            if(Department == null) Department="";

            if(JobTitle == null) JobTitle="";

        }
        
    }
}