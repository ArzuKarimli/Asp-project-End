using Asp_project.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Asp_project.Services.Interfaces
{
    public interface IAccountService
    {
       
        Task<SignInResult> Login(SignInVM request);
        Task Logout();
    }
}
