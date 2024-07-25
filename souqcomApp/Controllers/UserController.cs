using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using souqcomApp.Models;

namespace souqcomApp.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        SouqcomContext db = new SouqcomContext();
        List<User> allUsers = db.Users.ToList();
        return View(allUsers);
    }
    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
