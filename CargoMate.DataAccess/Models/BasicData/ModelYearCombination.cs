using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{

    [Table("BasicData.ModelYearCombinations")]
    public class ModelYearCombination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long? VehicleModelId { get; set; }

        public long Year { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public virtual VehicleModel VehicleModel { get; set; }
    }
}
