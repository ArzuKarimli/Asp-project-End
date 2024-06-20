using System.ComponentModel.DataAnnotations;

namespace Asp_project.Areas.Admin.ViewModels.Advertisment
{
    public class AdvertismentEditVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required]
        public string Title { get; set; }
        public string DiscountInfo { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
