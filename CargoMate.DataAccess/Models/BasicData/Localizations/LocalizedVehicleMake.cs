using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData.Localizations
{
    [Table("[BasicData.Localization].LocalizedMakes")]
    public class LocalizedVehicleMake
    {
        public long Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public long? MakeId { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }
    }
}
