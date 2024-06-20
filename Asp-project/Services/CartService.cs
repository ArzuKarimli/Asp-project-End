using Asp_project.Data;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Asp_project.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        public CartService(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _accessor = contextAccessor;

        }

        public  async Task DeleteAsync(int id)
        {
            List<BasketVM> basketproducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            if (basketproducts == null) return;
            var product = basketproducts.FirstOrDefault(m => m.Id == id);
            if (product == null) return;
            basketproducts.Remove(product);
            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketproducts));
        }

        public async Task<List<CartVM>> GetCartProductsAsync()
        {
            var basketJson = _accessor.HttpContext.Request.Cookies["basket"];
            if (basketJson == null) return new List<CartVM>();
            List<BasketVM> basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);

            var cartProducts = new List<CartVM>();

            foreach (var basketProduct in basketProducts)
            {
                var product = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(m => m.Id == basketProduct.Id);

                if (product != null)
                {
                    var mainImage = product.ProductImages.FirstOrDefault(m => m.IsMain == true).Name;


                    cartProducts.Add(new CartVM
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Image = mainImage,
                        Count = basketProduct.Count,
                        Price = basketProduct.Count * basketProduct.Price,
                        TotalPrice = basketProducts.Sum(m => m.Count * m.Price)
                    });
                }
            }
            return cartProducts;
        }

        public async Task IncrementProductCountAsync(int id)
        {
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            var product = basket.FirstOrDefault(m => m.Id == id);
            product.Count -= 1;
            if (product.Count == 0)
            {
                basket.Remove(product);
            }
            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }

        public async Task ReductionCounterAsync(int id)
        {
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            var product = basket.FirstOrDefault(m => m.Id == id);
            product.Count += 1;
            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }
     

        public async  Task<(int count, decimal total)> AddProductToBasketAsync(int id)
        {
            List<BasketVM> basketProducts = null;

            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basketProducts = new List<BasketVM>();
            }

            var dbProduct = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            var existProduct = basketProducts.FirstOrDefault(m => m.Id == id);
            if (existProduct is not null)
            {
                existProduct.Count++;
            }
            else
            {
                basketProducts.Add(new BasketVM
                {
                    Id = id,
                    Count = 1,
                    Price = dbProduct.Price
                });
            }

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));

            int count = basketProducts.Sum(m => m.Count);
            decimal total = basketProducts.Sum(m => m.Count * m.Price);

            return (count, total);
        }
    }
}
