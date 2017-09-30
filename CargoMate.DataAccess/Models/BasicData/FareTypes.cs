using CargoMate.DataAccess.Models.BasicData.Localizations;
using CargoMate.DataAccess.Models.Transporters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{
    [Table("BasicData.FareTypes")]
    public class FareType
    {
        public FareType()
        {
            LocalizedFareTypes = new HashSet<LocalizedFareType>();
            DriverFareTypes = new HashSet<DriverFareType>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<LocalizedFareType> LocalizedFareTypes { get; set; }

        public virtual ICollection<DriverFareType> DriverFareTypes { get; set; }
        
    }
}
