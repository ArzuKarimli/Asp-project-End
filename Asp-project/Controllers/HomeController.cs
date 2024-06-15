
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace Asp_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInformationService _informationService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IAdvertismentService _advertiserService;
        public readonly IBannerService _bannerService;
        public HomeController(IInformationService informationService,
                              ICategoryService categoryService,
                              IProductService productService, 
                              IAdvertismentService advertiserService)
                             
        {
            _informationService = informationService;
            _categoryService = categoryService;
            _productService = productService;
            _advertiserService = advertiserService;
           
        }
        public async Task<IActionResult> Index()
        {
            var features= await _informationService.GetAllFeatures();
            var counters = await _informationService.GetAllCounter();
            List<Category> categories= await _categoryService.GetAllAsync();
            List<Product> products= await _productService.GetAllWithProductImage();
            List<Advertisment> advertisments= await _advertiserService.GetAllAsync();

            HomeVM model = new()
            {
                Features = features,
                Counters = counters,
                Categories = categories,
                Products = products,
                Advertisments= advertisments,
                
            };
            return View(model);
        }
    }
}