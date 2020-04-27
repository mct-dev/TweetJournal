using System;
using TweetJournal.Access.Authentication.Contract;
using TweetJournal.Api.Contracts.V1.Requests;

namespace TweetJournal.Api.Tests.Authentication
{
    public static class Mother
    {
        public static UserRegistrationRequest UserRegistrationRequest => new UserRegistrationRequest
        {
            EmailAddress = $"{Guid.NewGuid()}testemail@testingdomain.com",
            Password = $"{Guid.NewGuid()}password",
            PhoneNumber = 8052422424
        };
        public static AuthenticationResult FailedAuthenticationResult => new AuthenticationResult
        {
            Success = false,
            Errors = new[]
            {
                "Test error 1.",
                "Test error 2."
            }
        };
        public static string TestJwt = $"{Guid.NewGuid()}";
        public static LoginRequest LoginRequest => new LoginRequest
        {
            Username = "testusername",
            Password = TestJwt
        };
        public static AuthenticationResult SuccessfulAuthResult => new AuthenticationResult
        {
            Success = true,
            Errors = null,
            Token = TestJwt
        };
    }
}