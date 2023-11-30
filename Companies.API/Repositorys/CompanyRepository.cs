using AutoMapper;
using Companies.API.Data;
using Companies.API.Dtos.CompaniesDtos;
using Companies.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Companies.API.Repositorys
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly APIContext db;

        public CompanyRepository(APIContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(Company company)
        {
            await db.Companies.AddAsync(company);
        }

        public async Task<List<Company>> GetAsync(bool includeEmployees = false)
        {
            return includeEmployees ?  await db.Companies.Include(c => c.Employees).ToListAsync() 
                                        :  await db.Companies.ToListAsync();

        }
        public async Task<Company?> GetAsync(Guid id)
        {
            return await db.Companies.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Remove(Company company)
        {
            db.Companies.Remove(company);
        }

        public void Update(Company company)
        {
            db.Companies.Update(company);
        }
    }
}
