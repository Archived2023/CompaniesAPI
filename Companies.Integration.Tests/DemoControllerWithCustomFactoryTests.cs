using Companies.API;
using Companies.API.Data;
using Companies.API.Dtos.CompaniesDtos;
using System.Net.Http;

namespace Companies.Integration.Tests
{
    public class DemoControllerWithCustomFactoryTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient httpClient;
        private readonly APIContext context;

        public DemoControllerWithCustomFactoryTests(CustomWebApplicationFactory<Program> applicationFactory)
        {
            applicationFactory.ClientOptions.BaseAddress = new Uri("https://localhost:7157/api/demo/");
            httpClient = applicationFactory.CreateClient();
            context = applicationFactory.Context;
        }

        [Fact]
        public async Task Get_ShouldReturn_FromInMemoryDatabase()
        {
            var dto = await httpClient.GetFromJsonAsync<CompanyDto>("getone");
            Assert.Equal("TestCompanyName", dto.Name);

        }

        [Fact]
        public async Task GetAll_ShouldReturnAll()
        {
            var dtos = await httpClient.GetFromJsonAsync<IEnumerable<CompanyDto>>("getall");
            Assert.Equal(context.Companies.Count(), dtos.Count());
        }
    }
}
