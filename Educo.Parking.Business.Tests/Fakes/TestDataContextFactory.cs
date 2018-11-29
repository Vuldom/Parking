using System;
using Educo.Parking.Data;
using Microsoft.EntityFrameworkCore;

namespace Educo.Parking.Business.Tests.Fakes
{
    public class TestDataContextFactory : DataContextFactory
    {
        readonly string databaseName;

        public TestDataContextFactory(string databaseName):base (null)
        {
            if(string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentException($"{nameof(databaseName)} is null or empty.", nameof(databaseName));
            }
            this.databaseName = databaseName;
        }

        public override ParkingDBContext CreateDbContext(string[] args = null)
        {
            var context = new InMemoryDbContext(databaseName);
            return context;
        }
    }
}
