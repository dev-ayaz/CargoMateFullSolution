using CargoMate.DataAccess.Models.BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.Transporters
{
    [Table("Transporters.DriverFareTypes")]
    public class DriverFareType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long FareTypeId { get; set; }

        public long DriverPersonalInfoId { get; set; }

        public virtual DriverPersonalInfo DriverPersonalInfo { get; set; }

        public virtual FareType FareType { get; set; }

    }
}
