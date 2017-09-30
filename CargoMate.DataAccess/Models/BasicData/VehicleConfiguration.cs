using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{
    [Table("BasicData.VehicleConfigurations")]
    public class VehicleConfiguration
    {

        public VehicleConfiguration()
        {
            LocalizedVehicleConfigurations = new HashSet<Localizations.LocalizedVehicleConfiguration>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string ImageUrl { get; set; }

        public long? VehicleTypeId { get; set; }

        [StringLength(50)]
        public string Source { get; set; }

        public bool? IsActive { get; set; }

        public virtual VehicleType VehicleType { get; set; }

        public virtual ICollection<Localizations.LocalizedVehicleConfiguration> LocalizedVehicleConfigurations { get; set; }
    }
}
