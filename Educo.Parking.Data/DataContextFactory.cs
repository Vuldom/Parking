using System;
using System.IO;
using Educo.Parking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Educo.Parking.Data
{
    public class DataContextFactory : IDesignTimeDbContextFactory<ParkingDBContext>
    {
        private IConfiguration configuration;

        public DataContextFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DataContextFactory()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            this.configuration= new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
        }
 
        public virtual ParkingDBContext CreateDbContext(string[] args=null)
        {
            var context = new ParkingDBContext(configuration);
            return context;
        }
    }
}
