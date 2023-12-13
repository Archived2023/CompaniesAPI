using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Tests.Extensions
{
    public static class ControllerExtensions
    {
        public static void SetUserIsAuthenticated(this ControllerBase controller, bool isAuthenticated)
        {
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(isAuthenticated);

            var controllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            controller.ControllerContext = controllerContext;
        } 
        
        public static void SetUserIsAuthenticated2(this ControllerBase controller, bool isAuthenticated)
        {
            var claimsPrincipal = new Mock<ClaimsPrincipal>();
            claimsPrincipal.SetupGet(x => x.Identity.IsAuthenticated).Returns(isAuthenticated);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal.Object
                }
            };
        }
    }
}
