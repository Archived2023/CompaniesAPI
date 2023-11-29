using Companies.API.Data;

namespace Companies.API.Repositorys
{
    public class CompanyRepository
    {
        private readonly APIContext db;

        public CompanyRepository(APIContext db)
        {
            this.db = db;
        }
    }
}
