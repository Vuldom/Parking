using System;
using System.Collections.Generic;
using System.Linq;

namespace Educo.Parking.Business
{
    public class Parking
    {
        protected List<Car> cars;

        public Parking()
        {
            cars = new List<Car>();
        }

        public IEnumerable<Car> Cars
        {
            get
            {
                return cars;
            }
        }

        public double Latitude
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }

        public string ParkingName
        {
            get;
            set;
        }

        public Car AddCar(string number, User owner, DateTime arrival)
        {
            if(number == null)
            {
                throw new ArgumentNullException();
            }
            if(owner == null)
            {
                throw new ArgumentNullException();
            }

            Car car = new Car { Owner = owner, StateNumber = number, Arrival = arrival };
            cars.Add(car);
            return car;
        }

        public bool RemoveCar(Car car)
        {
            if(car == null)
            {
                throw new ArgumentNullException();
            }
            return cars.Remove(car);
        }
    }
}
