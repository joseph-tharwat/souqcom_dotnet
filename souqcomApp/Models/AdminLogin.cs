

using System.ComponentModel.DataAnnotations;

namespace admin.login
{
    public class AdminLogin
    {
        [Required]
        public string UserName {get; set;} = "username"; 
        
        [Required]
        public string Password {get; set;} = "password";

        public string ErrorMsg {get; set;} = "";

    }   
}
