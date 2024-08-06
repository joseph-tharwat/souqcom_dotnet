using admin.login;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using souqcomApp.Models;

namespace souqcomApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MyIdentityUser> userManager;
        private readonly SignInManager<MyIdentityUser> signInManager;

        public AccountController(SignInManager<MyIdentityUser> sm, UserManager<MyIdentityUser> um)
        {
            userManager = um; 
            signInManager  = sm; 
        }

        public ActionResult Login(string RUrl = "") //RUrl alwayse null, i do not know why.
        {
            string url = Request.Query["ReturnUrl"];
            AdminLogin temp = new AdminLogin() { RoutedUrl = url };
            return View(temp);
        }
        
        [HttpPost]
        [CustomExceptionFilter] //route any unhandle exception to this filter(CustomExceptionFilter)
        public async Task<ActionResult> Login(AdminLogin userInfo)
        {
            if (ModelState.IsValid == true)
            {
                var user = await userManager.FindByNameAsync(userInfo.UserName);
                if (user != null)
                {
                    var signed = await signInManager.PasswordSignInAsync(user, userInfo.Password, false, false);
                    if (signed.Succeeded == true)
                    {
                        var roles = await userManager.GetRolesAsync(user);
                        string role = roles.FirstOrDefault();

                        if (role == "Admin")
                        {
                            if (userInfo.RoutedUrl != "/Admin")
                            {
                                return Redirect(userInfo.RoutedUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");   
                            }
                        }
                        else if (role == "Client")
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                }
            }

            return View(userInfo);
        }


        public ActionResult Register()
        {
            AdminLogin temp = new AdminLogin();
            return View(temp);
        }

        [HttpPost]
        public async Task<ActionResult> Register(AdminLogin userInfo)
        {
            if(ModelState.IsValid == true)
            {
                MyIdentityUser user = new MyIdentityUser()
                {
                    UserName = userInfo.UserName,
                    Email = userInfo.UserName + "@test.com",
                    NormalizedEmail = userInfo.UserName,
                    NormalizedUserName = userInfo.UserName + userInfo.UserName
                };
                try
                {
                    var created = await userManager.CreateAsync(user, userInfo.Password);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = "Check the inputs";
                    return View(userInfo);
                }

                await userManager.AddToRoleAsync(user, "Client");
                
                //must add this Client to user table 

                return RedirectToAction("Index", "Home"); 
            }
            ViewBag.ErrorMsg = "check the inputs";
            return View(userInfo);
        }

        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }

}
