using System.ComponentModel.DataAnnotations;

namespace Companies.API.Entities
{
    public class Company
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
        public string Address { get; set; } = string.Empty;
      
        [MaxLength(30, ErrorMessage = "Maximum length for the {0} is {1} characters")]
        public string? Country { get; set; } = string.Empty;

        //Nav prop
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
