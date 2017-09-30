using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{
    [Table("BasicData.VehicleModel")]
    public class VehicleModel
    {

        public VehicleModel()
        {
            LocalizedVehicleModels = new HashSet<Localizations.LocalizedVehicleModel>();
            ModelYearCombinations = new HashSet<ModelYearCombination>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(500)]
        public string ImageURL { get; set; }

        public long? VehicleMakeId { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }

        public virtual ICollection<Localizations.LocalizedVehicleModel> LocalizedVehicleModels { get; set; }

        public virtual ICollection<ModelYearCombination> ModelYearCombinations { get; set; }
    }
}
