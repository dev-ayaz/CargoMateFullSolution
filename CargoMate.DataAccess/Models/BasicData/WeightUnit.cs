using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{
    [Table("BasicData.WeightUnits")]
    public class WeightUnit
    {

        public WeightUnit()
        {
            LocalizedWeightUnits = new HashSet<Localizations.LocalizedWeightUnit>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool? IsMetric { get; set; }

        public decimal? WeightMultiple { get; set; }

        public virtual ICollection<Localizations.LocalizedWeightUnit> LocalizedWeightUnits { get; set; }
    }
}
