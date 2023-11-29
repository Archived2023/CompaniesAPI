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
            CreateMap<CompanyForUpdateDto, Company>().ConvertUsing<CountryConverter>();
        }

    }
    public class CountryConverter : ITypeConverter<CompanyForUpdateDto, Company>
    {
        public Company Convert(CompanyForUpdateDto source, Company destination, ResolutionContext context)
        {

            ArgumentNullException.ThrowIfNull(nameof(source));
            ArgumentNullException.ThrowIfNull(nameof(source.Address));

            var parts = source.Address?.Split(",");

            if(parts != null && parts.Length == 2)
            {
                destination.Address = parts[0].Trim();
                destination.Country = parts[1].Trim();
                destination.Name = source.Name?.Trim();
            } 
            else if(parts != null && parts.Length == 1)
            {
                destination.Address = parts[0].Trim();
                destination.Name = source.Name?.Trim();
            }
            else
            {
                //ToDo custom exception!!!
                throw new ArgumentException();
            }

            return destination;
        }
    }
}
