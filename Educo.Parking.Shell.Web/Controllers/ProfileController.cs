using Educo.Parking.Business;
using Educo.Parking.Data;
using Educo.Parking.Shell.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using Educo.Parking.Business.Managers;
using Microsoft.AspNetCore.Authorization;

namespace Educo.Parking.Shell.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IdentityProfileManager profileManager;
        private DataContextFactory contextFactory;
        private IHostingEnvironment environment;
        public ProfileController(IdentityProfileManager profileManager, DataContextFactory factory, IHostingEnvironment environment)
        {
            this.profileManager = profileManager;
            contextFactory = factory;
            this.environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> AccountSettings()
        {
            AccountSettingsViewModel accountSettingsViewModel = new AccountSettingsViewModel();
            User user = await profileManager.GetUserAsync(User.Identity.Name);
            accountSettingsViewModel.FirstName = user.FirstName;
            accountSettingsViewModel.LastName = user.LastName;
            accountSettingsViewModel.Email = user.Email;
            if (user.UserPhoto != null)
            {
                accountSettingsViewModel.Photo = user.UserPhoto;

            }
            else accountSettingsViewModel.Photo = "Error";

            return View(accountSettingsViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Update(AccountSettingsViewModel model, IFormFile Photo)
        {
            User user = new User();
            string ph = (await profileManager.GetUserAsync(User.Identity.Name)).UserPhoto;
            if (ph != null) {
                model.Photo = ph;
            }

            if (Photo == null || Photo.Length == 0)
            {
                user.Username = User.Identity.Name;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserPhoto = model.Photo;
                await profileManager.UserUpdateAsync(user);

                return RedirectToAction("AccountSettings", "Profile");
            }

            if (Photo.ContentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) < 0)
            {
                user.Username = User.Identity.Name;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserPhoto = model.Photo;
                await profileManager.UserUpdateAsync(user);

                return RedirectToAction("AccountSettings", "Profile");
            }

            string imageFolder = "UsersImages";
            string fileName = User.Identity.Name + ".jpg";
            string pathRoot = environment.WebRootPath;
            string targetFolder = pathRoot + "\\images\\" + imageFolder + "\\";
            string targetFile = targetFolder + fileName;
            using (var stream = new FileStream(targetFile, FileMode.Create))
            {

               Photo.CopyTo(stream);

            }

            user.Username = User.Identity.Name;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;          
            user.UserPhoto = Path.GetRelativePath(pathRoot, targetFile);
            await profileManager.UserUpdateAsync(user);

            return RedirectToAction("AccountSettings", "Profile");

        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(AccountSettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Password = model.Password;
                if ((await profileManager.ChangePasswordAsync(User.Identity.Name, model.Password, model.NewPassword)).Succeeded == false)
                {
                    TempData["repeatpass"] = "change password failed";
                }

                return RedirectToAction("AccountSettings", "Profile");
            }
            else
            {
                TempData["repeatpass"] = "Please, repeat old password correctly";

                return RedirectToAction("AccountSettings", "Profile");
            }
        }

    }
}
