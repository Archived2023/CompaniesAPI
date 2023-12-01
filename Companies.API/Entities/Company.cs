using System.ComponentModel.DataAnnotations;

namespace Companies.API.Entities
{
    /// <summary>
    /// A company with Id, Name, Address and Country fields
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Id of the compay
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the company
        /// </summary>

        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }

        /// <summary>
        /// Address of the company
        /// </summary>
        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Country of the company
        /// </summary>     

      
        [MaxLength(30, ErrorMessage = "Maximum length for the {0} is {1} characters")]
        public string? Country { get; set; } = string.Empty;

        //Nav prop
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
