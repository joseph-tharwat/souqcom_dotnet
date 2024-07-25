using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using souqcomApp.Models;
using admin.login;

namespace souqcomApp.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        AdminLogin temp =new AdminLogin();
        return View(temp);
    }
    
    [HttpPost]
    public ActionResult Login(AdminLogin adminInfo)
    {
        adminServices adminServ = new adminServices();
        bool status = adminServ.Login(adminInfo.UserName, adminInfo.Password);
        if(status == true)
        {
            return View("Dashboard");
        }
        adminInfo.ErrorMsg = "error username or password";
        return View("Index",adminInfo);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
