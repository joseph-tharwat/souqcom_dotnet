using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using souqcomApp.Models;
using admin.login;
using category.modification;

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

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(CategoryModel CategoryInfo)
    {
        CategoryServices CategoryServ = new CategoryServices();
        bool status = CategoryServ.Create(CategoryInfo.Name, CategoryInfo.Description);
        if(status == true)
        {
            //Go to the Dashboard page
            return View("Index", "category");
        }
        // adminInfo.ErrorMsg = "error ";
        //Go to Create page
        return View("Create", CategoryInfo);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
