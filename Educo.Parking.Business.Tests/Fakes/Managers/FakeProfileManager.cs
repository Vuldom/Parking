using Educo.Parking.Business.Managers;
using Educo.Parking.Data;
using Microsoft.AspNetCore.Identity;

namespace Educo.Parking.Business.Tests.Fakes.Managers
{
    internal class FakeProfileManager : IdentityProfileManager
    {
        internal FakeProfileManager(UserManager<ApplicationUser>  userManager, User[] users) : base (userManager)
        {
            foreach(User user in users)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = user.Username;
                applicationUser.Email = user.Email;
                userManager.CreateAsync(applicationUser, user.Password);
            }
        }
    }
}
