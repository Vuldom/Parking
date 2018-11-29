using Educo.Parking.Business;
using Educo.Parking.Business.Managers.Profile;
using Educo.Parking.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Educo.Parking.Business.Managers
{
    public class IdentityProfileManager : IIdentityProfileManager
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityProfileManager(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userName, string currentPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(currentPassword)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(newPassword)) throw new ArgumentNullException();

            ApplicationUser applicationUser = await userManager.FindByNameAsync(userName);

            if (applicationUser == null)
            {
                throw new ApplicationException($"Не удалось загрузить пользователя с именем: {userName}");
            }

            return await userManager.ChangePasswordAsync(applicationUser, currentPassword, newPassword);
        }

        public async Task<User> GetUserAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException();

            ApplicationUser applicationUser = await userManager.FindByNameAsync(userName);

            if (applicationUser == null)
            {
                throw new ApplicationException($"Не удалось загрузить пользователя с именем: {userName}");
            }

            User user = new User
            {
                Email = applicationUser.Email,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.Lastname,
                Username = applicationUser.UserName,
                UserPhoto = applicationUser.UserPhoto
            };

            return user;
        }

        public Task<User> GetUserAsync(ClaimsPrincipal principal)
        {
            string username = principal.FindFirstValue(ClaimTypes.Name);
            return GetUserAsync(username);
        }

        public async Task<IdentityResult> UserUpdateAsync(User user)
        {
            if (user == null) throw new ArgumentNullException();

            ApplicationUser applicationUser = await userManager.FindByNameAsync(user.Username);

            if (applicationUser == null)
            {
                throw new ApplicationException($"Не удалось загрузить пользователя с именем: {user.Username}");
            }

            applicationUser.Email = user.Email;
            applicationUser.FirstName = user.FirstName;
            applicationUser.Lastname = user.LastName;
            applicationUser.UserPhoto = user.UserPhoto;

            return await userManager.UpdateAsync(applicationUser);
        }

      
    }
}
