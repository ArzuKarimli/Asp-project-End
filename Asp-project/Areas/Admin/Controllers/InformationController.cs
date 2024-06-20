using Asp_project.Areas.Admin.ViewModels.Information;
using Asp_project.Models;
using Asp_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Areas.Admin.Controllers
{
    public class InformationController : Controller
    {
        private readonly InformationService _informationService;
        public InformationController(InformationService informationService)
        {
            _informationService = informationService;
        }
        public async Task<IActionResult> Index()
        {
            List<Information> infos= await _informationService.GetAllAsync();
            List<InformationVM> model = new();
            foreach (var item in infos)
            {
                InformationVM result = new()
                {
                    Id = item.Id,
                    Icon = item.Icon,
                    Description = item.Description,
                    Title = item.Title,
                };
                model.Add(result);
            }
            return View(model);
        }
    }
}
