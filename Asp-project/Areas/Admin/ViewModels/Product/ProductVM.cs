using Asp_project.Models;

namespace Asp_project.Areas.Admin.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int SalesCount { get; set; } = 0;
      
        public string Category { get; set; }
        public double Rating { get; set; }
        public double Weight { get; set; }
        public string CountryOfOrigin { get; set; }
        public double MinWeight { get; set; }
        public string Check { get; set; }
        public string Quality { get; set; }
    }
}
