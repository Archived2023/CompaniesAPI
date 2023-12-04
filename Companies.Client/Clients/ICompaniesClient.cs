
namespace Companies.Client.Clients
{
    public interface ICompaniesClient
    {
        Task<T?> GetAsync<T>(string path, string contentType = "application/json");
    }
}