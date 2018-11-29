using Educo.Parking.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Educo.Parking.Shell.Web.Extensions
{
    public static class ApplicationExtensions
    {
        public static void UseDataMigrations(this IApplicationBuilder app)
        {
            DataContextFactory factory = (DataContextFactory)app.ApplicationServices.GetService(typeof(DataContextFactory));

            using (ParkingDBContext context = factory.CreateDbContext())
            {
                context.Database.Migrate();
            }
        }
    }
}
