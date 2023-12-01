using System.ComponentModel.DataAnnotations;

namespace Companies.API.Entities
{
#pragma warning disable CS1591
    public class Employee
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Age is a required field.")]
        public int Age { get; set; }

        //[Required(ErrorMessage = "Position is a required field.")]
        //[StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        //public string Position { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = default!;

        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = default!; // = new Company();
    }
#pragma warning restore C1591
}
