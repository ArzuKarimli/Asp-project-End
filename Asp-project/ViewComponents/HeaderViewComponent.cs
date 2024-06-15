using Asp_project.Data;
using Asp_project.Services;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        public HeaderViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string,string> settings= await _settingService.GetAllAsync();

            HeaderVM response = new()
            {
                Settings = settings
            };
            return View(response);
        }
    }


}
