using Asp_project.Areas.Admin.Helpers.Extentions;
using Asp_project.Areas.Admin.ViewModels.Advertisment;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdvertismentController : Controller
    {
        private readonly IAdvertismentService _advertismentService;
        private readonly IWebHostEnvironment _env;
        public AdvertismentController(IAdvertismentService advertismentService, IWebHostEnvironment env)
        {
            _advertismentService = advertismentService;
            _env = env;

        }
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
           List<Advertisment> adverdisments= await _advertismentService.GetAllAsync();
           List<AdvertismentVM> model=new();
            foreach (var item in adverdisments)
            {
                AdvertismentVM result = new()
                {  
                    Id=item.Id,
                    Image = item.Image,
                    DiscountInfo = item.DiscountInfo,
                    Title = item.Title
                };
                model.Add(result);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CheckedAds(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            await _advertismentService.SetImageMain((int)id);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if(id is null) return BadRequest();
            Advertisment adv= await _advertismentService.GetByIdAsync((int)id);
            if (adv is null) return NotFound();
            return View(new AdvertismentDetailVM { DiscountInfo=adv.DiscountInfo,Title=adv.Title,Image=adv.Image});

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            Advertisment adv = await _advertismentService.GetByIdAsync((int)id);
            if (adv is null) return NotFound();
            await _advertismentService.DeleteAsync(adv);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> Create(AdvertismentCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;
            string path = Path.Combine(_env.WebRootPath, "img", fileName);

            request.Image.SaveFileToLocalAsync(path);
            Advertisment advertisment = new()
            {
                Title = request.Title,
                DiscountInfo = request.DiscountInfo,
                Image = fileName
            };

            await _advertismentService.CreateAsync(advertisment);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Advertisment adv = await _advertismentService.GetByIdAsync((int)id);
            if (adv is null) return NotFound();
            AdvertismentEditVM model = new()
            {
                Id = adv.Id,
                DiscountInfo = adv.DiscountInfo,
                Title = adv.Title,
                Image = adv.Image,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id,AdvertismentEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            Advertisment adv = await _advertismentService.GetByIdAsync((int)id);
            if (adv is null) return BadRequest();

           
            
                string oldPath = Path.Combine(_env.WebRootPath, "img", adv.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.NewImage.FileName);
                string newPath = Path.Combine(_env.WebRootPath, "img", fileName);
                await request.NewImage.SaveFileToLocalAsync(newPath);

                adv.Image = fileName;
          

            adv.Title = request.Title;
            adv.DiscountInfo = request.DiscountInfo;

            await _advertismentService.EditAsync(adv);
            return RedirectToAction(nameof(Index));
        }

    }
}
