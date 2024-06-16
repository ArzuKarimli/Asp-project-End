namespace Asp_project.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int SalesCount { get; set; } = 0;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public double Rating { get; set; }
        public double Weight { get; set; }
        public string CountryOfOrigin { get; set; }
        public double MinWeight { get; set; }
        public string Check { get; set; }
        public string Quality { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }

    }
}
