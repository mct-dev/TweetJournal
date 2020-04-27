using System;
using System.Diagnostics.CodeAnalysis;

namespace TweetJournal.Access.Authentication.Domain
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}