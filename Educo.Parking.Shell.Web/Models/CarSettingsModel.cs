using Educo.Parking.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educo.Parking.Shell.Web.Models
{
    public class CarSettingsModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public string CurrentCar { get; set; }
        

    }
}
