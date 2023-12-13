using Companies.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Companies.Tests.Extensions;
using Companies.API.Repositorys;
using AutoMapper;
using Companies.API.Mappings;
using Microsoft.AspNetCore.Identity;
using Companies.API.Entities;
using Companies.API.Dtos.CompaniesDtos;
using Companies.Tests.Fixtures;

namespace Companies.Tests.Controllers
{
    public class TestDemoControllerTests : IClassFixture<TestDemoControllerFixture>
    {
        private TestDemoController sut;
        private Mock<ICompanyRepository> mockRepo;
        private readonly TestDemoControllerFixture fixture;

        public TestDemoControllerTests(TestDemoControllerFixture fixture)
        {
            this.fixture = fixture;
            sut = fixture.Controller;
            mockRepo = fixture.MockRepo;
        }

        [Fact]
        public async Task GetEmployees_ShouldReturnOkResult()
        {
            sut.SetUserIsAuthenticated(true);
            var output = await sut.GetEmployee();
            var okResult = output.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetEmployee_IfNotAuthenticated_ShouldReturnBadRequest()
        {

            sut.SetUserIsAuthenticated2(false);

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

        [Fact]
        public async Task GetCompany_ShouldReturn200Ok()
        {
            var companies = GetCompanys();
            mockRepo.Setup(x => x.GetAsync(false)).ReturnsAsync(companies);

            var output = await sut.GetCompany();

            var okResult = Assert.IsType<OkObjectResult>(output.Result);
            var items = Assert.IsType<List<CompanyDto>>(okResult.Value);
            Assert.Equal(items.Count, companies.Count);
        }

        [Fact]
        public async Task GetCompany_WhenNotFound_ShouldReturnNotFound()
        {
            var nonExistingGuid = Guid.NewGuid();
            fixture.MockRepo.Setup(x => x.GetAsync(nonExistingGuid)).ReturnsAsync(() => null);

            var output = await fixture.Controller.GetCompany(nonExistingGuid);
            Assert.IsType<NotFoundResult>(output.Result);
        }

        private List<Company> GetCompanys()
        {
            return new List<Company>
            {
                new Company
                {
                     Id = Guid.NewGuid(),
                     Name = "Test",
                     Address = "Ankeborg, Sweden",
                     Employees = new List<Employee>()
                },
                 new Company
                {
                     Id = Guid.NewGuid(),
                     Name = "Test",
                     Address = "Ankeborg, Sweden",
                     Employees = new List<Employee>()
                }
            };

        }

        //public void Dispose()
        //{
        //    //Not used here

        //}
    }
}
