using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Educo.Parking.Shell.Web.Models
{
    public class CarViewModel
    {
        
        [Display(Name = "State Number")]
        public string StateNumber { get; set; }
        [Display(Name = "Make")]
        public string Make { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Year")]
        public int? Year { get; set; }
        [Display(Name = "Color")]
        public string Color { get; set; }
    }
}
