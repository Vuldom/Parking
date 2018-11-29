using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Educo.Parking.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string UserPhoto { get; set; }

        [ForeignKey("IdUser")]
        public ICollection<UsersHaveCars> UsersHaveCars { get; set; }
        [ForeignKey("IdUser")]
        public ICollection<ParkingHistoryEntity> History { get; set; }
    }

}
