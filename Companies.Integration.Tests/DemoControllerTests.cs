using Companies.API;
using Companies.API.Dtos.CompaniesDtos;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;

namespace Companies.Integration.Tests
{
    public class DemoControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient httpClient;

        public DemoControllerTests(WebApplicationFactory<Program> applicationFactory)
        {
            applicationFactory.ClientOptions.BaseAddress = new Uri("https://localhost:7157/api/demo/");
            httpClient = applicationFactory.CreateClient();
        }

        [Fact]
        public async Task Index_ShouldReturnOk()
        {
            var response = await httpClient.GetAsync("");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Index_ShouldReturnExpectedMessage()
        {
            var response = await httpClient.GetAsync("");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal("Working", content);
            Assert.Equal("text/plain", response.Content.Headers.ContentType.MediaType);

        }

        [Fact]
        public async Task Index2_ShouldReturnExpectedMediaType()
        {
            var response = await httpClient.GetAsync("dto");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var dto = JsonSerializer.Deserialize<CompanyDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal("Working", dto.Name);
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);

        }

        [Fact]
        public async Task Index3_ShouldReturnExpectedMessage_WithStream()
        {
            var response = await httpClient.GetStreamAsync("dto");

            var dto = await JsonSerializer.DeserializeAsync<CompanyDto>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal("Working", dto.Name);

        }

        [Fact]
        public async Task Index4_ShouldReturnExpectedMessageSimplifyed()
        {
            var dto = await httpClient.GetFromJsonAsync<CompanyDto>("dto");
            Assert.Equal("Working", dto.Name);

        }



    }
}
