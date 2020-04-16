using System.Threading.Tasks;
using TweetJournalApi.Domain;

namespace TweetJournalApi.Services
{
    public interface IdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<bool> LoginAsync(string email, string password);
    }
}