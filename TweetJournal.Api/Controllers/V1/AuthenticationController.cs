using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TweetJournal.Access.Authentication;
using TweetJournal.Api.Contracts.V1;
using TweetJournal.Api.Contracts.V1.Requests;
using TweetJournal.Api.Contracts.V1.Responses;

namespace TweetJournal.Api.Controllers.V1
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationAccess _authenticationAccess;

        public AuthenticationController(IAuthenticationAccess authenticationAccess)
        {
            _authenticationAccess = authenticationAccess;
        }

        [HttpPost(ApiRoutes.Authentication.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest userRequest)
        {
            var authResponse =
                await _authenticationAccess.RegisterAsync(userRequest.EmailAddress, userRequest.Password);
            if (authResponse.Success)
                return Ok(authResponse);

            return new BadRequestObjectResult(new AuthFailedResponse
            {
                Errors = authResponse.Errors
            });
        }

        [HttpPost(ApiRoutes.Authentication.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var authResponse = await _authenticationAccess.LoginAsync(loginRequest.Username, loginRequest.Password);
            if (authResponse.Success)
                return Ok(new AuthSuccessResponse
                {
                    Token = authResponse.Token
                });

            return BadRequest(new
            {
                authResponse.Errors
            });
        }
    }
}