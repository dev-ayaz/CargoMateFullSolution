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

        public decimal? Weight { get; set; }

        public decimal? Length { get; set; }

        public long? MaximumQuantity { get; set; }

        public decimal? Breadth { get; set; }

        public decimal? Height { get; set; }

        public decimal? MaxQuantity { get; set; }

        public long? VehicleTypeId { get; set; }

        public long? WeightUnitId { get; set; }

        public long? LengthUnitId { get; set; }

        public long? UOMId { get; set; }

        public bool? IsActive { get; set; }

        public virtual UOM UOM { get; set; }

        public virtual WeightUnit WeightUnit { get; set; }

        public virtual LengthUnit LengthUnit { get; set; }

        public virtual VehicleType VehicleType { get; set; }

        public virtual ICollection<Localizations.LocalizedVehicleCapacity> LocalizedVehicleCapacities { get; set; }
    }
}
