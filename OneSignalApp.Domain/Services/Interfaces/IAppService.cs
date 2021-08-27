using OneSignalApp.Domain.Models.AppModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneSignalApp.Domain.Services.Interfaces
{
    public interface IAppService
    {
        Task<List<App>> GetApps();
        Task<App> GetApp(string id);
        Task<bool> UpdateApp(string id, UpdateAppModel model);
        Task<bool> CreateApp(CreateAppModel model);
    }
}
