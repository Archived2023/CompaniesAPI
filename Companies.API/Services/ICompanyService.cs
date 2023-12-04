using Companies.API.Dtos.CompaniesDtos;
using Companies.API.Responses;

namespace Companies.API.Services
{
    public interface ICompanyService
    {
        Task<BaseResponse> GetAsync(bool includeEmployees = false);
        Task<CompanyDto> GetAsync(Guid id);
        Task UpdateAsync(Guid id, CompanyForUpdateDto dto);
    }
}