using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{

    [Table("BasicData.VehicleTypes")]
    public class VehicleType
    {
        public VehicleType()
        {
            PayLoadTypes = new HashSet<PayLoadType>();
            VehicleCapacities = new HashSet<VehicleCapacity>();
            VehicleTypeConfigurations = new HashSet<VehicleConfiguration>();
            LocalizedVehicleTypes = new HashSet<Localizations.LocalizedVehicleType>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string ImageUrl { get; set; }

        public bool? IsEquipment { get; set; }

        [StringLength(250)]
        public string Source { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<PayLoadType> PayLoadTypes { get; set; }

        public virtual ICollection<VehicleCapacity> VehicleCapacities { get; set; }

        public virtual ICollection<VehicleConfiguration> VehicleTypeConfigurations { get; set; }

        public virtual ICollection<Localizations.LocalizedVehicleType> LocalizedVehicleTypes { get; set; }
    }
}
