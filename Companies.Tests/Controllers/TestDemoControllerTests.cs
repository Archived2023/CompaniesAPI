using Companies.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Tests.Controllers
{
    public class TestDemoControllerTests
    {
        private TestDemoController sut;

        public TestDemoControllerTests()
        {
            sut = new TestDemoController();
        }

        [Fact(Skip = "Not working yet :)")]
        public async Task GetEmployees_ShouldReturnOkResult()
        {
            var output = await sut.GetEmployee();
            var okResult = output.Result as OkResult;

            Assert.IsType<OkResult>(okResult);
        }

        [Fact]
        public async Task GetEmployee_IfNotAuthenticated_ShouldReturnBadRequest()
        {
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(false);

            var controllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            sut.ControllerContext = controllerContext;

            var output = await sut.GetEmployee();
            var resType = output.Result as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(resType);

        } 
        
        [Fact]
        public async Task GetEmployee_IfAuthenticated_ShouldReturnBadRequest()
        {
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(true);

            var controllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            sut.ControllerContext = controllerContext;

            var output = await sut.GetEmployee();
            var resType = output.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(resType);

        }

    }
}
