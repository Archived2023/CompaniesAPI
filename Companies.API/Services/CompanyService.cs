using AutoMapper;
using Companies.API.Dtos.CompaniesDtos;
using Companies.API.Repositorys;

namespace Companies.API.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> GetAsync(bool includeEmployees = false)
        {
            //Maybe some more logic from another service
            var companies = await unitOfWork.CompanyRepository.GetAsync(includeEmployees);
            var dtos = mapper.Map<IEnumerable<CompanyDto>>(companies);
            return dtos;
        }

        public async Task<CompanyDto> GetAsync(Guid id)
        {
            var company = await unitOfWork.CompanyRepository.GetAsync(id);
            var companyDto = mapper.Map<CompanyDto>(company);
            return companyDto;
        }
    }
}
