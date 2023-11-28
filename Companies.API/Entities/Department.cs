using System.ComponentModel.DataAnnotations;

namespace Companies.API.Entities
{
    public class Department
    {
        public Guid Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}