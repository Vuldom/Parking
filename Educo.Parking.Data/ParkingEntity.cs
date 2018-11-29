using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Educo.Parking.Data
{
    [Table("Parkings")]
    public class ParkingEntity
    {
        [Column("IdParking"), Required,Key]
        public int IdParking { get; set; }
        [Column("Name"), Required]
        public string Name { get; set; }
        [Column("Latitude")]
        public double Latitude { get; set; }
        [Column("Longitude")]
        public double Longitude { get; set; }

        [ForeignKey("IdParking")]
        public ICollection<ParkingHistoryEntity> History{ get; set; }

    }
}
