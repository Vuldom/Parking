using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Educo.Parking.Data
{
    [Table("Cars")]
    public class CarEntity
    {
        [Column("IdCar"), Required,Key]
        public int IdCar { get; set; }

        [Column("StateNumber"), Required, MaxLength(8)]
        public string StateNumber { get; set; }

        [Column("Manufacturer"), MaxLength(25)]
        public string Manufacturer { get; set; }

        [Column("Model"), MaxLength(20)]
        public string Model { get; set; }

        [Column("Color"), MaxLength(15)]
        public string Color { get; set; }

        [Column("Year")]
        public int Year { get; set; }

        [ForeignKey("IdCar")]
        public ICollection<UsersHaveCars> UsersHaveCars { get; set; }

        [ForeignKey("IdCar")]
        public ICollection<ParkingHistoryEntity> History{ get; set; }

    }
}
