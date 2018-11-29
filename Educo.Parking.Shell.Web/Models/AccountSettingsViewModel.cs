using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Educo.Parking.Business;

namespace Educo.Parking.Shell.Web.Models
{
    public class AccountSettingsViewModel
    {
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public List<Roles> Roles { get; set; }

        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [Display(Name = "Repeat Password")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }

        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        public string Photo { get; set; }



    }


}
