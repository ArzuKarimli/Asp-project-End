﻿using Asp_project.Models;
using System.ComponentModel.DataAnnotations;

namespace Asp_project.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price can't be empty")]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Weight can't be empty")]
        public double Weight { get; set; }
        [Required]
        public string CountryOfOrigin { get; set; }
        public double MinWeight { get; set; }
        [Required(ErrorMessage = "Check can't be empty")]
        public string Check { get; set; }
        [Required]
        public string Quality { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
