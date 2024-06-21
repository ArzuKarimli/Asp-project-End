
using Asp_project.Models;
using Asp_project.Services;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Linq;

namespace Asp_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInformationService _informationService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IAdvertismentService _advertiserService;
        public readonly IBannerService _bannerService;
        public readonly ICartService _cartService;
        public HomeController(IInformationService informationService,
                              ICategoryService categoryService,
                              IProductService productService, 
                              IAdvertismentService advertiserService,
                              ICartService cartService)
                             
        {
            _informationService = informationService;
            _categoryService = categoryService;
            _productService = productService;
            _advertiserService = advertiserService;
            _cartService = cartService;
           
        }
        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> AddProductToBasket(int? id)
        {
            if (id == null) return BadRequest();

           List<BasketVM> result= await _cartService.AddProductToBasketAsync((int)id);
            int count = result.Sum(m => m.Count);
            decimal total = result.Sum(m => m.Count * m.Price);

            return Ok(new { count, total });

          
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchText)
        {
            var products = await _productService.SearchProductAsync(searchText);

            var model = new HomeVM
            {
                SearchProduct = products,
                Features = await _informationService.GetAllFeatures(),
                Counters = await _informationService.GetAllCounter(),
                Categories = await _categoryService.GetAllAsync(),
                Products = await _productService.GetAllWithProductImage(),
                Advertisments = await _advertiserService.GetAllAsync(),
            };

            return View(model);
        }

    }
}