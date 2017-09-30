using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{
    [Table("BasicData.LengthUnits")]
    public class LengthUnit
    {

        public LengthUnit()
        {
            LocalizedLengthUnits = new HashSet<Localizations.LocalizedLengthUnit>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool? IsMetric { get; set; }

        public decimal? LengthMultiple { get; set; }

        public virtual ICollection<Localizations.LocalizedLengthUnit> LocalizedLengthUnits { get; set; }
    }
}
