using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Asp_project.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly AppDbContext _context;
        public CartService(IHttpContextAccessor accessor,AppDbContext context)
        {
            _accessor = accessor;
            _context = context;
        }
        public async Task<List<BasketVM>> AddProductToBasketAsync(int id)
        {
            List<BasketVM> basketProducts;

          
            if (_accessor.HttpContext.Request.Cookies.TryGetValue("basket", out string basketCookieValue))
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(basketCookieValue);
            }
            else
            {
                basketProducts = new List<BasketVM>();
            }

           
            var dbProduct = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

          
            var existingProduct = basketProducts.FirstOrDefault(m => m.Id == id);
            if (existingProduct != null)
            {
                existingProduct.Count++;
            }
            else
            {
              
                basketProducts.Add(new BasketVM
                {
                    Id = id,
                    Count = 1,
                    Price = dbProduct?.Price ?? 0
                });
            }

          
            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));

            return basketProducts;
        }
    }
}
