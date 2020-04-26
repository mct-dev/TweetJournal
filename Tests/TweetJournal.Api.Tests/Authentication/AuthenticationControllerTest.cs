using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TweetJournal.Access.Authentication;
using TweetJournal.Access.Authentication.Contract;
using TweetJournal.Api.Contracts.V1.Requests;
using TweetJournal.Api.Controllers.V1;

namespace TweetJournal.Api.Tests.Authentication
{
    [TestFixture]
    public class AuthenticationControllerTest
    {
        private Mock<IMapper> _mapper;
        private Mock<IAuthenticationAccess> _authenticationAccess;
        private AuthenticationController _sut;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>(MockBehavior.Strict);
            _authenticationAccess = new Mock<IAuthenticationAccess>();
            _sut = new AuthenticationController(_authenticationAccess.Object);
        }
        
        [Test]
        public async Task ShouldReturnOkForSuccessfulRegister()
        {
            var userRegistrationRequest = new UserRegistrationRequest
            {
                EmailAddress = $"{Guid.NewGuid()}testemail@testingdomain.com",
                Password = $"{Guid.NewGuid()}password",
                PhoneNumber = 8052422424
            };
            var successfulAuthResult = new AuthenticationResult
            {
                Success = true
            };

            _authenticationAccess
                .Setup(aa => aa.RegisterAsync(userRegistrationRequest.EmailAddress, userRegistrationRequest.Password))
                .ReturnsAsync(successfulAuthResult);
            
            var authSuccessResponse = await _sut.Register(userRegistrationRequest);
            var actual = (OkObjectResult) authSuccessResponse;
            
            Assert.AreEqual(successfulAuthResult, actual.Value);
        }
        
        [Test]
        public async Task ShouldReturnBadResponseWithErrorsForFailedRegister()
        {
            var userRegistrationRequest = new UserRegistrationRequest
            {
                EmailAddress = $"{Guid.NewGuid()}testemail@testingdomain.com",
                Password = $"{Guid.NewGuid()}password",
                PhoneNumber = 8052422424
            };
            var failedAuthResult = new AuthenticationResult
            {
                Success = false,
                Errors = new string[]
                {
                    "Test error 1.",
                    "Test error 2."
                }
            };

            _authenticationAccess
                .Setup(aa => aa.RegisterAsync(userRegistrationRequest.EmailAddress, userRegistrationRequest.Password))
                .ReturnsAsync(failedAuthResult);
            
            var authFailedResponse = await _sut.Register(userRegistrationRequest);
            var actual = (BadRequestObjectResult) authFailedResponse;
            
            Assert.AreEqual(failedAuthResult.Errors, ((object)actual.Value)["Errors"]);
        }
    }
}