using AutoMapper;
using Companies.API.Dtos.EmployeesDtos;
using Companies.API.Entities;

namespace Companies.API.Mappings
{
    public class EmployeeMappings : Profile
    {
        public EmployeeMappings()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(
                dest => dest.Position,
                from => from.MapFrom(e => e.Department.Name));
        }
    }
}
