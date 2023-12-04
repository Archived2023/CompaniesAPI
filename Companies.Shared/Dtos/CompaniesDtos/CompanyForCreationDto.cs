using System.ComponentModel.DataAnnotations;

namespace Companies.API.Dtos.CompaniesDtos
{
    public class CompanyForCreationDto : CompanyForManipulationDto
    {
        [MaxLength(30, ErrorMessage = "Maximum length for the {0} is {1} characters")]
        public string? Country { get; set; }
    }
}
