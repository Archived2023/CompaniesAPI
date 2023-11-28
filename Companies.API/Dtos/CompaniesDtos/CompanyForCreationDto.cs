using System.ComponentModel.DataAnnotations;

namespace Companies.API.Dtos.CompaniesDtos
{
    public class CompanyForCreationDto
    {
        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the {0} is {1} characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the {0} is {1} characters")]
        public string? Address { get; set; }
        public string? Country { get; set; }

    }
}
