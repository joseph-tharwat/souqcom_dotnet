

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace admin.login
{
    public class AdminLogin
    {
        [Required(ErrorMessage ="UserName is required")]
        //[Remote("ValidateUserName", "Account", ErrorMessage ="error user name")] //Remote Validation
        public string UserName {get; set;} = "username";

        [Required(ErrorMessage = "Password is required")]
        public string Password {get; set;} = "password";

        public string RoutedUrl { get; set; } = "";
    }   
}
