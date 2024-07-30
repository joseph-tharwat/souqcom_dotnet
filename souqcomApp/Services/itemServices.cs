

using Microsoft.EntityFrameworkCore;

using souqcomApp.Models;
using Items.modification;


public class ItemsServices
{
    public SouqcomContext context {get; set;}

    public ItemsServices()
    {
        context = new SouqcomContext();
    }

    public List<Item> GetList(int CatId)
    {
        return context.Items.Where(item => item.ItemCategoryId == CatId).ToList();
    }

    public bool Create(string Name, string Description, IFormFile PhotoFile, int Price, int CatId)
    {
        bool IsExist = context.Items.Where(a=> a.ItemName == Name).Any();
        if(IsExist == true)
        {
            return false;
        }

        Item NewItem = new Item();  
        NewItem.ItemName = Name;
        NewItem.ItemDescription = Description;
        NewItem.ItemCategoryId = CatId;
        NewItem.ItemPrice = Price;

        //neglect the photo now
        // var temp = NewItem.CategoryPhoto;
        // SaveImage(ref temp, PhotoFile);
        // NewItem.ItemPhoto = temp;

        context.Items.Add(NewItem);
        context.SaveChanges(); 
        return true;
    }

}