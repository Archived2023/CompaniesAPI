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

namespace Companies.Tests.Controllers
{
    public class TestDemoControllerTests : IDisposable
    {
        private TestDemoController sut;
        private Mock<ICompanyRepository> mockRepo;

        public TestDemoControllerTests()
        {
            mockRepo  = new Mock<ICompanyRepository>();
            var mockUow = new Mock<IUnitOfWork>();

            mockUow.Setup(x => x.CompanyRepository).Returns(mockRepo.Object);

            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CompanyMappings>();
            }));

            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            var userManger = new UserManager<IdentityUser>(mockUserStore.Object,null, null, null, null, null, null, null, null);

            sut = new TestDemoController(mockUow.Object, mapper, userManger);
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

            var resultType = output.Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(resultType);
            Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);

            var items = resultType.Value as List<CompanyDto>;
            Assert.IsType<List<CompanyDto>>(items);

            Assert.Equal(items.Count, companies.Count);
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

        public void Dispose()
        {
            //Not used here

        }
    }
}
