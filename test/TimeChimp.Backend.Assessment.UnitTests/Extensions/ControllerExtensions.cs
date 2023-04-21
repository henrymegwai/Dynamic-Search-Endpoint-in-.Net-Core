using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stashbox.Mocking.Moq;

namespace TimeChimp.Backend.Assessment.UnitTests.Extensions
{
    public static class ControllerExtensions
    {
        public static T BypassStub<T>(this T controller, StashMoq stash) where T : Controller
        {
            stash.Mock<HttpRequest>().Setup(m => m.Query).Returns<IQueryCollection>(null);
            var httpRequest = stash.Get<HttpRequest>();

            stash.Mock<HttpContext>().Setup(m => m.Request).Returns(httpRequest);
            controller.ControllerContext = new ControllerContext { HttpContext = stash.Get<HttpContext>() };

            return controller;
        }
    }
}
