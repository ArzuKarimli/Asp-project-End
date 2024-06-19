using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.ViewComponents
{
     public class FooterViewComponent : ViewComponent
        {
            private readonly ISettingService _settingService;
            public FooterViewComponent(ISettingService settingService)
            {
                _settingService = settingService;
            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
           Dictionary<string,string> settings = await _settingService.GetAllAsync();

            FooterVM response = new()
            {
                Settings = settings
            };
            return View(response);
        }
        }
    }
