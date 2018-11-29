using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Educo.Parking.Business;
using Educo.Parking.Business.Managers;
using Educo.Parking.Shell.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Educo.Parking.Shell.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private CarManager carManager;
        private IParkingManager parkingManager;
        private readonly IdentityProfileManager identityProfileManager;

        public HomeController(CarManager carManager, IParkingManager parkingManager, IdentityProfileManager identityProfileManager)
        {
            this.carManager = carManager;
            this.parkingManager = parkingManager;
            this.identityProfileManager = identityProfileManager;            
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string username = User.FindFirstValue(ClaimTypes.Name);
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Photo = "photo";
            homeViewModel.FirstName = (await identityProfileManager.GetUserAsync(User.Identity.Name)).FirstName;
            homeViewModel.Cars = carManager.GetParkedCars(User.Identity.Name);
            homeViewModel.CarItems = homeViewModel.Cars.Select(c => new SelectListItem { Text = c.StateNumber, Value = c.StateNumber });
            homeViewModel.ParkingItems = parkingManager.GetParkings().Select(p => new SelectListItem { Text = p.ParkingName, Value = p.ParkingName });
            foreach (Car car in homeViewModel.Cars)
            {
                homeViewModel.Parkings.Add(car.Id, await parkingManager.GetParkingNameByCarId(car.Id));
            }

            return View(homeViewModel);
        }

        [HttpGet]
        public JsonResult FindNearestParking(double lat, double lon)
        {
            double minDistance=1000000;
            double latitude=0;
            double longitude=0;
            string nameparking=null;
            double R = 6.371;       //Earth's radius
            List<Business.Parking> parkings= parkingManager.GetParkings();
            foreach (Business.Parking parking in parkings)
            {
                double dLat = (parking.Latitude-lat);
                double dLon = (parking.Longitude-lon);

                double a = Math.Sin(dLat / 2) * Math.Sin(dLat) + Math.Cos(parking.Latitude) * Math.Cos(lat) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                double d = R * c;
                if (d < minDistance)
                {
                    minDistance = d;
                    latitude = parking.Latitude;
                    longitude = parking.Longitude;
                    nameparking = parking.ParkingName;
                }
            }
            
            return Json(new {latitude, longitude, nameparking});
        }

        [HttpGet]
        public JsonResult FindParkingCoordinates(string name)
        {
            Business.Parking parking = parkingManager.GetParkings().Single(p=>p.ParkingName==name);
            return Json(new { parking.Latitude, parking.Longitude, parking.ParkingName });
        }

        [HttpGet]
        public IActionResult OrderCar(string SelectedCar, string SelectedParking)
        {
            Car car = carManager.GetCar(SelectedCar);
            parkingManager.Depart(car);
            TempData["SuccessMessage"] = "Your order in process, you can take your car "+ SelectedCar+" on "+ SelectedParking+ " Parking";
            return RedirectToAction("Index", "Home");          
        }

        
    }
}
