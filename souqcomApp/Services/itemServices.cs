

using Microsoft.EntityFrameworkCore;

using souqcomApp.Models;
using Items.modification;
using Microsoft.IdentityModel.Tokens;


public class ItemsServices
{
    public SouqcomContext context {get; set;}

    public ItemsServices()
    {
        context = new SouqcomContext();
    }

    public List<Item> GetList(int CatId)
    {
        if(CatId == -1)
        {
            //return all items
            return context.Items.Where(item => item.ItemCategoryId == item.ItemCategoryId).ToList();
        }
        return context.Items.Where(item => item.ItemCategoryId == CatId).ToList();
    }
    public List<Item> GetListByName(string ItemName)
    {
        if (ItemName.IsNullOrEmpty() == true)
        {
            //return all items
            return context.Items.Where(item => item.ItemCategoryId == item.ItemCategoryId).ToList();
        }
        return context.Items.Where(item => item.ItemName.Contains(ItemName)).ToList();
    }
    public List<Item> GetListByNameAndCategoryId(string ItemName, int CategoryId = -1)
    {
        if (ItemName.IsNullOrEmpty() == true && CategoryId == -1)
        {
            //return all items
            return context.Items.Where(item => item.ItemCategoryId == item.ItemCategoryId).ToList();
        }
        else if (CategoryId == -1)
        {
            return context.Items.Where(item =>item.ItemName.Contains(ItemName)).ToList();
        }
        else if(ItemName.IsNullOrEmpty() == true)
        {
            return context.Items.Where(item => item.ItemCategoryId == CategoryId).ToList();
        }
        return context.Items.Where(item => item.ItemCategoryId == CategoryId && item.ItemName.Contains(ItemName)).ToList();
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