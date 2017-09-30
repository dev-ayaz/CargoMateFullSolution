using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.Transporters.Localization
{
    [Table("[Transporters.Localization].LocalizedInsuranceCompanies")]
    public class LocalizedInsuranceCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(20)]
        public string CultureCode { get; set; }

        public long? InsuranceCompanyId { get; set; }

        public virtual InsuranceCompany InsuranceCompany { get; set; }
    }
}
