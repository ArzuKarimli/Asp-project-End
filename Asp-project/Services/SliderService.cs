using Asp_project.Data;
using Asp_project.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using Asp_project.Areas.Admin.Helpers.Extentions;
using Asp_project.Services.Interfaces;

namespace Asp_project.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task AddAsync(Slider slider)
        {
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
        }



        public async Task DeleteSliderAsync(Slider slider)
        {
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Slider>> GetAllAsync()
        {
            return await _context.Sliders.Where(m => !m.SoftDeleted).ToListAsync();

        }

        public async Task<Slider> GetByIdForSliderAsync(int id)
        {
            return await _context.Sliders.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<SliderInfo> GetByIdForSliderInfoAsync(int id)
        {
            return await _context.SliderInfos.FirstOrDefaultAsync();
        }

        public async Task<SliderInfo> GetSliderInfoAsync()
        {
            return await _context.SliderInfos.FirstOrDefaultAsync();
        }
        public async Task UpdateSliderImageAsync(Slider slider, IFormFile newImage)
        {

            string oldPath = Path.Combine(_env.WebRootPath, "img", slider.Image);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }


            string fileName = Guid.NewGuid().ToString() + "-" + newImage.FileName;
            string newPath = Path.Combine(_env.WebRootPath, "img", fileName);

            await newImage.SaveFileToLocalAsync(newPath);

            slider.Image = fileName;


            await _context.SaveChangesAsync();
        }
    }
}
