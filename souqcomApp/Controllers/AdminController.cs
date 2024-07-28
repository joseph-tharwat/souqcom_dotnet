using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using souqcomApp.Models;
using admin.login;
using category.modification;

namespace souqcomApp.Controllers;

public class AdminController : Controller
{
    private static CategoryServices CategoryServ = null;
    static AdminController()
    {
        if(CategoryServ == null)
        {
            CategoryServ = new CategoryServices();
        }
    }

    //login page
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
        ViewBag.Msg = "error in username or password";
        return View("Index",adminInfo);
    }

    public ActionResult CategoryDashboard()
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
        bool status = CategoryServ.Create(CategoryInfo.Name, CategoryInfo.Description, CategoryInfo.PhotoFile);
        if(status == true)
        {
            //Go to the Dashboard page
            return RedirectToAction("CategoryDashboard");
        }
        ViewBag.Msg = "Duplicate Category";
        //Go to Create page
        return View("CreateCategory", CategoryInfo);
    }

    public ActionResult EditCategory(string catName)
    {
        Category CatToEdit = CategoryServ.FindCategory(catName);

        CategoryModel Cat = new CategoryModel();
        Cat.Name = CatToEdit.CategoryName;
        Cat.Description = CatToEdit.CategoryDescription;
        Cat.Photo = CatToEdit.CategoryPhoto;

        return View("EditCategory", Cat);
    }

    [HttpPost]
    public ActionResult EditCategory(CategoryModel CategoryInfo)
    {
        bool status = CategoryServ.Edit(CategoryInfo);
        if(status == false)
        {
            ViewBag.Msg = "Category not Found";
            return View("EditCategory", CategoryInfo);
        }
        return RedirectToAction("CategoryDashboard", "Admin");
    }

    //This can be deleteConfirmed and the next one to be HttpPost to delete
    // public ActionResult Delete()
    // {
    //     //Go to the Dashboard page
    //     return View("DeleteConformation", CatName);
    // }

    public ActionResult DeleteCategory(string CatName)
    {
        CategoryModel CategoryInfo = new CategoryModel();
        CategoryInfo.Name = CatName;
        bool status = CategoryServ.Delete(CategoryInfo);
        if(status == true)
        {
            ViewBag.Msg = "Successful Deleting.";
        }
        else 
        {
            ViewBag.Msg = "Can not delete.";   
        }
        return RedirectToAction("CategoryDashboard");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
