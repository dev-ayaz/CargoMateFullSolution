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
    [Table("BasicData.DriverStatuses")]
    public class DriverStatus
    {

        public DriverStatus()
        {
            LocalizedDriverStatuses = new HashSet<Localizations.LocalizedDriverStatus>();
            DriverPersonalInfos = new HashSet<DriverPersonalInfo>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<DriverPersonalInfo> DriverPersonalInfos { get; set; }
        public virtual ICollection<Localizations.LocalizedDriverStatus> LocalizedDriverStatuses { get; set; }

    }
}
