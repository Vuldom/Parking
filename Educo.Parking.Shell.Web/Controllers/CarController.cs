using Educo.Parking.Business;
using Educo.Parking.Business.Managers;
using Educo.Parking.Shell.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educo.Parking.Shell.Web.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        public CarController(CarManager carManager,IParkingManager parkingManager)
        {
            this.carManager = carManager;
            this.parkingManager=parkingManager;
        }

        private CarManager carManager;
        private IParkingManager parkingManager;

        [HttpGet]
        public IActionResult CarSettings()
        {
            
            CarSettingsModel carSettingsModel = new CarSettingsModel();
            carSettingsModel.Cars = carManager.GetCars(User.Identity.Name);
            return View(carSettingsModel);
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            CarViewModel addCarViewModel = new CarViewModel();
            return View(addCarViewModel);
        }

        [HttpGet]
        public IActionResult EditCar(string statenumber)
        {
            ViewData["StateNumber"] = statenumber;
            CarViewModel carViewModel = new CarViewModel();
            Car car = carManager.GetCar(statenumber);
            carViewModel.Color = car.Color;
            carViewModel.Make = car.Manufacturer;
            carViewModel.Model = car.Model;
            carViewModel.StateNumber = car.StateNumber;
            carViewModel.Year = car.Year;
            return View(carViewModel);
        }

        [HttpPost]
        public IActionResult SaveCar(CarViewModel addCarViewModel)
        {
            Car car = new Car();
            car.Color = addCarViewModel.Color;
            car.Manufacturer = addCarViewModel.Make;
            car.Model = addCarViewModel.Model;
            car.StateNumber = addCarViewModel.StateNumber;
            car.Year = addCarViewModel.Year.Value;
            carManager.AddCar(car, User.Identity.Name);
            return RedirectToAction("CarSettings");
        }

        [HttpPost]
        public IActionResult UpdateCar(CarViewModel updateCarViewModel)
        {
            Car car = carManager.GetCar(updateCarViewModel.StateNumber);
            car.Color = updateCarViewModel.Color;
            car.Manufacturer = updateCarViewModel.Make;
            car.Model = updateCarViewModel.Model;
            car.Year = updateCarViewModel.Year.Value;
            carManager.CarUpdate(car);
            return RedirectToAction("CarSettings");
        }

        [HttpGet]
        public IActionResult RemoveCar(string statenumber)
        {
            Car car = carManager.GetCar(statenumber);
            carManager.RemoveCar(car);
            return RedirectToAction("CarSettings");
        }

        [HttpGet]
        public IActionResult ArriveCar(string number)
        {
            string name=null;
            Random random = new Random();
            int idParking = random.Next(1, 4);
            switch (idParking)
            {
                case 1:
                    name = "Petersburg";
                    break;
                case 2:
                    name = "Moscow";
                    break;
                case 3:
                    name = "Brest";
                    break;
                case 4:
                    name = "Mohilev";
                    break;
            }
            Business.Parking parking = parkingManager.GetParkings().Single(p => p.ParkingName == name);
            Car car = carManager.GetCar(number);
            string username = User.Identity.Name;
            parkingManager.Arrive(car, parking,username);
            TempData["ParkMessage"] = "Your car " + number + " has been parked at " + parking.ParkingName + " Parking";
            return RedirectToAction("CarSettings","Car");
        }

    }
}
