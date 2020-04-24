using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TweetJournal.Api.Tests
{
    public class MockWebContext
    {
        public Mock<HttpContext> HttpContext { get; private set; }
        public Mock<HttpRequest> HttpRequest { get; private set; }

        public MockWebContext()
        {
            HttpContext = new Mock<HttpContext>(MockBehavior.Loose);
            HttpRequest = new Mock<HttpRequest>(MockBehavior.Loose);

            HttpContext
                .SetupGet(c => c.Request)
                .Returns(HttpRequest.Object);
        }

        public static ControllerContext BasicControllerContext()
        {
            return new ControllerContext
            {
                HttpContext = new MockWebContext().HttpContext.Object
            };
        }
    }
}