using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{
    [Table("BasicData.VehicleCapacities")]
    public class VehicleCapacity
    {

        public VehicleCapacity()
        {
            LocalizedVehicleCapacities = new HashSet<Localizations.LocalizedVehicleCapacity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long? Capacity { get; set; }

        public long? Length { get; set; }

        public long? PalletNumber { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public long? VehicleTypeId { get; set; }

        [StringLength(250)]
        public string Source { get; set; }

        public bool? IsActive { get; set; }

        public virtual VehicleType VehicleType { get; set; }

        public virtual ICollection<Localizations.LocalizedVehicleCapacity> LocalizedVehicleCapacities { get; set; }
    }
}
