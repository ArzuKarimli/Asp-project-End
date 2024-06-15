using Asp_project.Areas.Admin.ViewModels.SliderInfo;
using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.Services
{
    public class SliderInfoService : ISliderInfoService
    {
        private readonly AppDbContext _context;
        public SliderInfoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateInfoAsync(SliderInfoCreateVM  sliderInfo)
        {
           await _context.SliderInfos.AddAsync(new SliderInfo { Title = sliderInfo.Title,Description= sliderInfo.Description });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInfoAsync(SliderInfo info)
        {
           _context.SliderInfos.Remove(info);
            await _context.SaveChangesAsync();

        }

        public async Task EditAsync(SliderInfo existInfo, SliderInfoEditVM info)
        {
           existInfo.Title = info.Title;
            existInfo.Description = info.Description;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string title, string desc)
        {
            return await _context.SliderInfos.AnyAsync(m => m.Title == title || m.Description == desc);
        }

       
        public async Task<List<SliderInfo>> GetAll()
        {
           return await _context.SliderInfos.OrderByDescending(m => m.Id).Where(m => !m.SoftDeleted).ToListAsync();
        }

        public async Task<SliderInfo> GetByIdAsync(int id)
        {
           return await _context.SliderInfos.Where(m=>m.Id == id).FirstOrDefaultAsync();
        }
    }
}
