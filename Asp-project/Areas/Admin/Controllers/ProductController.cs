using Asp_project.Areas.Admin.Helpers.Extentions;
using Asp_project.Areas.Admin.ViewModels.Product;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        public ProductController(IProductService productService,ICategoryService categoryService,IWebHostEnvironment env)
        {
            _productService= productService;
            _categoryService= categoryService;
            _env = env;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
           List<Product> products = await _productService.GetAllAsync();

            List<ProductVM> model = new List<ProductVM>();
            foreach (var item in products)
            {
                var image = item.ProductImages.FirstOrDefault()?.Name;
                ProductVM result = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    SalesCount = item.SalesCount,
                    Weight = item.Weight,
                    MinWeight = item.MinWeight,
                    Quality = item.Quality,
                    Rating = item.Rating,
                    Category = item.Category.Name,
                   
                    CountryOfOrigin = item.CountryOfOrigin,
                    Image= image,
                };
                model.Add(result);
            }
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();
            List<ProductImageVM> images = new List<ProductImageVM>();
            foreach (var item in product.ProductImages)
            {
                images.Add(new ProductImageVM
                {
                    Image = item.Name,
                    IsMain=item.IsMain,
                });
            }

            ProductDetailVM model = new()
            {

                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                SalesCount = product.SalesCount,
                Weight = product.Weight,
                MinWeight = product.MinWeight,
                Quality = product.Quality,
                Rating = product.Rating,
                Category = product.Category.Name,
                CountryOfOrigin = product.CountryOfOrigin,
                Images=images
                
            };
            
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllSelectAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(ProductCreateVM request)
        { 
            ViewBag.Categories = await _categoryService.GetAllSelectAsync();
            if(!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileSize(500))
                {
                    ModelState.AddModelError("Images", "Image size be must max 500 kb");
                    return View();
                }
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File type must be only image");
                    return View();
                }
            }

            List<ProductImage> images = new();

            foreach (var item in request.Images)
            {
                string filaName = Guid.NewGuid().ToString() + "-" + item.FileName;
                string path = Path.Combine(_env.WebRootPath, "img", filaName);
                await item.SaveFileToLocalAsync(path);
                images.Add(new ProductImage
                {
                    Name = filaName,
                });
            }
            images.FirstOrDefault().IsMain = true;
            Product product = new()
            {
                Name = request.Name,
                Description = request.Description,
                Price = decimal.Parse(request.Price.ToString().Replace(".",",")),
                CategoryId = request.CategoryId,
                Weight= request.Weight,
                MinWeight=request.MinWeight,
                ProductImages = images,
                Check=request.Check,
                CountryOfOrigin=request.CountryOfOrigin,
        
                Quality=request.Quality,
            };

            await _productService.CreateAsync(product);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();

            foreach (var item in product.ProductImages)
            {
                string path = Path.Combine(_env.WebRootPath, "img", item.Name);
                path.DeleteFileFromLocal();
            }

            await _productService.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.categories = await _categoryService.GetAllSelectAsync();

            if (id is null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();
            ProductEditVM model = new()
            {

                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
               
                Weight = product.Weight,
                MinWeight = product.MinWeight,
                Quality = product.Quality,
               
                CategoryId = product.CategoryId,
                CountryOfOrigin = product.CountryOfOrigin,
                Images= product.ProductImages.Select(m=> new ProductImageVM
                {
                    IsMain=m.IsMain,
                    Image=m.Name
                }).ToList(),

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id,ProductEditVM editProduct)
        {
            ViewBag.categories = await _categoryService.GetAllSelectAsync();

            if (id is null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(editProduct);
            }

            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();
            foreach (var item in product.ProductImages)
            {
                string path = Path.Combine(_env.WebRootPath, "img", item.Name);
                path.DeleteFileFromLocal();
            }
            List<ProductImage> images = new();

            foreach (var item in editProduct.NewImages)
            {
                string filaName = Guid.NewGuid().ToString() + "-" + item.FileName;
                string path = Path.Combine(_env.WebRootPath, "img", filaName);
                await item.SaveFileToLocalAsync(path);
                images.Add(new ProductImage
                {
                    Name = filaName,
                });
            }

            product.ProductImages = images;
            await _productService.EditAsync(product, editProduct);
            return RedirectToAction(nameof(Index));
        }

    }
}
