
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

using souqcomApp.Models;

namespace Items.modification
{
    public class ItemModel
    {
        [Required]
        public string Name {get; set;} = "";
         
        public string Description {get; set;} = ""; 
        
        public string Photo  {get; set;} = ""; 

        public IFormFile PhotoFile {get; set;} = null;

        public int CategoryId {get; set;}

        public int Price {get; set;}

        public List<SelectListItem> CategoriesId;
    }


    public class ItemFilterView
    {
        public List<Item> AllItems { get; set; }
        public ItemModel ItemModelFilter { get; set; }
    }


    public interface Itemp
    {
        public string GetString();
        public string SetString(string str1);
        public int GetNum();
    }

     public class TempInject: Itemp
    {
        public int num { get; set; } = 0;
        public string str { get; set; } = null;

        public TempInject()
        {
            num++;
            str = "test";
        }
        
        public string GetString()
        {
            return str;
        }
        public string SetString(string str1)
        {
            num++;
            str = str1;
            return "";
        }

        public int GetNum()
        {
            return num;
        }
    }
}
