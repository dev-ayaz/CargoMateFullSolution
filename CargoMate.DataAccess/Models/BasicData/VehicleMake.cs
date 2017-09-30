using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{
    [Table("BasicData.VehicleMakes")]
    public class VehicleMake
    {

        public VehicleMake()
        {
            LocalizedVehicleMakes = new HashSet<Localizations.LocalizedVehicleMake>();
            VehicleModels = new HashSet<VehicleModel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long? CountryId { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public bool? IsActive { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Localizations.LocalizedVehicleMake> LocalizedVehicleMakes { get; set; }

        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
