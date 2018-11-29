using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Educo.Parking.Business;
using System.Collections;

namespace Educo.Parking.Business.Tests
{

    public class ParkingTests
    {
        [Fact]
        public void AddCarTest()
        {

            Parking parking = new Parking();
            Car car = parking.AddCar("mmmm", new User() { Username = "Morpheus" }, new DateTime(2018, 2, 23));
            Assert.NotNull(car.StateNumber);
            Assert.NotNull(car.Owner);
            Assert.Contains(car, parking.Cars);
        }

        [Fact]
        public void RemoveCarTest()
        {
            User user = new User();
            Car car = new Car() { Owner = user, StateNumber = "number", Arrival = new DateTime(2018, 1, 23)};
            FakeParking parking = new FakeParking(car);
            Assert.True(parking.RemoveCar(car));
            Assert.Empty(parking.Cars);
        }


    }
}
