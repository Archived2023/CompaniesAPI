
using Microsoft.EntityFrameworkCore;

namespace Companies.API.Data
{
    public class SeedData
    {
        private static APIContext db;
        internal static async Task InitAsync(APIContext context)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));

            if (await db.Companies.AnyAsync()) return;
        }
    }
}
