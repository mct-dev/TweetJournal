using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TweetJournal.Access.Authentication;
using TweetJournal.Access.Authentication.Contract;
using TweetJournal.Api.Contracts.V1.Responses;
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
            var userRegistrationRequest = Mother.UserRegistrationRequest;
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
            var userRegistrationRequest = Mother.UserRegistrationRequest;
            var failedAuthResult = Mother.FailedAuthenticationResult;
            var authFailedResponse = new AuthFailedResponse
            {
                Errors = failedAuthResult.Errors
            };

            _authenticationAccess
                .Setup(aa => aa.RegisterAsync(userRegistrationRequest.EmailAddress, userRegistrationRequest.Password))
                .ReturnsAsync(failedAuthResult);
            
            var actionResult = await _sut.Register(userRegistrationRequest);
            var actual = (BadRequestObjectResult) actionResult;
            
            Assert.AreEqual(authFailedResponse.Errors, ((AuthFailedResponse)actual.Value).Errors);
        }

        [Test]
        public async Task ShouldReturnOkWithTokenForSuccessfulLogin()
        {
            var loginRequest = Mother.LoginRequest;
            var testToken = Mother.TestJwt;
            var authResult = Mother.SuccessfulAuthResult;

            _authenticationAccess
                .Setup(aa => aa.LoginAsync(loginRequest.Username, loginRequest.Password))
                .ReturnsAsync(authResult);

            var actionResult = await _sut.Login(loginRequest);
            var actual = (OkObjectResult)actionResult;
            
            Assert.AreEqual(testToken, ((AuthSuccessResponse)actual.Value).Token);
        }
    }
}