using System;
using System.Collections.Generic;
using System.Linq;
using Educo.Parking.Business;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Educo.Parking.Shell.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SelectListItem> CarItems
        {
            get;
            set;
        }

        public IEnumerable<Car> Cars
        {
            get;
            set;
        }

        public IEnumerable<SelectListItem> ParkingItems
        {
            get;
            set;
        }

        public string SelectedCar
        {
            get;
            set;
        }

        public string SelectedParking
        {
            get;
            set;
        }



        public string Photo
        {
            get;
            set;
        }

        public Dictionary<int, string> Parkings { get; set; }

        public HomeViewModel()
        {
            Parkings = new Dictionary<int, string>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
