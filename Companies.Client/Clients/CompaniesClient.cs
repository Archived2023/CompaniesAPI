using Companies.API.Dtos.CompaniesDtos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Companies.Client.Clients
{
    public class CompaniesClient : ICompaniesClient
    {
        private readonly HttpClient client;
        private const string json = "application/json";


        public CompaniesClient(HttpClient httpClient)
        {
            this.client = httpClient;
            client.BaseAddress = new Uri("https://localhost:7157");
            // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(json));
            client.Timeout = new TimeSpan(0, 0, 5);
        }

        public async Task<T?> GetAsync<T>(string path, string contentType = json)
        {
            var requst = new HttpRequestMessage(HttpMethod.Get, path);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

            var response = await client.SendAsync(requst, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();
            var result = JsonSerializer.Deserialize<T>(stream, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return result;

        }
    }
}
