

using souqcomApp.Models;

class CategoryServices
{
    public SouqcomContext context {get; set;}

    public CategoryServices()
    {
        context = new SouqcomContext();
    }

    public bool Create(string Name, string Description)
    {
        return context.Categories.Where(a=> a.CategoryName == Name).Any();  
    }
}