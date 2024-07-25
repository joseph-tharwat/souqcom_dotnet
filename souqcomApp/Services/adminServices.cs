

using souqcomApp.Models;

class adminServices
{
    public SouqcomContext context {get; set;}

    public adminServices()
    {
        context = new SouqcomContext();
    }

    public bool Login(string username, string password)
    {
        return context.Admins.Where(a=> a.AdminUserName == username && a.AdminPassword == password).Any();  
    }
}