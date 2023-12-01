namespace Companies.API.Dtos.CompaniesDtos
{
    /// <summary>
    /// A company for update with Id field inheriting values for Name and Address from CompanyForManipuationDto
    /// </summary>
    public class CompanyForUpdateDto: CompanyForManipulationDto 
    {
        /// <summary>
        /// Id for the company to be updated
        /// </summary>
        public Guid Id { get; set; }
    }
}
