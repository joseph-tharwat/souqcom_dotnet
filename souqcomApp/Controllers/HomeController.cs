using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using souqcomApp.Models;

namespace souqcomApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        SouqcomContext db = new SouqcomContext();
        List<Category> allCategories = db.Categories.ToList();
        return View(allCategories);
    }
    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
