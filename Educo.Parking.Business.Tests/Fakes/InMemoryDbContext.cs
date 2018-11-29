using System;
using Educo.Parking.Data;
using Microsoft.EntityFrameworkCore;

namespace Educo.Parking.Business.Tests.Fakes
{
    class InMemoryDbContext : ParkingDBContext
    {
        private string databaseName;

        internal InMemoryDbContext(string databaseName):base(null)
        {
            this.databaseName = databaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName);
        }
    }
}
