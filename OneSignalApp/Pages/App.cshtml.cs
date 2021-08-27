using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OneSignalApp.Domain.Entities.Enums;
using OneSignalApp.Domain.Models.AppModels;
using OneSignalApp.Domain.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OneSignalApp.Pages
{
    public class AppModel : PageModel
    {
        private readonly IAppService _appService;
        private readonly IHttpContextAccessor _accessor;

        [BindProperty]
        public App App { get; set; }

        public AppModel(IAppService appService,
            IHttpContextAccessor accessor)
        {
            _appService = appService;
            _accessor = accessor;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            if (!_accessor.HttpContext.User.IsInRole(UserRole.Admin.ToString()))
            {
                return this.RedirectToPage("/AppList");
            }

            if (string.IsNullOrEmpty(id))
            {
                App = new App();
                return this.Page();
            }
            App = await _appService.GetApp(id);
            return this.Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!_accessor.HttpContext.User.IsInRole(UserRole.Admin.ToString()))
            {
                return this.RedirectToPage("/AppList");
            }

            if (String.IsNullOrEmpty(App.Id))
            {
                return await CreateApp();
            }
            return await UpdateApp();
        }

        private async Task<IActionResult> UpdateApp()
        {
            UpdateAppModel model = new UpdateAppModel() { Name = App.Name };
            bool result = await _appService.UpdateApp(App.Id, model);
            if (!result)
            {
                throw new System.Exception("Error with updating record " + App.Id);
            }
            return this.RedirectToPage("/AppList");
        }

        private async Task<IActionResult> CreateApp()
        {
            CreateAppModel model = new CreateAppModel() { Name = App.Name };
            bool result = await _appService.CreateApp(model);
            if (!result)
            {
                throw new System.Exception("Error with creating new record!");
            }
            return this.RedirectToPage("/AppList");
        }
    }
}
