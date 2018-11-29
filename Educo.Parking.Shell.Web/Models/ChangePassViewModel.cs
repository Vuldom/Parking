using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educo.Parking.Shell.Web.Models
{
    public class ChangePassViewModel
    {
        
        public string OldPassword { get; set; }
        public string RepeatPassword { get; set; }
        public string NewPassword { get; set; }

    }
}
