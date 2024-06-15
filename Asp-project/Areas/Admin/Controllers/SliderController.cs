
using Asp_project.Areas.Admin.Helpers.Extentions;
using Asp_project.Areas.Admin.ViewModels.Slider;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;

namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly  ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;
        public SliderController(ISliderService sliderService, IWebHostEnvironment env)
        {
            _sliderService = sliderService;
            _env = env;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();


            List<SliderVM> model = new List<SliderVM>();

            foreach (var item in sliders)
            {
                SliderVM result = new()
                {
                    Id = item.Id,
                    Image = item.Image,

                };

                model.Add(result);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Slider slider = await _sliderService.GetByIdForSliderAsync((int)id);
            if(slider is null) return NotFound();

            return View(new SliderDetailVM { Image = slider.Image });

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "File must be only image format");
                }

                if (!item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Image", "Image size must be max 200 kb");
                }

            }
            foreach (var image in request.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                string path = Path.Combine(_env.WebRootPath, "img", fileName);
                await image.SaveFileToLocalAsync(path);
                await _sliderService.AddAsync(new Slider { Image = fileName });
               
            }
              return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Slider slider = await _sliderService.GetByIdForSliderAsync((int)id);
            if (slider is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "img", slider.Image);
            path.DeleteFileFromLocal();
            await _sliderService.DeleteSliderAsync(slider);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Slider slider = await _sliderService.GetByIdForSliderAsync((int)id);
            if (slider == null) return NotFound();
            return View(new SliderEditVM { Image = slider.Image });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (id == null) return BadRequest();

            Slider slider = await _sliderService.GetByIdForSliderAsync((int)id);
            if (slider == null) return NotFound();

            if (request.NewImage is null) return RedirectToAction(nameof(Index));

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "File must be only image format");
                request.Image = slider.Image;
                return View(request);
            }

            if (!request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200 kb");
                request.Image = slider.Image;
                return View(request);
            }

            await _sliderService.UpdateSliderImageAsync(slider,request.NewImage);

           

            return RedirectToAction(nameof(Index));
        }
    }
}
