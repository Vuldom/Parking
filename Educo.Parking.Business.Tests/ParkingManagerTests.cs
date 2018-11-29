using Educo.Parking.Business.Managers;
using Educo.Parking.Business.Tests.Fakes;
using Educo.Parking.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Educo.Parking.Business.Tests
{
    public class ParkingManagerTests
    {
        DataContextFactory contextFactory;
        public ParkingManagerTests()
        {
            var databaseName = Guid.NewGuid().ToString();
            contextFactory = new TestDataContextFactory(databaseName);
        }

        [Fact]
        public void ArrivalTest()
        {
            User user = new User();
            user.Username = "Neo";
            Car car = new Car();
            car.Owner = user;
            car.StateNumber = "1234";
            Parking parking = new Parking();
            parking.ParkingName = "Zion";

            using (ParkingDBContext context = contextFactory.CreateDbContext())
            {
                ParkingEntity parkingEntity = new ParkingEntity();
                parkingEntity.Name = parking.ParkingName;
                context.Parkings.Add(parkingEntity);

                ApplicationUser userEntity = new ApplicationUser();
                userEntity.UserName = user.Username;
                context.Users.Add(userEntity);

                CarEntity carEntity = new CarEntity();
                carEntity.StateNumber = car.StateNumber;

                UsersHaveCars uhc = new UsersHaveCars();
                uhc.UserEntity = userEntity;
                uhc.CarEntity = carEntity;
                userEntity.UsersHaveCars = new List<UsersHaveCars>();
                userEntity.UsersHaveCars.Add(uhc);
                context.SaveChanges();
            }

            ParkingManager parkingManager = new ParkingManager(contextFactory);
            Parking parkList=parkingManager.Arrive(car, parking,car.Owner.Username);
            Assert.NotEmpty(parkList.Cars);

        }
        
        [Fact]
        public void ArrivalCarExceptionTest()
        {
            Car car = null;
            Parking parking = new Parking();
            try
            {
                ParkingManager parkingManager = new ParkingManager(contextFactory);
                Parking parkList = parkingManager.Arrive(car,parking,"somename");
                Assert.NotEmpty(parkList.Cars);
            }
            catch (ArgumentNullException ex)
            {
                Assert.True(ex is ArgumentNullException);
            }
        }

        [Fact]
        public void ArrivalParkingExceptionTest()
        {
            Car car = new Car();
            Parking parking =null;
            try
            {
                ParkingManager parkingManager = new ParkingManager(contextFactory);
                Parking parkList = parkingManager.Arrive(car, parking,"somename");
                Assert.NotEmpty(parkList.Cars);
            }
            catch (ArgumentNullException ex)
            {
                Assert.True(ex is ArgumentNullException);
            }
        }

        [Fact]
        public void DepartureTest()
        {
            User user = new User();
            user.Username = "Neo";
            Car car = new Car();
            car.Owner = user;
            car.StateNumber = "1234";
            Parking parking = new Parking();
            parking.ParkingName = "Zion";

            using (ParkingDBContext context = contextFactory.CreateDbContext())
            {
                ParkingEntity parkingEntity = new ParkingEntity();
                parkingEntity.Name = parking.ParkingName;
                context.Parkings.Add(parkingEntity);

                ApplicationUser userEntity = new ApplicationUser();
                userEntity.UserName = user.Username;
                context.Users.Add(userEntity);

                CarEntity carEntity = new CarEntity();
                carEntity.StateNumber = car.StateNumber;

                UsersHaveCars uhc = new UsersHaveCars();
                uhc.UserEntity = userEntity;
                uhc.CarEntity = carEntity;
                userEntity.UsersHaveCars = new List<UsersHaveCars>();
                userEntity.UsersHaveCars.Add(uhc);
                context.SaveChanges();
            }
            ParkingManager parkingManager = new ParkingManager(contextFactory);
            parkingManager.Arrive(car,parking,car.Owner.Username);
            parkingManager.Depart(car);
            Assert.DoesNotContain(car,parking.Cars);
        }
        
        [Fact]

        public void DepartureExceptionTest()
        {
            Car car = null;
            Parking parking = new Parking();
            try
            {
                ParkingManager parkingManager = new ParkingManager(contextFactory);
                parkingManager.Depart(car);
                Assert.DoesNotContain(car, parking.Cars);
            }
            catch (ArgumentNullException ex)
            {
                Assert.True(ex is ArgumentNullException);
            }

        }


    }
}
