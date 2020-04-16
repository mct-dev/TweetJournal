using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TweetJournal.Contracts.V1;
using TweetJournal.Contracts.V1.Requests;
using TweetJournal.Contracts.V1.Responses;
using TweetJournalApi.Domain;
using TweetJournalApi.Services;

namespace TweetJournalApi.Controllers.V1
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityService _identityService;

        public IdentityController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest userRequest)
        {
            var authResponse = await _identityService.RegisterAsync(userRequest.EmailAddress, userRequest.Password);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(authResponse);
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var successful = await _identityService.LoginAsync(loginRequest.Username, loginRequest.Password);
            if (!successful)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}