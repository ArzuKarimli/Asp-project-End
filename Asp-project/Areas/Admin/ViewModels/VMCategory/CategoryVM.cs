using System.ComponentModel.DataAnnotations;

namespace Asp_project.Areas.Admin.ViewModels.VMCategory
{

    public class CategoryVM 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This input can`t be empty"),]
        [StringLength(20, ErrorMessage = "Length must be max 20")]
        public string Name { get; set; }
    }
}
