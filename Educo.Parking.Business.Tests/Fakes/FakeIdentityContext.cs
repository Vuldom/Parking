using Educo.Parking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Educo.Parking.Business.Tests.Fakes
{
    class FakeIdentityContext: ParkingDBContext
    {
        private IConfiguration configuration;

        public FakeIdentityContext(IConfiguration configuration) : base(null)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = configuration.GetSection("databaseName").Value;
            optionsBuilder.UseInMemoryDatabase(connectionString);
        }
    }
}
