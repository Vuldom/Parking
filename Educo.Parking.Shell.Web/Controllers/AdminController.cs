using Educo.Parking.Business;
using Educo.Parking.Business.Managers;
using Educo.Parking.Data;
using Educo.Parking.Shell.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educo.Parking.Shell.Web.Controllers
{
    [Authorize]
    public class AdminController:Controller 
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IdentityProfileManager identityProfileManager;

        public AdminController(UserManager<ApplicationUser> userManager, IdentityProfileManager identityProfileManager)
        {
            this.userManager = userManager;
            this.identityProfileManager = identityProfileManager;
        }

        [HttpGet]
        public async Task<IActionResult> Role()
        {
            RoleSettingsModel roleSettingsModel = new RoleSettingsModel();
            var users = userManager.Users;

            foreach(var user in users)
            {
                ApplicationUserModel applicationUserModel = new ApplicationUserModel();
                applicationUserModel.User = user;
                bool isAdmin = await userManager.IsInRoleAsync(user, Roles.Administrator.ToString());
                applicationUserModel.IsAdmin = isAdmin;
                roleSettingsModel.UsersWithRoles.Add(applicationUserModel);
            }          
            return View(roleSettingsModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveRoles(string[] userId)
        {
            var users = userManager.Users;
            foreach(var user in users)
            {
                await userManager.RemoveFromRoleAsync(user, Roles.Administrator.ToString());
            }
            foreach (var id in userId)
            {
                ApplicationUser applicationUser = await userManager.FindByIdAsync(id);
                await userManager.AddToRoleAsync(applicationUser, Roles.Administrator.ToString());
            }

            return RedirectToAction("Role","Admin");
        }

    }
}
