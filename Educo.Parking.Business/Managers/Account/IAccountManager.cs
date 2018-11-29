using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Educo.Parking.Business.Managers
{
    public interface IAccountManager
    {
        Task<string> PasswordRecovery(string userName);
        Task<SignInResult> LoginAsync(string userName, string password, bool rememberMe, bool lockoutOnFailure);
        Task LogoutAsync();
    }
}
