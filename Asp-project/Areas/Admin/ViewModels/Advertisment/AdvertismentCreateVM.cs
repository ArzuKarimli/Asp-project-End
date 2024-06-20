using System.ComponentModel.DataAnnotations;

namespace Asp_project.Areas.Admin.ViewModels.Advertisment
{
    public class AdvertismentCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string DiscountInfo { get; set; }
    }
}
