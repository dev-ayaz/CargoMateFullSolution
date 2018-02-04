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
    [Table("Transporters.InsuranceCompanies")]
    public class InsuranceCompany
    {
        public InsuranceCompany()
        {
            LocalizedInsuranceCompanies = new HashSet<Localization.LocalizedInsuranceCompany>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(20)]
        public string FaxNumber { get; set; }

        [StringLength(50)]
        public string WebSite { get; set; }

        public long? CountryId { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        public DbGeography Location { get; set; }

        public bool? IsActive { get; set; }

        public virtual BasicData.Country Country { get; set; }

        public virtual ICollection<Localization.LocalizedInsuranceCompany> LocalizedInsuranceCompanies { get; set; }
    }
}
