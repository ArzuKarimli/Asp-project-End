using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp_project.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public CartController(AppDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<IActionResult> Index()
        {
            var basketJson = _accessor.HttpContext.Request.Cookies["basket"];
            if (basketJson == null)
                return View(new List<CartVM>());

            List<BasketVM> basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(basketJson);

            var cartProducts = new List<CartVM>();

            foreach (var basketProduct in basketProducts)
            {
                var product = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(m => m.Id == basketProduct.Id);

                if (product != null)
                {
                    var image = product.ProductImages.FirstOrDefault()?.Name;

                    cartProducts.Add(new CartVM
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Image = image,
                        Count = basketProduct.Count,
                        Price = product.Price, 
                        TotalPrice = basketProduct.Count * product.Price
                    });
                }
            }

            decimal totalPrice = cartProducts.Sum(m => m.TotalPrice);
            int productCount = cartProducts.Sum(m => m.Count);

            ViewBag.TotalPrice = totalPrice;
            ViewBag.ProductCount = productCount;

            return View(cartProducts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var basketJson = _accessor.HttpContext.Request.Cookies["basket"];
            if (basketJson == null)
                return NotFound();

            List<BasketVM> basketproducts = JsonConvert.DeserializeObject<List<BasketVM>>(basketJson);
            var product = basketproducts.FirstOrDefault(m => m.Id == id);
            if (product != null)
            {
                basketproducts.Remove(product);
                _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketproducts));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult IncrementCounterProduct(int? id)
        {
            if (id == null)
                return BadRequest();

            var basketJson = _accessor.HttpContext.Request.Cookies["basket"];
            if (basketJson == null)
                return NotFound();

            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(basketJson);
            var product = basket.FirstOrDefault(m => m.Id == id);
            if (product != null)
            {
                product.Count += 1;
                _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            }

            decimal totalPrice = basket.Sum(m => m.Count * m.Price);
            var count = product.Count;
            var price = product.Count * product.Price;

            return Ok(new { count, totalPrice, price });
        }

        [HttpPost]
        public IActionResult ReductionCounterProduct(int? id)
        {
            if (id == null)
                return BadRequest();

            var basketJson = _accessor.HttpContext.Request.Cookies["basket"];
            if (basketJson == null)
                return NotFound();

            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(basketJson);
            var product = basket.FirstOrDefault(m => m.Id == id);
            if (product != null)
            {
                product.Count -= 1;
                if (product.Count <= 0)
                    basket.Remove(product);

                _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            }

            decimal totalPrice = basket.Sum(m => m.Count * m.Price);
            var count = 0;
            var price = 0;
            if (product != null)
            {
                count = product.Count;
                price = (int)(product.Count * product.Price);
            }
            else
            {
                price = 0;
                count = 0;
            }

            return Ok(new { count, totalPrice, price });
        }
    }
}

