using TweetJournalApi.Contracts.V1;
using Microsoft.AspNetCore.Mvc;

namespace TweetJournalApi.Controllers.V1
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost(ApiRoutes.Authentication.Login)]
        public IActionResult Login(string username, string password)
        {
            return Ok();
        }
    }
}