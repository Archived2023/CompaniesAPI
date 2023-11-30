using Companies.API.Dtos.CompaniesDtos;

namespace Companies.API.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAsync(bool includeEmployees = false);
        Task<CompanyDto> GetAsync(Guid id);
    }
}