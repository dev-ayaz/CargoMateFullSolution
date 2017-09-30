using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{
    [Table("BasicData.TripTypes")]
    public class TripType
    {
        public TripType()
        {
            LocalizedTripTypes = new HashSet<Localizations.LocalizedTripType>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<Localizations.LocalizedTripType> LocalizedTripTypes { get; set; }
    }
}
