
using Microsoft.EntityFrameworkCore;

using souqcomApp.Models;
using category.modification;

class CategoryServices
{
    public SouqcomContext context {get; set;}

    public CategoryServices()
    {
        context = new SouqcomContext();
    }
    
    public Category FindCategory(string Name)
    {
        return context.Categories.Where(a=> a.CategoryName == Name).FirstOrDefault();
    }

    public bool Create(string Name, string Description, IFormFile PhotoFile)
    {
        bool IsExist = context.Categories.Where(a=> a.CategoryName == Name).Any();
        if(IsExist == true)
        {
            return false;
        }

        Category NewCategory = new Category();
        NewCategory.CategoryName = Name;
        NewCategory.CategoryDescription= Description;

        var temp = NewCategory.CategoryPhoto;
        SaveImage(ref temp, PhotoFile);
        NewCategory.CategoryPhoto = temp;

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
        Category OldCat = context.Categories.Where(cat=>cat.CategoryName == catInfo.Name).FirstOrDefault();
        if(OldCat == null)
        {
            return false;
        }
        
        if(catInfo.Description != "")
        {
            OldCat.CategoryDescription = catInfo.Description;
        }
        
        if(catInfo.PhotoFile != null)
        {
             DeleteOldImage(OldCat.CategoryPhoto);

            var temp = catInfo.Photo;
            SaveImage(ref temp, catInfo.PhotoFile);
            OldCat.CategoryPhoto = temp;
        }

        // context.Categories.Attach(cat);
        context.Entry(OldCat).State = EntityState.Modified;
        // context.Entry(cat).State = System.Data.Entity.EntityState.Modified;
        context.SaveChanges();
        return true;
    }

    public bool Delete(CategoryModel catInfo)
    {
        Category cat = context.Categories.Where(c=>c.CategoryName == catInfo.Name).FirstOrDefault();
        if(cat != null)
        {
            context.Categories.Remove(cat);
            context.SaveChanges();
            return true;
        }
        return false;
    }


    private void SaveImage(ref string CategoryPhoto, IFormFile PhotoFile)
    {
        Guid newGuid = Guid.NewGuid();
        CategoryPhoto = newGuid.ToString();

        string PathToSave = "wwwroot/CategoryPhotos/" + CategoryPhoto + ".png";
        using (var fileStream = new FileStream(PathToSave, FileMode.Create))
        {
            PhotoFile.CopyToAsync(fileStream);
        }
    }

    private void DeleteOldImage(string CategoryPhoto)
    {
        string Path = "wwwroot/CategoryPhotos/" + CategoryPhoto + ".png";
        if(File.Exists(Path))
        {
            File.Delete(Path);
        }
    }
}