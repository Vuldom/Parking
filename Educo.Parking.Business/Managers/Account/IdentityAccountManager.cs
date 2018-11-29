using Educo.Parking.Business.Managers;
using Educo.Parking.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educo.Parking.Business.Managers
{
    public class IdentityAccountManager : IAccountManager
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public IdentityAccountManager(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<SignInResult> LoginAsync(string userName, string password, bool rememberMe, bool lockoutOnFailure)
        {
            SignInResult result = await signInManager.PasswordSignInAsync(userName, password, rememberMe, lockoutOnFailure: false);

            return result;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public Task<string> PasswordRecovery(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
