using System;
using System.Collections.Generic;
using System.Text;

namespace Educo.Parking.Business
{
    public class Car
    {
        public int Id { get; set; }
        private string stateNumber;
        public string StateNumber
        {
            get { return stateNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException();
                }
                else stateNumber = value;
            }
        }
        public User Owner { get; internal set; }
        public string Manufacturer { get; set; }
        public  string Model { get; set;}

        public int Year { get; set; }

        public string Color { get; set; }
        
        private DateTime arrival;
        private DateTime departure;

        public DateTime Arrival
        {
            get { return arrival; }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new IncorrectDateTimeException();
                }
                else arrival = value;
            }
        }

        public DateTime Departure
        {
            get { return departure; }
            set
            {
                if (value < Arrival)
                {
                    throw new IncorrectDateTimeException();
                }
                else departure = value;

            }


        }
    }
}
