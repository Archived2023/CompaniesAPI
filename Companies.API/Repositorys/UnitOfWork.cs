using Companies.API.Data;

namespace Companies.API.Repositorys
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly APIContext db;
        private readonly Lazy<CompanyRepository> companyRepository;

        public ICompanyRepository CompanyRepository => companyRepository.Value;

        public UnitOfWork(APIContext db)
        {
            this.db = db;
            companyRepository = new Lazy<CompanyRepository>(() => new CompanyRepository(db));
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
