using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Companies.API.Dtos.CompaniesDtos;
using Companies.API.Dtos.EmployeesDtos;
using Companies.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Companies.Tests.Controllers
{
    public class CompanyControllerTests : IClassFixture<CompaniesControllerFixture>
    {
        private readonly CompaniesControllerFixture fixture;

        public CompanyControllerTests(CompaniesControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task GetCompany_ShouldReturnAllCompanies()
        {
            var comp = GetCompanys();
            fixture.CompanyService.Setup(x => x.GetAsync(false)).ReturnsAsync(comp);

            var output = await fixture.Controller.GetCompany();

            var okResult = Assert.IsType<OkObjectResult>(output.Result);
            var items = Assert.IsType<List<CompanyDto>>(okResult.Value);
            Assert.Equal(items.Count, comp.Count);
        }

        private List<CompanyDto> GetCompanys()
        {
            return new List<CompanyDto>
            {
                new CompanyDto
                {
                     Id = Guid.NewGuid(),
                     Name = "Test",
                     Address = "Ankeborg, Sweden",
                     Employees = new List<EmployeeDto>()
                },
                 new CompanyDto
                {
                     Id = Guid.NewGuid(),
                     Name = "Test",
                     Address = "Ankeborg, Sweden",
                     Employees = new List<EmployeeDto>()
                }
            };

        }
    }
}
