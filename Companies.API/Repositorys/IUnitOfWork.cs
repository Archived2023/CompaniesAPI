
namespace Companies.API.Repositorys
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }

        Task CompleteAsync();
    }
}