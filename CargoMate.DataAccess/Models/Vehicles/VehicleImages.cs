using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.Vehicles
{
    [Table("Vehicles.VehicleImages")]
    public class VehicleImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long? VehicleId { get; set; }

        public string ImageUrl { get; set; }

        public string ImageTitle { get; set; }

        public System.DateTime? CreationDate { get; set; }

        public bool IsActive { get; set; }

        public virtual Vehicle Vehicle { get; set; }

    }
}
