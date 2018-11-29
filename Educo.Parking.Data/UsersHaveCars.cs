using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Educo.Parking.Data
{
    [Table("UsersHaveCars")]
    public class UsersHaveCars
    {
        [Column("IdUser"), Required,Key]
        public string IdUser { get; set; }

        [Column("IdCar"), Required,Key]
        public int IdCar { get; set; }

        [ForeignKey("IdCar")]
        public CarEntity CarEntity { get; set; }

        [ForeignKey("IdUser")]
        public ApplicationUser UserEntity { get; set; }
        
    }
}
