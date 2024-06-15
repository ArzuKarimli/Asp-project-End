using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_project.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public SliderViewComponent(AppDbContext context)
        {
          _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Slider> sliders= await _context.Sliders.ToListAsync();
            SliderInfo sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync();

            SliderVMVC datas = new()
            {
                SliderInfo = sliderInfo,
                Sliders = sliders
            };
            return await Task.FromResult(View(datas));
        }
    }
    public class SliderVMVC
    {
        public List<Slider> Sliders { get; set; }
        public SliderInfo SliderInfo { get; set; }
    }
}
