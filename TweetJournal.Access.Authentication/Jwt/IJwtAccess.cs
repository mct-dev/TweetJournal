
namespace TweetJournal.Access.Authentication.Jwt
{
    public interface IJwtAccess
    {
        string GenerateJwtToken(Domain.User user);
    }
}