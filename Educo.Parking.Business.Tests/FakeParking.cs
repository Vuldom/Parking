using System;
using System.Collections.Generic;
using System.Text;
using Educo.Parking.Business;

namespace Educo.Parking.Business.Tests
{
    public class FakeParking : Parking
    {

        public FakeParking(Car car)
        {
            cars = new List<Car>();
            cars.Add(car);

        }
    }
}
