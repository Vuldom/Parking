using Educo.Parking.Business.Managers;
using Educo.Parking.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educo.Parking.Business.Managers
{
    public class ParkingManager : IParkingManager
    {
        readonly DataContextFactory factory;
        public ParkingManager(DataContextFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory), $"{nameof(factory)} is null.");
        }

        public Parking Arrive(Car car, Parking parking,string username)
        {
            if (car == null) throw new ArgumentNullException();
            if (parking == null) throw new ArgumentNullException();

            DateTime arrival = DateTime.Now;
            using (ParkingDBContext context = factory.CreateDbContext())
            {
                ApplicationUser userEntity = context.Users.Single(us => us.UserName == username);
                User user=new User();
                user.Username = userEntity.UserName;
                user.FirstName = userEntity.FirstName;
                user.Email = userEntity.Email;
                user.LastName = userEntity.Lastname;
                user.Password = userEntity.PasswordHash;
                user.UserPhoto = userEntity.UserPhoto;
                CarEntity carEntity = context.Cars.Single(ca => ca.StateNumber == car.StateNumber);
                ParkingEntity parkingEntity = context.Parkings.Single(park => park.Name == parking.ParkingName);
                ParkingHistoryEntity historyEntity = new ParkingHistoryEntity();
                historyEntity.IdCar = carEntity.IdCar;
                historyEntity.IdUser = userEntity.Id;
                historyEntity.IdParking = parkingEntity.IdParking;
                historyEntity.Arrival = arrival;
                context.ParkingHistory.Add(historyEntity);
                context.SaveChanges();
                parking.AddCar(car.StateNumber, user, arrival);
                return parking;
            }
        }

        public Parking Depart(Car car)
        {
            if (car == null) throw new ArgumentNullException();
            DateTime departure = DateTime.Now;
            Parking parking = new Parking();
            using (ParkingDBContext context = factory.CreateDbContext())
            {
                CarEntity carEntity = context.Cars.Single(ca => ca.StateNumber == car.StateNumber);
                var history = context.ParkingHistory.Where(ph => ph.IdCar == carEntity.IdCar).Last();

                ParkingEntity parkingEntity = context.Parkings.Single(pa => pa.IdParking == history.IdParking);
                var carEntities = context.ParkingHistory.Include(ph => ph.Car).Where(ph => ph.IdParking == parkingEntity.IdParking && ph.Departure == null).Select(ph => ph.Car).ToList();
                parking.ParkingName = parkingEntity.Name;
                parking.Latitude = parkingEntity.Latitude;
                parking.Longitude = parkingEntity.Longitude;
                foreach (var carEnt in carEntities)
                {
                    User user = new User();
                    ApplicationUser userEntity = context.Users.Single(us => us.UsersHaveCars.Any(uhc => uhc.IdCar == carEnt.IdCar));
                    user.Username = userEntity.UserName;
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.Lastname;
                    user.UserPhoto = userEntity.UserPhoto;
                    Car carInParking = new Car();
                    carInParking.StateNumber = carEnt.StateNumber;
                    carInParking.Id = carEnt.IdCar;
                    carInParking.Manufacturer = carEnt.Manufacturer;
                    carInParking.Color = carEnt.Color;
                    carInParking.Model = carEnt.Model;
                    carInParking.Year = carEnt.Year;
                    carInParking.Owner = user;
                    ParkingHistoryEntity parkingHistoryIn = context.ParkingHistory.Last(phe => phe.IdCar == carEnt.IdCar);
                    parking.AddCar(carInParking.StateNumber, carInParking.Owner, parkingHistoryIn.Arrival);
                }
                history.Departure = departure;
                context.SaveChanges();
                return parking;
            }
        }

        public async Task<string> GetParkingNameByCarId(int carId)
        {
            string parkingName = "";

            using (ParkingDBContext context = factory.CreateDbContext())
            {
                ParkingHistoryEntity parkingHistoryEntity = await context.ParkingHistory.Include(p => p.Parking).Where(ph => ph.IdCar == carId).LastOrDefaultAsync();

                parkingName = parkingHistoryEntity?.Parking?.Name;
            }

            return parkingName;
        }

        public List<Parking> GetParkings()
        {
            List<Parking> parkings = new List<Parking>();
            using (ParkingDBContext context = factory.CreateDbContext())
            {
                var parkingEntities = context.Parkings.Select(pe => new Parking
                {
                    ParkingName = pe.Name,
                    Longitude = pe.Longitude,
                    Latitude = pe.Latitude
                }).ToList();

                foreach (var parkEnt in parkingEntities)
                {
                    Parking parking = new Parking();
                    parking.ParkingName = parkEnt.ParkingName;
                    parking.Latitude = parkEnt.Latitude;
                    parking.Longitude = parkEnt.Longitude;
                    parkings.Add(parking);
                }
            }
            return parkings;
        }
    }
}
