using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Services
{
    public class AdvertismentService : IAdvertismentService
    {
        private readonly AppDbContext _context;
        public AdvertismentService(AppDbContext context)
        {
               _context = context;
        }

        public async Task<List<Advertisment>> GetAllAsync()
        {
            return await _context.advertisments.ToListAsync();
        }
    }
}
