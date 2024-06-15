using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel.ProductVM;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Controllers
{
   
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBannerService _bannerService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService,
                                ICategoryService categoryService,
                                IBannerService bannerService)
        {
            _productService = productService;
            _bannerService = bannerService;
            _categoryService = categoryService;
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

