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
            var dbOptions = new DbContextOptionsBuilder<IdentityContext>()
                .UseSqlServer("Server=JOSEPH-THARWAT;Database=souqcom;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;

            var db = new IdentityContext(dbOptions);
            var userStore = new UserStore<MyIdentityUser>(db);

            userManager = um; 

            signInManager  = sm; 

        }
        //public AccountController()
        //{
        //    var dbOptions = new DbContextOptionsBuilder<IdentityContext>()
        //        .UseSqlServer("Server=JOSEPH-THARWAT;Database=souqcom;Trusted_Connection=True;TrustServerCertificate=True;")
        //        .Options;

        //    var db = new IdentityContext(dbOptions);
        //    var userStore = new UserStore<MyIdentityUser>(db);

        //    userManager = new UserManager<MyIdentityUser>(
        //        userStore,
        //        new OptionsWrapper<IdentityOptions>(new IdentityOptions()),
        //        new PasswordHasher<MyIdentityUser>(),
        //        new List<IUserValidator<MyIdentityUser>>(),
        //        new List<IPasswordValidator<MyIdentityUser>>(),
        //        new UpperInvariantLookupNormalizer(),
        //        new IdentityErrorDescriber(),
        //        null,
        //        null
        //    );

        //    var mockHttpContext = new DefaultHttpContext();
        //    var httpContextAccessor = new HttpContextAccessor { HttpContext = mockHttpContext };
        //    var userClaimsPrincipalFactory = new UserClaimsPrincipalFactory<MyIdentityUser>(userManager, new OptionsWrapper<IdentityOptions>(new IdentityOptions()));
        //    var optionsAccessor = new OptionsWrapper<IdentityOptions>(new IdentityOptions());
        //    var logger = new NullLogger<SignInManager<MyIdentityUser>>();

        //    signInManager = new SignInManager<MyIdentityUser>(
        //        userManager,
        //        httpContextAccessor,
        //        userClaimsPrincipalFactory,
        //        optionsAccessor,
        //        logger,
        //        null, // IAuthenticationManager
        //        null  // ISecurityStampValidator
        //    );

        //    //var mockHttpContext = new DefaultHttpContext();
        //    //var httpContextAccessor = new HttpContextAccessor { HttpContext = mockHttpContext };

        //    //signInManager = new SignInManager<MyIdentityUser>(
        //    //    userManager,
        //    //    httpContextAccessor,
        //    //    new UserClaimsPrincipalFactory<MyIdentityUser>(userManager, new OptionsWrapper<IdentityOptions>(new IdentityOptions())),
        //    //    new OptionsWrapper<IdentityOptions>(new IdentityOptions()),
        //    //    new NullLogger<SignInManager<MyIdentityUser>>(), null, null
        //    //);


        //}


        //public ActionResult Login()
        //{
        //    AdminLogin temp = new AdminLogin();
        //    return View(temp);
        //}

        [HttpGet]
        public ActionResult Login(string RUrl = "")
        {
            string url = Request.Query["ReturnUrl"];
            AdminLogin temp = new AdminLogin() { RoutedUrl = url };
            return View(temp);  
        }

        [HttpPost]
        public async Task<ActionResult> Login(AdminLogin userInfo)
        {
            var user = await userManager.FindByNameAsync(userInfo.UserName);
            if(user != null)
            {
                var signed = await signInManager.PasswordSignInAsync(user, userInfo.Password, false, false);
                if(signed.Succeeded == true )
                {
                    var roles = await userManager.GetRolesAsync(user);
                    string role = roles.FirstOrDefault();

                    if (role == "Admin")
                    {
                        return Redirect(userInfo.RoutedUrl);
                    }
                    else if (role == "Client")
                    {
                        return RedirectToAction("Index", "Home");
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


    }

}
