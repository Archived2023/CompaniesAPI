using Companies.API.Entities;

namespace Companies.API.Repositorys
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company);
        Task<List<Company>> GetAsync(bool includeEmployees = false);
        Task<Company?> GetAsync(Guid id);
        void Remove(Company company);
    }
}