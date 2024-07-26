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

    public ActionResult GetCategories()
    {
        return View("CategoryDashboard", new CategoryServices().GetList());
    }

    public ActionResult CreateCategory()
    {
        return View(new CategoryModel());
    }

    [HttpPost]
    public ActionResult CreateCategory(CategoryModel CategoryInfo)
    {
        CategoryServices CategoryServ = new CategoryServices();
        bool status = CategoryServ.Create(CategoryInfo.Name, CategoryInfo.Description);
        if(status == true)
        {
            //Go to the Dashboard page
            return View("CategoryDashboard", CategoryServ.GetList());
        }
        CategoryInfo.ErrorMsg = "Duplicate Category";
        //Go to Create page
        return View("CreateCategory", CategoryInfo);
    }

    public ActionResult EditCategory()
    {
        return View("EditCategory", new CategoryModel());
    }

    [HttpPost]
    public ActionResult EditCategory(CategoryModel CategoryInfo)
    {
        CategoryServices CategoryServ = new CategoryServices();
        bool status = CategoryServ.Edit(CategoryInfo);
        if(status == true)
        {
            CategoryInfo.ErrorMsg = "Successful editing.";
        }
        return View("EditCategory", CategoryInfo);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
