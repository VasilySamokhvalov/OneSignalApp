using Microsoft.AspNetCore.Identity;
using OneSignalApp.Domain.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OneSignalApp.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Login(string email, string password)
        {
            if (email is null || password is null)
            {
                throw new Exception("Email and Password coundn`t be null!");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new Exception("Coundn`t find user with this email!");
            }
            var result = await _signInManager.PasswordSignInAsync(user, password, true, false);
            if (!result.Succeeded)
            {
                throw new Exception("Wrong email or password!");
            }
            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
