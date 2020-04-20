using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TweetJournal.Access.Authentication;
using TweetJournal.Contracts.V1;
using TweetJournal.Contracts.V1.Requests;
using TweetJournal.Contracts.V1.Responses;

namespace TweetJournal.Api.Controllers.V1
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost(ApiRoutes.Authentication.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest userRequest)
        {
            var authResponse =
                await _authenticationService.RegisterAsync(userRequest.EmailAddress, userRequest.Password);
            if (authResponse.Success)
                return Ok(authResponse);
            
            return BadRequest(new
            {
                authResponse.Errors
            });
        }

        [HttpPost(ApiRoutes.Authentication.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var authResponse = await _authenticationService.LoginAsync(loginRequest.Username, loginRequest.Password);
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