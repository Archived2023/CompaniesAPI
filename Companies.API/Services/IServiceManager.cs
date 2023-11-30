namespace Companies.API.Services
{
    public interface IServiceManager
    {
        ICompanyService CompanyService { get; }
    }
}