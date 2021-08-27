using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OneSignalApp.Domain;
using OneSignalApp.Domain.Services.Interfaces;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OneSignalApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _accessor;

        [BindProperty]
        public LoginModel loginModel { get; set; }
        public bool IsLoggedIn { get; set; }

        public IndexModel(IAccountService accountService,
            IHttpContextAccessor accessor)
        {
            _accountService = accountService;
            _accessor = accessor;
            IsLoggedIn = (accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email) != null);
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _accountService.Login(loginModel.Email, loginModel.Password);
            if (!result)
            {
                return this.RedirectToPage("/Error");
            }
            return this.RedirectToPage("/AppList");
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await _accountService.Logout();
            _accessor.HttpContext.User = new ClaimsPrincipal();
            return this.RedirectToPage("/Index");
        }
    }
}
