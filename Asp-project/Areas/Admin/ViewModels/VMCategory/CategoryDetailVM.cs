using System.ComponentModel.DataAnnotations;

namespace Asp_project.Areas.Admin.ViewModels.VMCategory
{

    public class CategoryDetailVM
    {
        [Required(ErrorMessage = "This input can`t be empty"),]
        [StringLength(20, ErrorMessage = "Length must be max 20")]
        public string Name { get; set; }
        public int ProductCount { get; set; }
    }
}
