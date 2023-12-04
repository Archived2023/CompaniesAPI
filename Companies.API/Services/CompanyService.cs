using AutoMapper;
using Companies.API.Dtos.CompaniesDtos;
using Companies.API.Exceptions;
using Companies.API.Repositorys;
using Companies.API.Responses;

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

        public async Task<BaseResponse> GetAsync(bool includeEmployees = false)
        {
            //Maybe some more logic from another service
            var companies = await unitOfWork.CompanyRepository.GetAsync(includeEmployees);
            var dtos = mapper.Map<IEnumerable<CompanyDto>>(companies);
            return new OkResponse<IEnumerable<CompanyDto>>(dtos);
        }

        public async Task<BaseResponse> GetAsync(Guid id)
        {
            var company = await unitOfWork.CompanyRepository.GetAsync(id); 
            if(company is null)
                return new CompanyNotFoundResponse(id);

            return new OkResponse<CompanyDto>(mapper.Map<CompanyDto>(company));
        }

        public async Task UpdateAsync(Guid id, CompanyForUpdateDto dto)
        {
            var company = await unitOfWork.CompanyRepository.GetAsync(id) ?? throw new CompanyNotFoundException(id);
            mapper.Map(dto, company);
            unitOfWork.CompanyRepository.Update(company);
            await unitOfWork.CompleteAsync();

        }
    }
}
