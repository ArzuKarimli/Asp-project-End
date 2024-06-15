using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Services
{
    public class BannerService : IBannerService
    {
        private readonly AppDbContext _context;
        public BannerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Banner>> GetAllAsync()
        {
           return await _context.Banners.ToListAsync();
        }
    }
}
