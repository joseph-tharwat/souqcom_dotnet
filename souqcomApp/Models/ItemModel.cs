
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
}
