using System.ComponentModel.DataAnnotations;

namespace Asp_project.Areas.Admin.ViewModels.Slider
{
    public class SliderCreateVM
    {

        [Required]
        public List<IFormFile> Images { get; set; } 

    }
}
