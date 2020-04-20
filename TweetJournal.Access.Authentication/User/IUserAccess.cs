using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TweetJournal.Access.Authentication.User
{
    public interface IUserAccess
    {
        Task<IdentityUser> CreateAsync(string email, string password);
        Task<IdentityUser> GetByEmailAsync(string email);
        Task<bool> VerifyPasswordAsync(IdentityUser identityUser, string password);

    }
}