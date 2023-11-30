using Companies.API.Data;

namespace Companies.API.Repositorys
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly APIContext db;
        private readonly Lazy<ICompanyRepository> companyRepository;

        public ICompanyRepository CompanyRepository => companyRepository.Value;

        public UnitOfWork(APIContext db, Lazy<ICompanyRepository> companyrepo)
        {
            this.db = db;
            companyRepository = companyrepo;
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
