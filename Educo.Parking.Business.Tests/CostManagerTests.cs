using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Educo.Parking.Business.Tests
{
    public class CostManagerTests
    {
        [Fact]
        public void GetPriceReturnsValidCost ()
        {
            Car car = new Car();
            car.Arrival = new DateTime(2018, 06, 08, 10, 00, 00);
            car.Departure = new DateTime(2018, 06, 08, 22, 00, 00);

            CostManager costManager = new CostManager();
            Money result = costManager.GetPrice(car);

            Assert.NotNull(result);
            Assert.Equal(120, result.Value);
        }

        [Fact]
        public void GetPriceThrowsArgumentNullException ()
        {
            try
            {
                CostManager costManager = new CostManager();
                Money result = costManager.GetPrice(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.True(ex is ArgumentNullException);
            }
        }
    }
}
