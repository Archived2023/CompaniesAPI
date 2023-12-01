using System.ComponentModel.DataAnnotations;

namespace Companies.API.Dtos.CompaniesDtos
{
    public abstract class CompanyForManipulationDto
    {
        /// <summary>
        /// Name of company for update
        /// </summary>
        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the {0} is {1} characters.")]
        public string? Name { get; set; }
        /// <summary>
        /// Address of company for update
        /// </summary>
        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the {0} is {1} characters")]
        public string? Address { get; set; }

       

    }
}
