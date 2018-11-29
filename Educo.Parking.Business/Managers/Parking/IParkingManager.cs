using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Educo.Parking.Business.Managers
{
    public interface IParkingManager
    {
        Parking Arrive(Car car, Parking parking,string username);
        Parking Depart(Car car);
        Task<string> GetParkingNameByCarId(int carId);
        List<Parking> GetParkings();
    }
}
