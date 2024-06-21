using Asp_project.Helpers;
using Asp_project.Models;
using Asp_project.Services.Interfaces;
using Asp_project.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Policy;

namespace Asp_project.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

   

        public async Task<SignInResult> Login(SignInVM request)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailOrUserName) ?? await _userManager.FindByNameAsync(request.EmailOrUserName);

            if (user == null)
            {
                return SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            return result;
        }

        public async Task Logout()
        {
           await _signInManager.SignOutAsync();
        }
    }
}

