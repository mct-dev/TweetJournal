using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TweetJournal.Access.Authentication.User
{
    public class UserAccess : IUserAccess
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserAccess(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityUser> CreateAsync(string email, string password)
        {
            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email
            };
            var result = await _userManager.CreateAsync(newUser, password);
            if (result == null)
                return null;

            return await GetByEmailAsync(newUser.Email);
        }

        public async Task<IdentityUser> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> VerifyPasswordAsync(IdentityUser identityUser, string password)
        {
            return await _userManager.CheckPasswordAsync(identityUser, password);
        }
    }
}