using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
      
        public async Task<List<Category>> GetAllAsync()
        {
                return await _context.Categories.Include(m => m.Products).Where(m => !m.SoftDeleted && m.Products.Count != 0).ToListAsync();
        }
        
    }
}
