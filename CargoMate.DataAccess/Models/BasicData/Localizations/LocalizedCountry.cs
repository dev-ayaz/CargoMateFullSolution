using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.BasicData.Localizations
{
    [Table("[BasicData.Localization].LocalizedCountries")]
    public class LocalizedCountry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(50)]
        public string CountryCode { get; set; }

        [StringLength(50)]
        public string CurrencyLong { get; set; }

        [StringLength(50)]
        public string CurrencyCode { get; set; }

        public long? CountryId { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public virtual Country Country { get; set; }
    }
}
