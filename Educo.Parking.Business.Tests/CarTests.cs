using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Educo.Parking.Business;
using Xunit;

namespace Educo.Parking.Business.Tests
{

    public class CarTestCase : IEnumerable<object[]>
    {

        public readonly List<object[]> IsDateTimeTestCase = new List<object[]>
        {
              new object[]{"something",new DateTime(2018,1,23), new DateTime(2018, 1, 22) },
              new object[]{"something", new DateTime(2020, 1, 23), new DateTime(2020, 1, 24) },
              new object[]{"something", new DateTime(2018, 1, 23), new DateTime(2018, 1, 24) },
              new object[]{null, new DateTime(2018, 1, 23), new DateTime(2018, 1, 24) },
              new object[]{"", new DateTime(2018, 1, 23), new DateTime(2018, 1, 24) },
              new object[]{" ", new DateTime(2018, 1, 23), new DateTime(2018, 1, 24) }
        };

        public IEnumerator<object[]> GetEnumerator() => IsDateTimeTestCase.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }


    public class CarTests
    {

        [Theory]
        [ClassData(typeof(CarTestCase))]
        public void ArriveDepartureTimeTest(string number, DateTime arrival, DateTime departure)
        {
            try
            {
                Car car = new Car() { StateNumber = number, Arrival = arrival, Departure = departure };
                Assert.NotNull(car);
            }
            catch (IncorrectDateTimeException ex)
            {
                Assert.True(ex is IncorrectDateTimeException);
            }
            catch (ArgumentNullException ex)
            {
                Assert.True(ex is ArgumentNullException);
            }
        }


    }
}
