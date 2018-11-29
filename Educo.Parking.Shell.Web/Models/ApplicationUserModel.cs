using Educo.Parking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educo.Parking.Shell.Web.Models
{
    public class ApplicationUserModel
    {
        public ApplicationUser User { get; set; }
        public bool IsAdmin { get; set; }

    }
}
