using System.Threading.Tasks;

namespace BDSA2017.Lecture11.App.Models
{
    public interface IAuthenticationHelper
    {
        Task<string> AcquireTokenAsync();
        Task<string> AcquireTokenSilentAsync();
    }
}