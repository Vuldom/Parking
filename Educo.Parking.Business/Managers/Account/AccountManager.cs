using Educo.Parking.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educo.Parking.Business.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountManager(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public AccountManager()
        {

        }


        internal string GetPassword()
        {
            Random rnd = new Random();
            StringBuilder password = new StringBuilder();

            for (int i = 0; i < 5; i++)
            {
                password.Append((char)rnd.Next(33, 123));
            }

            return password.ToString();
        }

        public async Task<string> PasswordRecovery(string userName)
        {
            string password = GetPassword();

            ApplicationUser userEntity = await userManager.FindByNameAsync(userName);

            if (userEntity != null)
            {
                string code = await userManager.GeneratePasswordResetTokenAsync(userEntity);
                IdentityResult result = await userManager.ResetPasswordAsync(userEntity, code, password);
                if (!result.Succeeded)
                {
                    throw new ApplicationException("Не удалось изменить пароль");
                }
            }

            return password;
        }

        public Task<SignInResult> LoginAsync(string userName, string password, bool rememberMe, bool lockoutOnFailure)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
