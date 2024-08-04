using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using souqcomApp.Models;
using admin.login;
using category.modification;
using Items.modification;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Authorization;


namespace souqcomApp.Controllers;

[Authorize(Roles ="Admin")]
public class AdminController : Controller
{
    private static CategoryServices CategoryServ = null;
    private static ItemsServices ItemServ = null;
    static AdminController()
    {
        if (CategoryServ == null)
        {
            CategoryServ = new CategoryServices();
            ItemServ = new ItemsServices();
        }
    }

    //login page
    public IActionResult Index()
    {
        AdminLogin temp = new AdminLogin();
        return View(temp);
    }

    [HttpPost]
    public ActionResult Login(AdminLogin adminInfo)
    {
        adminServices adminServ = new adminServices();
        bool status = adminServ.Login(adminInfo.UserName, adminInfo.Password);
        if (status == true)
        {
            return View("Dashboard");
        }
        ViewBag.Msg = "error in username or password";
        return View("Index", adminInfo);
    }

    [ServiceFilter(typeof(executionTimeFilter))]
    public ActionResult AllItemsDashboard(int CatId = -1)
    {
        ItemFilterView item = new ItemFilterView();
        ItemModel model = new ItemModel();
        model.CategoriesId = CreateDropDownList();
        item.ItemModelFilter = model;
        item.AllItems = new ItemsServices().GetList(CatId);
        
        return View("ItemsDashboard", item);
    }

    [ResponseCache(Duration = 10)]
    [HttpGet]
    public ActionResult ItemsDashboard(ItemFilterView item)
    {
        // get all items
        if (item.ItemModelFilter == null) 
        {
            //item.AllItems = new ItemsServices().GetList(-1);
            //item.AllItems.AddRange(new ItemsServices().GetListByName(""));
            //item.AllItems = item.AllItems.Distinct().ToList();
            //item.AllItems = item.AllItems.GroupBy(t => t.ItemId).Select(group=>group.First()).ToList();
            //handle all the above insed the method
            item.AllItems = new ItemsServices().GetListByNameAndCategoryId("", -1);
        }
        else
        {
            //item.AllItems = new ItemsServices().GetList(item.ItemModelFilter.CategoryId);
            //item.AllItems.AddRange(new ItemsServices().GetListByName(item.ItemModelFilter.Name));
            //item.AllItems = item.AllItems.GroupBy(t => t.ItemId).Select(group => group.First()).ToList();
            item.AllItems = new ItemsServices().GetListByNameAndCategoryId(item.ItemModelFilter.Name, item.ItemModelFilter.CategoryId);
        }
        item.ItemModelFilter.CategoriesId = CreateDropDownList();
        return View("ItemsDashboard", item);
    }


    private List<SelectListItem> CreateDropDownList()
    {
        var categoriesId = new List<SelectListItem>();

        categoriesId.Add(new SelectListItem { Value = $"{-1}", Text = "All" });
        foreach (var cat in CategoryServ.GetList())
        {
            categoriesId.Add(new SelectListItem { Value = $"{cat.CategoryId}", Text = cat.CategoryName });
        }

        return categoriesId;
    }

    public ActionResult CreateItem()
    {
        ItemModel model = new ItemModel();
        model.CategoriesId =  CreateDropDownList();
        return View(model);
    }

    [HttpPost]
    public ActionResult CreateItem(ItemModel ItemInfo)
    {
        bool status = ItemServ.Create(ItemInfo.Name, ItemInfo.Description, ItemInfo.PhotoFile, ItemInfo.Price, ItemInfo.CategoryId);
        if(status == true)
        {
            //Go to the Dashboard page
            return RedirectToAction("ItemsDashboard");
        }
        ViewBag.Msg = "Duplicate Category";
        //Go to Create page
        return View("CreateItem", ItemInfo);
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
