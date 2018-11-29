using Educo.Parking.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Educo.Parking.Business
{
    public class CarManager
    {
        readonly DataContextFactory factory;
        public CarManager(DataContextFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory), $"{nameof(factory)} is null.");
        }


        public List<Car> GetCars(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(username);
            List<Car> cars = new List<Car>();

            using (ParkingDBContext context = factory.CreateDbContext())
            {
                ApplicationUser userEntity = context.Users.SingleOrDefault(us => us.UserName == username);
                if (userEntity != null)
                {
                    var carEntities = context.Cars.Where(c => c.UsersHaveCars.Any(uhc => uhc.IdUser == userEntity.Id));
                    //var carEntities = context.Cars.Where(c => c.UsersHaveCars.Any(uhc => uhc.IdUser == userEntity.Id)).Select(c => new Car
                    //{
                    //    StateNumber = c.StateNumber,
                    //    Manufacturer = c.Manufacturer,
                    //    Color = c.Color,
                    //    Model = c.Model,
                    //    Year = c.Year

                    //}).ToList();
                    foreach (var carEnt in carEntities)
                    {
                        Car car = new Car();
                        car.Id = carEnt.IdCar;
                        car.StateNumber = carEnt.StateNumber;
                        car.Manufacturer = carEnt.Manufacturer;
                        car.Color = carEnt.Color;
                        car.Model = carEnt.Model;
                        car.Year = carEnt.Year;
                        cars.Add(car);
                    }
                }
            }

            return cars;
        }
        public Car CarUpdate(Car car)
        {
            if (car == null) throw new ArgumentNullException();

            using (ParkingDBContext context = factory.CreateDbContext())
            {
                CarEntity carEntity = context.Cars.Single(c => c.IdCar == car.Id);

                carEntity.StateNumber = car.StateNumber;
                carEntity.Manufacturer = car.Manufacturer;
                carEntity.Model = car.Model;
                carEntity.Color = car.Color;
                carEntity.Year = car.Year;
                context.Cars.Update(carEntity);
                int number = context.SaveChanges();
                if (number == 0)
                {
                    throw new InvalidOperationException();
                }

                return car;
            }

        }

        public Car AddCar(Car car, string username)
        {
            if (car == null) throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(username);
            using (ParkingDBContext context = factory.CreateDbContext())
            {
                ApplicationUser userEntity = context.Users.Single(us => us.UserName == username);
                CarEntity carEntity = new CarEntity();
                carEntity.Manufacturer = car.Manufacturer;
                carEntity.Model = car.Model;
                carEntity.StateNumber = car.StateNumber;
                carEntity.Year = car.Year;
                carEntity.Color = car.Color;
                context.Cars.Add(carEntity);
                UsersHaveCars uhc = new UsersHaveCars();
                uhc.UserEntity = userEntity;
                uhc.CarEntity = carEntity;
                carEntity.UsersHaveCars = new List<UsersHaveCars>();
                carEntity.UsersHaveCars.Add(uhc);
                context.SaveChanges();
                return car;
            }
        }

        public bool RemoveCar(Car car)
        {
            if (car == null) throw new ArgumentNullException();
            using (ParkingDBContext context = factory.CreateDbContext())
            {
                CarEntity carEntity= context.Cars.Single(c => c.IdCar == car.Id);
                context.Cars.Remove(carEntity);
                int result = context.SaveChanges();
                if (result != 1)
                {
                    throw new InvalidOperationException();
                }
                return true;
            }
        }

        public Car GetCar(string statenumber)
        {
            using (ParkingDBContext context = factory.CreateDbContext())
            {
                CarEntity carEntity = context.Cars.Single(c => c.StateNumber == statenumber);
                Car car = new Car();
                car.Color = carEntity.Color;
                car.Id = carEntity.IdCar;
                car.Manufacturer = carEntity.Manufacturer;
                car.Model = carEntity.Model;
                car.StateNumber = carEntity.StateNumber;
                car.Year = carEntity.Year;
                return car;
            }
        }

        public List<Car> GetParkedCars(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(username);
            List<Car> cars = new List<Car>();

            using (ParkingDBContext context = factory.CreateDbContext())
            {
                ApplicationUser userEntity = context.Users.SingleOrDefault(us => us.UserName == username);
                if (userEntity != null)
                {
                    var carEntities = context.Cars.Where(ca => ca.History.Any(hist => hist.User.UserName == username && hist.Departure == null));                                
                    foreach (var carEnt in carEntities)
                    {
                        Car car = new Car();
                        car.Id = carEnt.IdCar;
                        car.StateNumber = carEnt.StateNumber;
                        car.Manufacturer = carEnt.Manufacturer;
                        car.Color = carEnt.Color;
                        car.Model = carEnt.Model;
                        car.Year = carEnt.Year;
                        cars.Add(car);
                    }
                }
            }

            return cars;
        }

    }
}
