using System.Threading.Tasks;
using TweetJournal.Access.Authentication.Contract;

namespace TweetJournal.Access.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}