using Educo.Parking.Business.Tests.Fakes;
using Educo.Parking.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Educo.Parking.Business.Tests
{
    public class CarManagerTests
    {
        DataContextFactory contextFactory;
        public CarManagerTests()
        {
            var databaseName = Guid.NewGuid().ToString();
            contextFactory = new TestDataContextFactory(databaseName);
        }

        [Fact]
        public void GetCarsTest()
        {
            string username = "Alex";
            using (ParkingDBContext context = contextFactory.CreateDbContext())
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = username;
                CarEntity firstCar = new CarEntity();
                firstCar.StateNumber = "1515-IO7";

                context.Users.Add(user);

                UsersHaveCars uhc = new UsersHaveCars();
                uhc.UserEntity = user;
                uhc.CarEntity = firstCar;
                user.UsersHaveCars = new List<UsersHaveCars>();
                user.UsersHaveCars.Add(uhc);
                context.SaveChanges();
            }
            CarManager carManager = new CarManager(contextFactory);
            List<Car> cars = carManager.GetCars(username);
            Assert.NotEmpty(cars);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetCarsExceptionTest(string username)
        {
            try
            {
                CarManager carManager = new CarManager(contextFactory);
                List<Car> cars = carManager.GetCars(username);
                Assert.NotEmpty(cars);
            }
            catch (ArgumentNullException ex)
            {
                Assert.True(ex is ArgumentNullException);
            }

        }



        [Fact]
        public void CarUpdateTest()
        {
            Car car = new Car();
            using (ParkingDBContext context = contextFactory.CreateDbContext())
            {
                CarEntity carEntity = new CarEntity();
                carEntity.StateNumber = "1234-AA7";
                context.Cars.Add(carEntity);
                car.Id = carEntity.IdCar;
                context.SaveChanges();
            }

            car.StateNumber = "1478 -AA7";
            CarManager carManager = new CarManager(contextFactory);
            Car changedCar = carManager.CarUpdate(car);
            Assert.NotNull(changedCar);
            Assert.Equal(changedCar.StateNumber, car.StateNumber);

        }

        [Fact]
        public void CarUpdateExceptionTest()
        {
            Car car = null;
            try
            {
                CarManager carManager = new CarManager(contextFactory);
                Car changedCar = carManager.CarUpdate(car);
                Assert.NotNull(changedCar);
            }
            catch(ArgumentNullException ex)
            {
                Assert.True(ex is ArgumentNullException);
            }

        }

        [Fact]
        public void AddCarTest()
        {
            Car car = new Car();
            car.StateNumber = "1234-AA7";
            car.Manufacturer = "Tesla";
            car.Model = "X";
            car.Year = 2017;
            car.Color = "Black";
            string username = "alexant";

            using (ParkingDBContext context = contextFactory.CreateDbContext())
            {
                ApplicationUser entity = new ApplicationUser();
                entity.UserName = username;
                context.Users.Add(entity);
                context.SaveChanges();
            }

            CarManager carManager = new CarManager(contextFactory);
            Car addedCar = carManager.AddCar(car, username);
            Assert.NotNull(addedCar);
            Assert.Equal(car.StateNumber, addedCar.StateNumber);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void AddCarUsernameExceptionTest(string username)
        {
            Car car = new Car();
            try
            {
                CarManager carManager = new CarManager(contextFactory);
                Car addedCar = carManager.AddCar(car, username);
                Assert.NotNull(addedCar);
                Assert.Equal(car.StateNumber, addedCar.StateNumber);
            }
            catch (ArgumentNullException ex)
            {
                Assert.True(ex is ArgumentNullException);
            }
        }

        [Fact]
        public void AddCarExceptionTest()
        {
            string username = "AlexAnt";
            Car car = null;
            try
            {
                CarManager carManager = new CarManager(contextFactory);
                Car addedCar = carManager.AddCar(car, username);
                Assert.NotNull(addedCar);
                Assert.Equal(car.StateNumber, addedCar.StateNumber);
            }
            catch (ArgumentNullException ex)
            {
                Assert.True(ex is ArgumentNullException);
            }
        }


        [Fact]
        public void RemoveCarTest()
        {
            Car car = new Car();
            car.StateNumber = "1234-AA7";
            using (ParkingDBContext context = contextFactory.CreateDbContext())
            {
                ApplicationUser userEntity = new ApplicationUser();
                userEntity.UserName = "AlexAnt";
                CarEntity carEntity = new CarEntity();
                carEntity.StateNumber = car.StateNumber;
                context.Users.Add(userEntity);
                context.Cars.Add(carEntity);
                car.Id = carEntity.IdCar;
                UsersHaveCars uhc = new UsersHaveCars();
                uhc.UserEntity = userEntity;
                uhc.CarEntity = carEntity;
                userEntity.UsersHaveCars = new List<UsersHaveCars>();
                userEntity.UsersHaveCars.Add(uhc);

                context.SaveChanges();
            }
            CarManager carManager = new CarManager(contextFactory);
            bool removedCar = carManager.RemoveCar(car);
            Assert.True(removedCar);
        }

        [Fact]
        public void RemoveCarExceptionsTest()
        {
            Car car = null;
            try
            {
                CarManager carManager = new CarManager(contextFactory);
                bool removedCar = carManager.RemoveCar(car);
                Assert.True(removedCar);
            }
            catch (ArgumentNullException ex)
            {
                Assert.True(ex is ArgumentNullException);
            }
        }

        [Fact]
        public void GetParkedCarsTest()
        {
            string username = "Alex";
            using (ParkingDBContext context = contextFactory.CreateDbContext())
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = username;
                CarEntity firstCar = new CarEntity();
                firstCar.StateNumber = "1515-IO7";

                context.Users.Add(user);

                UsersHaveCars uhc = new UsersHaveCars();
                uhc.UserEntity = user;
                uhc.CarEntity = firstCar;
                user.UsersHaveCars = new List<UsersHaveCars>();
                user.UsersHaveCars.Add(uhc);

                ParkingHistoryEntity parkingHistoryEntity = new ParkingHistoryEntity();
                parkingHistoryEntity.IdCar = firstCar.IdCar;
                parkingHistoryEntity.IdUser = user.Id;
                parkingHistoryEntity.IdParking =1;
                parkingHistoryEntity.Arrival = DateTime.Now;
                parkingHistoryEntity.Car = firstCar;
                parkingHistoryEntity.User = user;

                context.ParkingHistory.Add(parkingHistoryEntity);
                context.SaveChanges();
            }
            CarManager carManager = new CarManager(contextFactory);
            List<Car> cars = carManager.GetParkedCars(username);
            Assert.NotEmpty(cars);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetParkedCarsExceptionTest(string username)
        {
            try
            {
                CarManager carManager = new CarManager(contextFactory);
                List<Car> cars = carManager.GetParkedCars(username);
                Assert.NotEmpty(cars);
            }
            catch (ArgumentNullException ex)
            {
                Assert.True(ex is ArgumentNullException);
            }

        }


    }
}
