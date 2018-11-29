using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Educo.Parking.Business
{
    public class User
    {
        public User()
        {
            Roles = new List<Roles>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public List<Roles> Roles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserPhoto { get; set; }
    }
}
