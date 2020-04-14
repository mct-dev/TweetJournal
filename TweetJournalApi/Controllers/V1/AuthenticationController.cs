using Microsoft.AspNetCore.Mvc;
using TweetJournal.Contracts.V1;

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