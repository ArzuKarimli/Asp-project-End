using Asp_project.Models;

namespace Asp_project.ViewModel
{
    public class HomeVM
    {
        public List<Information> Features { get; set; }
        public List<Information> Counters { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Product> SearchProduct { get; set; } = new List<Product>();
        public List<Banner> Banners { get; set; }
        public List<Advertisment> Advertisments { get; set;}

    }
}
