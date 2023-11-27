
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

            var companies = GenerateCompanies(5);
            await db.AddRangeAsync(companies);
            await db.SaveChangesAsync();
        }

        private static IEnumerable<Company> GenerateCompanies(int nrOfCompanies)
        {
            var faker = new Faker<Company>("sv").Rules((f, c) =>
            {
                c.Name = f.Company.CompanyName();
                c.Country = f.Address.Country();
                c.Address = f.Address.StreetAddress();
                c.Employees = GenerateEmployees(f.Random.Int(min: 2, max: 10));
            });

            return faker.Generate(nrOfCompanies);
        }

        private static ICollection<Employee> GenerateEmployees(int nrOfEmplyees)
        {
            string[] positions = ["Developer", "Tester", "Manager"];

            var faker = new Faker<Employee>("sv").Rules((f, e) =>
            {
                e.Name = f.Person.FullName;
                e.Age = f.Random.Int(min: 18, max: 70);
                e.Position = positions[f.Random.Int(0, positions.Length - 1)];
            });

            return faker.Generate(nrOfEmplyees);
        }
    }
}
