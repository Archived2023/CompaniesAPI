using Companies.API.Controllers;
using Companies.API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Tests.Fixtures
{
    public class CompaniesControllerFixture 
    {
        public Mock<ICompanyService> CompanyService { get; }
        public CompaniesController Controller { get; }
        public CompaniesControllerFixture()
        {
            CompanyService = new Mock<ICompanyService>();
            var serviceManager = new Mock<IServiceManager>();
            serviceManager.Setup(x => x.CompanyService).Returns(CompanyService.Object);
            Controller = new CompaniesController(serviceManager.Object);
        }

    }
}
