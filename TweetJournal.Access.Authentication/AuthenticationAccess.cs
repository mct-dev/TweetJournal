using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using TweetJournal.Access.Authentication.Contract;
using TweetJournal.Access.Authentication.Jwt;
using TweetJournal.Access.Authentication.User;

namespace TweetJournal.Access.Authentication
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationAccess : IAuthenticationAccess
    {
        private readonly IJwtAccess _jwtAccess;
        private readonly IUserAccess _userAccess;

        public AuthenticationAccess(IJwtAccess jwtAccess, IUserAccess userAccess)
        {
            _jwtAccess = jwtAccess;
            _userAccess = userAccess;
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var existingUser = await _userAccess.GetByEmailAsync(email);
            if (existingUser != null)
                return UserExistsResult();
            
            var createdUser = await _userAccess.CreateAsync(email, password);
            if (createdUser == null)
                return null;
            
            var user = new Domain.User
            {
                Id = Guid.NewGuid(),
                Email = email,
                Password = password
            };
            var token = _jwtAccess.GenerateJwtToken(user);
            return AuthenticationSuccessResult(token);
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var identityUser = await _userAccess.GetByEmailAsync(email);
            if (identityUser == null)
                return AuthenticationErrorResult(new[] {"User does not exist"});

            var hasValidPassword = await _userAccess.VerifyPasswordAsync(identityUser, password);
            if (!hasValidPassword)
                return AuthenticationErrorResult(new[] {"User / Password combination is incorrect."});

            var user = new Domain.User
            {
                Id = Guid.NewGuid(),
                Email = email,
                Password = password
            };
            var token = _jwtAccess.GenerateJwtToken(user);
            return AuthenticationSuccessResult(token);
        }



        private AuthenticationResult UserExistsResult()
        {
            return new AuthenticationResult
            {
                Errors = new[] {"User with this email already exists."}
            };
        }


        private AuthenticationResult AuthenticationErrorResult(IEnumerable<string> errors)
        {
            return new AuthenticationResult
            {
                Errors = errors
            };
        }

        private AuthenticationResult AuthenticationSuccessResult(string token)
        {
            return new AuthenticationResult
            {
                Success = true,
                Token = token
            };
        }
    }
}