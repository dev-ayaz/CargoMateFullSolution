using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData.Localizations
{

    [Table("[BasicData.Localization].LocalizedVehicleConfiguration")]
    public class LocalizedVehicleConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public string Descreption { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public long? VehicleConfigurationId { get; set; }

        public virtual VehicleConfiguration VehicleConfiguration { get; set; }
    }
}
