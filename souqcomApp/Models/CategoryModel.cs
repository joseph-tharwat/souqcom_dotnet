

using System.ComponentModel.DataAnnotations;

namespace category.modification
{
    public class CategoryModel
    {
        [Required]
        public string Name {get; set;} = "";
         
        public string Description {get; set;} = ""; 
        
        public string Photo  {get; set;} = ""; 

        public IFormFile PhotoFile {get; set;} = null;
    }   
}
