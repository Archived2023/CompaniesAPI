using Companies.API;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

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



    }
}
