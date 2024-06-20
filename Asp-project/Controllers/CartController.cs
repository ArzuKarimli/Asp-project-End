using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
          var result= await _cartService.GetCartProductsAsync();
           return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) BadRequest();
            await _cartService.DeleteAsync((int)id);
              return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public async Task<IActionResult> IncrementCounterProduct(int? id)
        {
            if (id == null) BadRequest();
            await _cartService.IncrementProductCountAsync((int)id);
            var basketProducts = await _cartService.GetCartProductsAsync();
            var product = basketProducts.FirstOrDefault(m => m.Id == id);
            if (product is null) return NotFound(); 
            
                decimal totalPrice = basketProducts.Sum(m => m.Count * m.Price);
                var count = product.Count;
                var price = product.Count * product.Price;

                return Ok(new { count, totalPrice, price });
            
        }

        [HttpPost]
        public async Task<IActionResult> ReductionCounterProduct(int? id)
        {
            if(id == null) BadRequest();
            await _cartService.ReductionCounterAsync((int)id);
            var basketProducts = await _cartService.GetCartProductsAsync();
            var product = basketProducts.FirstOrDefault(m => m.Id == id);
            if (product is null) return NotFound();

            decimal totalPrice = basketProducts.Sum(m => m.Count * m.Price);
            var count = product.Count;
            var price = product.Count * product.Price;

            return Ok(new { count, totalPrice, price });
        }
        
    }
}
