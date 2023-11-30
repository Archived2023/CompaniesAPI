namespace Companies.API.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> companyService;
        public ICompanyService CompanyService => companyService.Value;

        public ServiceManager(Lazy<ICompanyService> companyService)
        {
            this.companyService = companyService;
        }
    }
}
