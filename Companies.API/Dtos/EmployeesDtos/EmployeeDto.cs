using System.ComponentModel.DataAnnotations;

namespace Companies.API.Dtos.EmployeesDtos
{
    public record EmployeeDto()
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }

        public int Age { get; init; }
        public string? Position { get; init; }
    } 
    
    public abstract record EmployeeForManipulationDto()
    {

        [Required]
        [MaxLength(20)]
        public string? Name { get; init; }

        [Range(18, 80)]
        public int Age { get; init; }

        public string? Position { get; init; }
    }

    public record EmployeesForUpdateDto() : EmployeeForManipulationDto { }
}
