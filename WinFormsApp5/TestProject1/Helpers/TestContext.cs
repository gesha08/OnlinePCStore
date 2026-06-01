using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;

namespace TestProject1.Helpers
{
    internal class TestContext
    {
        public static StoreContext CreateTestContext()
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString() )
                .Options;
            var context = new StoreContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
