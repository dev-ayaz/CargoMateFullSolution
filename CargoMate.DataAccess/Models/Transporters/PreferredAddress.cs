using CargoMate.DataAccess.Models.BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.Transporters
{
    [Table("Transporters.PreferredAddresses")]
    public partial class PreferredAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long CountryId { get; set; }

        [StringLength(50)]
        public string Locality { get; set; }

        [StringLength(50)]
        public string SubLocality { get; set; }

        public DbGeography Location { get; set; }

        public long? DriverPersonalInfoId { get; set; }

        public virtual Country Country { get; set; }

        public virtual DriverPersonalInfo DriverPersonalInfo { get;set;}
    }
}
