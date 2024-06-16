using Asp_project.Services.Interfaces;
using Asp_project.ViewModel.Account;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace Asp_project.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _accountService.Register(request);
            return RedirectToAction(nameof(SignIn));
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _accountService.Login(request);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login failed");
                return View(request);
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }

    
}
