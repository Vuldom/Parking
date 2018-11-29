using Educo.Parking.Business;
using Educo.Parking.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educo.Parking.Shell.Web.Models
{
    public class RoleSettingsModel
    {
        public RoleSettingsModel()
        {
            UsersWithRoles = new List<ApplicationUserModel>();
        }
        public List<ApplicationUserModel> UsersWithRoles { get; set; }
    }

}
