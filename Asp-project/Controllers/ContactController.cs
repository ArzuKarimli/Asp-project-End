using Asp_project.Services;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Asp_project.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ISettingService _settingService;
        public ContactController(IContactService contactService,ISettingService settingService)
        {
            _contactService = contactService;
            _settingService = settingService;

        }
 
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ContactVM review)
        {
         
            await _contactService.CreateMessage(review);
            Dictionary<string, string> settings = await _settingService.GetAllAsync();

            ContactVM model = new()
            {
                Settings = settings
            };
            return View(model);


        }
    }
}
