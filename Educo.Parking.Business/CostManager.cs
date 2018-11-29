using System;
using System.Collections.Generic;
using System.Text;

namespace Educo.Parking.Business
{
    public class CostManager
    {
        private const decimal CostPerHour = 10;

        public Money GetPrice(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException();
            }
            TimeSpan parkingTime = car.Departure - car.Arrival;
            int parkingTimeHours = parkingTime.Hours;
            decimal parkingCost = parkingTimeHours * CostPerHour;
            Money money = new Money();
            money.Value = parkingCost;
            money.Currency = Currencies.BYN;
            return money;
        }
    }
}
