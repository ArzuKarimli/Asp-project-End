using Asp_project.Areas.Admin.ViewModels.Slider;
using Asp_project.Areas.Admin.ViewModels.SliderInfo;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SliderInfoController : Controller
    {
        private readonly ISliderInfoService _sliderInfoService;
        public SliderInfoController(ISliderInfoService sliderInfoService)
        {
            _sliderInfoService = sliderInfoService; 
        }
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            List<SliderInfo> infos = await _sliderInfoService.GetAll();
            List<SliderInfoVM> result = infos.Select(m => new SliderInfoVM {Id=m.Id, Title=m.Title,Description=m.Description }).ToList();
            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> Create(SliderInfoCreateVM info)
        {
            if (!ModelState.IsValid)
            {
                return View(info);
            }

            bool existInfo = await _sliderInfoService.ExistAsync(info.Title, info.Description);
            if (existInfo)
            {
                ModelState.AddModelError(string.Empty, "This title or description already exists");
                return View(info);
            }

            await _sliderInfoService.CreateInfoAsync(info);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            SliderInfo info = await _sliderInfoService.GetByIdAsync((int)id);
            if (info is null) return NotFound();
            await _sliderInfoService.DeleteInfoAsync(info);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> Edit(int? id)

        {
            if (id == null) return BadRequest();
            SliderInfo info = await _sliderInfoService.GetByIdAsync((int)id);
            if (info is null) return NotFound();
            return View(new SliderInfoEditVM { Id=info.Id,Title=info.Title,Description=info.Description});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, SliderInfoEditVM info)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null) return BadRequest();
            SliderInfo existInfo = await _sliderInfoService.GetByIdAsync((int)id);
            if (existInfo is null) return NotFound();
          
            await _sliderInfoService.EditAsync(existInfo, info);
            return RedirectToAction(nameof(Index));

        }

    }
}
