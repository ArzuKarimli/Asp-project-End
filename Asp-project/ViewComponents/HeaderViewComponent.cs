using Asp_project.Data;
using Asp_project.Models;
using Asp_project.Services;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Asp_project.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _contextAccessor;
        public HeaderViewComponent(ISettingService settingService,IHttpContextAccessor  contextAccessor)
        {
            _settingService = settingService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> settings = await _settingService.GetAllAsync();

            List<BasketVM> basketProducts = new();

            if (_contextAccessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
            }

            HeaderVM response = new()
            {
                Settings = settings,
                BasketCount = basketProducts.Sum(m => m.Count),
                TotalPrice = basketProducts.Sum(m => m.Count * m.Price)
            };

            return View(response);
        }


    }


}
