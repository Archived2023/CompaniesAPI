namespace Companies.API.Dtos.CompaniesDtos
{
    public class CompanyForUpdateDto: CompanyForManipulationDto 
    {
        public Guid Id { get; set; }
    }
}
