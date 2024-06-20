using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_project.Areas.Admin.ViewModels.Advertisment;

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

        public async Task SetImageMain(int id)
        {
            List<Advertisment> ads = await _context.advertisments.ToListAsync();
            foreach (var item in ads)
            {
                item.IsMain = item.Id == id;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Advertisment> GetByIdAsync(int id)
        {
            return await _context.advertisments.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Advertisment adv)
        {
            _context.advertisments.Remove(adv);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Advertisment adv)
        {
            await _context.advertisments.AddAsync(adv);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Advertisment adv)
        {
            _context.advertisments.Update(adv);
            await _context.SaveChangesAsync();
        }
    }
}
