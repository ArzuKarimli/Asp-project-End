﻿using Asp_project.Models;

namespace Asp_project.ViewModel.ProductVM
{
    public class ProductDetailVM
    {
      
        public List<Category> Categories { get; set; }
        public Product Product{ get; set; }
        public List<Product> Products { get; set; }
        public List<Banner> Banners { get; set; }
        public List<Advertisment> Advertisments { get; set; }
    }
}
