namespace TweetJournal.Contracts.V1.Requests
{
    public class UserRegistrationRequest
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
    }
}