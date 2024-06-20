using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Services
{
    public class InformationService : IInformationService
    {
        private readonly AppDbContext _context;
        public InformationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Information>> GetAllAsync()
        {
           return await _context.Informations.ToListAsync();
        }

        public async Task<List<Information>> GetAllCounter()
        {
           return await _context.Informations.Where(m=>m.Type=="Counter").ToListAsync();
        }

        public async Task<List<Information>> GetAllFeatures()
        {
            return await _context.Informations.Where(m => m.Type == "Feature").ToListAsync();
        }
    }
}
