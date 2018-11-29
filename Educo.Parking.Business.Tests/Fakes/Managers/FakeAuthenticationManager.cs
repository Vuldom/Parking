using System;
using System.Collections.Generic;
using System.Text;
using Educo.Parking.Data;
using Microsoft.AspNetCore.Identity;

namespace Educo.Parking.Business.Tests.Fakes.Managers
{
    internal class FakeAuthenticationManager: AuthenticationManager
    {
        internal FakeAuthenticationManager(DataContextFactory factory, UserManager<ApplicationUser> userManager, User[] users) : base(factory, userManager)
        {
            foreach (User user in users)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = user.Username;
                applicationUser.Email = user.Email;
                userManager.CreateAsync(applicationUser, user.Password);
            }
        }
    }
}
