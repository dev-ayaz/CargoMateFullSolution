using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData
{
    [Table("BasicData.PayLoadTypes")]
    public class PayLoadType
    {

        public PayLoadType()
        {
            LocalizedPayLoadTypes = new HashSet<Localizations.LocalizedPayLoadType>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long? VehicleTypeId { get; set; }

        public string ImageUrl { get; set; }

        public bool? IsActive { get; set; }

        public virtual VehicleType VehicleType { get; set; }

        public virtual ICollection<Localizations.LocalizedPayLoadType> LocalizedPayLoadTypes { get; set; }
    }
}
