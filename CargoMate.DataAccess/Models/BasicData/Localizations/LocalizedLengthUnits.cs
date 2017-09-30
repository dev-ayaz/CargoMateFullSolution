using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData.Localizations
{
    [Table("[BasicData.Localization].LocalizedLengthUnits")]
    public class LocalizedLengthUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(100)]
        public string ShortName { get; set; }

        [StringLength(500)]
        public string FullName { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public long? LengthUnitId { get; set; }

        public virtual LengthUnit LengthUnit { get; set; }
    }
}
