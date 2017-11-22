using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace BDSA2017.Lecture11.App.Models
{
    public interface IAuthenticationHelper
    {
        Task<WebAccount> SignInAsync();
        Task SignOutAsync(WebAccount account);
        Task<string> AcquireTokenSilentAsync();
        Task<WebAccount> GetAccountAsync();
    }
}