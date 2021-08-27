using System.Threading.Tasks;

namespace OneSignalApp.Domain.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Login(string email, string password);
        Task Logout();
    }
}
