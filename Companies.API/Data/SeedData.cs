
using Bogus;
using Companies.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Companies.API.Data
{
    public class SeedData
    {
        private static APIContext db = null!;
        internal static async Task InitAsync(APIContext context)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));

            if (await db.Companies.AnyAsync()) return;

            var departments = GenerateDepartments();
            await db.AddRangeAsync(departments);
            var companies = GenerateCompanies(5, departments);
            await db.AddRangeAsync(companies);
            await db.SaveChangesAsync();
        }

        private static IEnumerable<Department> GenerateDepartments()
        {
            string[] positions = ["Developer", "Tester", "Manager"];

            List<Department> departments = new List<Department>();

            for (int i = 0; i < positions.Length; i++)
            {
                var department = new Department
                {
                    Name = positions[i]
                };

                departments.Add(department);
            }
            
            return departments;
        }

        private static IEnumerable<Company> GenerateCompanies(int nrOfCompanies, IEnumerable<Department> departments)
        {
            var faker = new Faker<Company>("sv").Rules((f, c) =>
            {
                c.Name = f.Company.CompanyName();
                c.Country = f.Address.Country();
                c.Address = f.Address.StreetAddress();
                c.Employees = GenerateEmployees(f.Random.Int(min: 2, max: 10), departments);
            });

            return faker.Generate(nrOfCompanies);
        }

        private static ICollection<Employee> GenerateEmployees(int nrOfEmplyees, IEnumerable<Department> departments)
        {
            var departmentList = departments.ToList();

            var faker = new Faker<Employee>("sv").Rules((f, e) =>
            {
                e.Name = f.Person.FullName;
                e.Age = f.Random.Int(min: 18, max: 70);
                e.Department = departmentList[f.Random.Int(0, departmentList.Count - 1)];
            });

            return faker.Generate(nrOfEmplyees);
        }
    }
}
