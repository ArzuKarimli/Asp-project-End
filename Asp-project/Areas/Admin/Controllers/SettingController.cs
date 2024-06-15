using Asp_project.Areas.Admin.ViewModels.Setting;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService; 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           Dictionary<string,string> settings= await _settingService.GetAllAsync();
           var setting = settings.Select(m => new Settings { Key=m.Key,Value=m.Value}).ToList();
              
            return View(new SettingVM { Settings=setting});
        }

    }
}
