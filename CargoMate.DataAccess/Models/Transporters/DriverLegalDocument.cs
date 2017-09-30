using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.Transporters
{
    [Table("Transporters.DriverLegalDocuments")]
    public class DriverLegalDocument
    {
        [Key, ForeignKey("DriverPersonalInfo")]
        public long Id { get; set; }

        [StringLength(50)]
        public string LicenseNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LicenseExpiryDate { get; set; }

        public string LicenseImage { get; set; }

        [StringLength(50)]
        public string ResidenceNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ResidenceExpiryDate { get; set; }

        public string ResidenceImage { get; set; }

        public virtual DriverPersonalInfo DriverPersonalInfo { get; set; }
    }
}
