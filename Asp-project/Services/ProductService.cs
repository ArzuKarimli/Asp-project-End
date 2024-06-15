using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(m => m.Category)
                                          .Include(m=>m.ProductImages)
                                          .ToListAsync();
        }

        public async Task<List<Product>> GetAllWithProductImage()
        {
            return await _context.Products.Where(m => !m.SoftDeleted).Include(m => m.ProductImages).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Where(m => !m.SoftDeleted).Include(m => m.ProductImages).Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == id);

        }

       

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int id)
        {
            return await _context.Products.Where(m => m.CategoryId == id).ToListAsync();
        }
    }
}
