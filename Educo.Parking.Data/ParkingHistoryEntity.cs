using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Educo.Parking.Data
{
    [Table("ParkingHistory")]
    public class ParkingHistoryEntity
    {
        [Column("IdHistory"), Required,Key]
        public int IdHistory { get; set; }

        [Column("IdParking"), Required]
        public int IdParking { get; set; }

        [Column("IdUser"), Required]
        public string IdUser { get; set; }

        [Column("IdCar"), Required]
        public int IdCar { get; set; }

        [Column("Arrival"), Required]
        public DateTime Arrival { get; set; }

        [Column("Departure")]
        public DateTime? Departure { get; set; }

        [ForeignKey("IdCar")]
        public CarEntity Car { get; set; }

        [ForeignKey("IdUser")]
        public ApplicationUser User { get; set; }

        [ForeignKey("IdParking")]
        public ParkingEntity Parking { get; set; }



    }
}
