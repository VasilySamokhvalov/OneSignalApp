using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OneSignalApp.Domain.Entities.Enums;
using OneSignalApp.Domain.Models.AppModels;
using OneSignalApp.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneSignalApp.Pages
{
    public class AppListModel : PageModel
    {
        private readonly IAppService _appService;
        private readonly IHttpContextAccessor _accessor;

        [BindProperty]
        public List<App> AppList { get; set; }
        public bool IsAdmin { get; set; }

        public AppListModel(IAppService appService,
            IHttpContextAccessor accessor)
        {
            _appService = appService;
            _accessor = accessor;
            IsAdmin = _accessor.HttpContext.User.IsInRole(UserRole.Admin.ToString());
        }

        public async Task<IActionResult> OnGet()
        {
            if (!_accessor.HttpContext.User.IsInRole(UserRole.Admin.ToString()) && !_accessor.HttpContext.User.IsInRole(UserRole.DataEntryOperator.ToString()))
            {
                return this.RedirectToPage("/Index");
            }
            AppList = await _appService.GetApps();
            return Page();
        }
    }
}
