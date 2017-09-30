using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData.Localizations
{
    [Table("[BasicData.Localization].LocalizedVehicleTypes")]
    public class LocalizedVehicleType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public string Descreption { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public long? VehicleTypeId { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
