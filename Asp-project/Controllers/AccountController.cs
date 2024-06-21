using Asp_project.Helpers;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel.Account;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using System.Security.AccessControl;
using MailKit.Net.Smtp;

namespace Asp_project.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _LoginManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> LoginManager, RoleManager<IdentityRole> roleManager,IAccountService accountService)
        {
            _userManager = userManager;
            _LoginManager = LoginManager;
            _roleManager = roleManager;
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
           

            AppUser user = new()
            {
                UserName = request.Username,
                Email = request.Email,
                FullName = request.FullName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                foreach (var role in Enum.GetValues(typeof(Roles)))
                {
                    if (!await _roleManager.RoleExistsAsync(nameof(Roles.SuperAdmin)))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(nameof(Roles.SuperAdmin)));
                    }
                }
                await _userManager.AddToRoleAsync(user, nameof(Roles.SuperAdmin));

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);


                string action = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token },Request.Scheme,Request.Host.ToString());
              
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("nesirkerimov7@gmail.com"));
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = "Confirmation";
                email.Body = new TextPart(TextFormat.Html) { Text =$"<a href=`{action}`>Click Here</a>" };

              
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("nesirkerimov7@gmail.com", "nesir1995");
                smtp.Send(email);
                smtp.Disconnect(true);
               


            }
            return RedirectToAction(nameof(VerifyEmail));
        }

        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, token);
            return RedirectToAction(nameof(SignIn));
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM request)
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
