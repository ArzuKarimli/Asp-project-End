using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel.Shop;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Controllers
{
   
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBannerService _bannerService;
        private readonly ICategoryService _categoryService;
        private readonly IAdvertismentService _advertiserService;

        public ShopController(IProductService productService,
                                ICategoryService categoryService,
                                IBannerService bannerService,
                                IAdvertismentService advertisment)
        {
            _productService = productService;
            _bannerService = bannerService;
            _categoryService = categoryService;
            _advertiserService = advertisment;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            List<Banner> banners = await _bannerService.GetAllAsync();
            List<Category> categories = await _categoryService.GetAllAsync();
            List<Product> products = await _productService.GetAllAsync();

            ShopVM model = new()
            {
                Banners= banners,
                Categories= categories,
                Products= products
            };
            ViewBag.pageCount = await GetPageCountAsync(4);
            ViewBag.currentPage = page;
            return View(model);
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            int count = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)count / take);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product is null) return NotFound();
            List<Banner> banners = await _bannerService.GetAllAsync();
            ViewBag.ProductCount = ( await _productService.GetProductsByCategoryIdAsync(product.CategoryId)).Count;
            List<Category> categories= await _categoryService.GetAllAsync();
            List<Product> products= await _productService.GetAllAsync();
            ProductDetailVM productDetail = new()
            {
                Product = product,
                Banners = banners,
                Categories = categories,
                Products = products
            };
            return View(productDetail);

        }


    }
}

