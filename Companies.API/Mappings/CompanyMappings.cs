using AutoMapper;
using Companies.API.Dtos.CompaniesDtos;
using Companies.API.Entities;

namespace Companies.API.Mappings
{
    public class CompanyMappings : Profile
    {
        public CompanyMappings()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(
                dest => dest.Address,
                from => from.MapFrom(
                    c => $"{c.Address}{(string.IsNullOrEmpty(c.Country) ? 
                    string.Empty : 
                    $", {c.Country}")}"));

            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<CompanyForUpdateDto, Company>();
        }
    }
}
