using System.ComponentModel.DataAnnotations;

namespace Asp_project.Areas.Admin.ViewModels.SliderInfo
{
    public class SliderInfoCreateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This input can`t be empty"),]
        [StringLength(40, ErrorMessage = "Length must be max 20")]
        public string Title { get; set; }
        [Required(ErrorMessage = "This input can`t be empty"),]
        [StringLength(40, ErrorMessage = "Length must be max 20")]
        public string Description { get; set; }
    }
}
