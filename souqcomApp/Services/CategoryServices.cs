

using souqcomApp.Models;
using category.modification;

class CategoryServices
{
    public SouqcomContext context {get; set;}

    public CategoryServices()
    {
        context = new SouqcomContext();
    }

    public bool Create(string Name, string Description)
    {
        bool IsExist = context.Categories.Where(a=> a.CategoryName == Name).Any();
        Category NewCategory = new Category();
        NewCategory.CategoryName = Name;
        NewCategory.CategoryDescription= Description;
        if(IsExist == true)
        {
            return false;
        }
        context.Categories.Add(NewCategory);
        context.SaveChanges(); 
        return true;
    }
    public List<Category> GetList()
    {
        return context.Categories.ToList();
    }

    public bool Edit(CategoryModel catInfo)
    {
        Category cat = new Category();
        cat.CategoryName = catInfo.Name;
        cat.CategoryDescription = catInfo.Description;
        context.Categories.Attach(cat);
        // context.Entry(cat).State = System.Data.Entity.EntityState.Modified;
        context.SaveChanges();
        return true;
    }
}