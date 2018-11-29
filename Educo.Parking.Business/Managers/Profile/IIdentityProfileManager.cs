using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Educo.Parking.Data;
using Microsoft.AspNetCore.Identity;

namespace Educo.Parking.Business.Managers.Profile
{
    public interface IIdentityProfileManager
    {
        Task<IdentityResult> UserUpdateAsync(User user);
        Task<IdentityResult> ChangePasswordAsync(string userName, string currentPassword, string newPassword);
        Task<User> GetUserAsync(string username);
      
    }
}
