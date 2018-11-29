using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
     

namespace Educo.Parking.Data
{
    public class ParkingDBContext: IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private IConfiguration configuration;

        public ParkingDBContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<CarEntity> Cars
        {
            get; set;
        }

        public DbSet<ParkingEntity> Parkings
        {
            get; set;
        }

        public DbSet<ParkingHistoryEntity> ParkingHistory
        {
            get;set;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UsersHaveCars>().HasKey("IdUser", "IdCar");
        }
    }
}
