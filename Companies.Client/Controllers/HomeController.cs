using Companies.API.Dtos.CompaniesDtos;
using Companies.API.Dtos.EmployeesDtos;
using Companies.Client.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Companies.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient httpClient;
        private const string json = "application/json";

        public HomeController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7157");
        }

        public async Task<IActionResult> Index()
        {

            //var res = await SimpleGetAsync();
            //var res2 = await SimpleGetAsync2();
            //var res3 = await GetWithRequestMessageAsync();
            //var res4 = await PostWithRequestMessageAsync();
            await PatchWithRequestMessageAsync();


            return View();
        }

        private async Task PatchWithRequestMessageAsync()
        {
            var patchDocument = new JsonPatchDocument<EmployeesForUpdateDto>();
            patchDocument.Replace(e => e.Age, 75);
            patchDocument.Replace(e => e.Name, "Nisse");

            var serializedPatchDoc = Newtonsoft.Json.JsonConvert.SerializeObject(patchDocument);

            var requst = new HttpRequestMessage(HttpMethod.Patch, "api/companies/ccc6bb2f-d2c6-4fbe-033b-08dbf0dc3565/employees/807f7457-fea9-492b-c46f-08dbf0dc3567");
            requst.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));
            requst.Content.Headers.ContentType = new MediaTypeHeaderValue(json);
            requst.Content = new StringContent(serializedPatchDoc);


            var response = await httpClient.SendAsync(requst);
            response.EnsureSuccessStatusCode();
        }

        private async Task<CompanyDto> PostWithRequestMessageAsync()
        {
            var requst = new HttpRequestMessage(HttpMethod.Post, "api/companies");
            requst.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            var companyToCreate = new CompanyForCreationDto
            {
                Name = "Apelsinkompaniet",
                Address = "Sveavägen 34",
                Country = "Sweden"
            };

            var jsonCompany = JsonSerializer.Serialize(companyToCreate);

            requst.Content = new StringContent(jsonCompany);
            requst.Content.Headers.ContentType = new MediaTypeHeaderValue(json);

            var response = await httpClient.SendAsync(requst);
            response.EnsureSuccessStatusCode();

            var res = await response.Content.ReadAsStringAsync();

            var companyDto = JsonSerializer.Deserialize<CompanyDto>(res, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var location = response.Headers.Location;
            return companyDto!;


        }

        private async Task<IEnumerable<CompanyDto>> GetWithRequestMessageAsync()
        {
            var requst = new HttpRequestMessage(HttpMethod.Get, "api/companies");
            requst.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            var response = await httpClient.SendAsync(requst);
            response.EnsureSuccessStatusCode();

            var res = await response.Content.ReadAsStringAsync();

            var companies = JsonSerializer.Deserialize<IEnumerable<CompanyDto>>(res, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return companies!;

        }

        private async Task<IEnumerable<CompanyDto>?> SimpleGetAsync2()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<CompanyDto>>("api/companies", new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        private async Task<IEnumerable<CompanyDto>> SimpleGetAsync()
        {
            var response = await httpClient.GetAsync("api/companies");
            response.EnsureSuccessStatusCode();

            var res = await response.Content.ReadAsStringAsync();

            var companies = JsonSerializer.Deserialize<IEnumerable<CompanyDto>>(res, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return companies!;
        }

    }
}
