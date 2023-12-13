using AutoMapper;
using Companies.API.Data;
using Companies.API.Entities;
using Companies.API.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Tests.Fixtures
{
    public class TestDataBaseFixture : IDisposable
    {
        public Mapper Mapper { get; private set; }
        public APIContext Context { get; }

        public TestDataBaseFixture()
        {
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeMappings>();
            }));

            var options = new DbContextOptionsBuilder<APIContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDB;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            Context = new APIContext(options);
            Context.Database.Migrate();

            Context.Companies.AddRange(new[]
          {
                new Company
                {
                    Name = "TestComp",
                     Address = "TestAddres",
                     Country = "Sweden",
                      Employees = new[]
                      {
                          new Employee
                          {
                               Name = "Pelle",
                                Age = 50,
                                 Department = new Department
                                 {
                                      Name = "Developer"
                                 }
                          },
                          new Employee
                          {
                               Name = "Nisse",
                                Age = 40,
                                 Department = new Department
                                 {
                                      Name = "Tester"
                                 }
                          },
                      }
                }
            });

            Context.SaveChanges();


        }


        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
