using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Tests.Fixtures
{
    [CollectionDefinition("InMemoryCollection", DisableParallelization = true)]
    public class TestDataBaseCollectionFixture : ICollectionFixture<TestDataBaseFixture>
    {
    }
}
