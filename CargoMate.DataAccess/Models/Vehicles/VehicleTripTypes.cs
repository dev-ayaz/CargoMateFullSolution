using CargoMate.DataAccess.Models.BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.Vehicles
{
    [Table("Vehicles.VehicleTripTypes")]
    public class VehicleTripType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long VehicleId { get; set; }

        public long TripTypeId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual TripType TripType { get; set; }
    }
}
