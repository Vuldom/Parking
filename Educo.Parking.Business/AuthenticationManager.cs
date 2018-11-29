using System;
using System.Linq;
using System.Threading.Tasks;
using Educo.Parking.Data;
using Microsoft.AspNetCore.Identity;

namespace Educo.Parking.Business
{
    public class AuthenticationManager
    {
        readonly DataContextFactory factory;
        private readonly UserManager<ApplicationUser> userManager;

        public AuthenticationManager(DataContextFactory factory, UserManager<ApplicationUser> userManager)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory), $"{nameof(factory)} is null.");
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddUser(string username, string lastname, string firstname, string pass, string email)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(username);
            if (string.IsNullOrWhiteSpace(lastname)) throw new ArgumentNullException(lastname);
            if (string.IsNullOrWhiteSpace(firstname)) throw new ArgumentNullException(firstname);
            if (string.IsNullOrWhiteSpace(pass)) throw new ArgumentNullException(pass);
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(email);

            ApplicationUser entity = new ApplicationUser { UserName = username, Lastname = lastname, FirstName = firstname, Email = email };
            IdentityResult result = await userManager.CreateAsync(entity, pass);

            return result;
        }


        public bool RemoveUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            using (ParkingDBContext context = factory.CreateDbContext())
            {
                ApplicationUser entity = context.Users.Single(us => us.UserName == user.Username);
                context.Users.Remove(entity);
                int number = context.SaveChanges();
                if (number != 1)
                {
                    throw new InvalidOperationException("Не удалось удалить из БД запись");
                }
                return true;
            }
        }
    }
}
