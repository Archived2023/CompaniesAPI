using Companies.API;
using Companies.API.Data;
using Companies.API.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace Companies.Integration.Tests
{
    public class CustomWebApplicationFactory<T> : WebApplicationFactory<Program>, IDisposable
    {
        private APIContext Context = null!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureServices(services =>
            {
                var serviceDescriptor = services.SingleOrDefault(
                   d => d.ServiceType == typeof(DbContextOptions<APIContext>));

                if (serviceDescriptor != null)
                {
                    services.Remove(serviceDescriptor);
                }

                services.AddDbContext<APIContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });

                var scope = services.BuildServiceProvider().CreateScope();

                var provider = scope.ServiceProvider;
                Context = provider.GetRequiredService<APIContext>();

                Context.Companies.AddRange(
                                [
                                    new Company()
                                    {
                                        Name = "TestCompanyName",
                                        Address = "TestAdress",
                                        Country = "TestCountry",
                                        Employees = new Employee[]
                                        {
                                              new Employee
                                              {
                                                  Age = 50,
                                                  Name = "TestName",
                                                   Department = new Department
                                                   {
                                                        Name = "Developer"
                                                   }
                                              }
                                        }
                                    }
                                ]);


                Context.SaveChanges();

            });

            
        }

        public override ValueTask DisposeAsync()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
            return base.DisposeAsync();
        }
    }
}
