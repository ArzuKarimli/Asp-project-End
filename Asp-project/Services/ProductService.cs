using Asp_project.Areas.Admin.ViewModels.Product;
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

        public async Task CreateAsync(Product product)
        {
           await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
      
        public async Task DeleteAsync(Product product)
        {
             _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task EditAsync(Product product, ProductEditVM editProduct)
        {
            product.CategoryId = editProduct.CategoryId;
            product.Description = editProduct.Description;
            product.Price = editProduct.Price;
            product.Quality = editProduct.Quality;
            product.Weight = editProduct.Weight;
            product.MinWeight= editProduct.MinWeight;
            product.Check= editProduct.Check;
            product.CountryOfOrigin = editProduct.CountryOfOrigin;
            product.Name = editProduct.Name;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(m => m.Category)
                                          .Include(m=>m.ProductImages)
                                          .ToListAsync();
        }
        public async Task<List<Product>> GetAllPaginationAsync(int page, int take = 4)
        {
            return await _context.Products.Include(m => m.Category).Include(m => m.ProductImages).Skip((page - 1) * take).Take(take).ToListAsync();
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

        public async Task<List<Product>> SearchProductAsync(string searchText)
        {
            return await _context.Products.Include(p => p.Category)
                                           .Where(p => p.Name.Contains(searchText) || p.Category.Name.Contains(searchText))
                                           .ToListAsync();
        }

       
    }
}
