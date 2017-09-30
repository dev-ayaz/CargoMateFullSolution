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
    [Table("BasicData.Countries")]
    public class Country
    {



        public Country()
        {
            VehicleMakes = new HashSet<VehicleMake>();
            LocalizedCountries = new HashSet<Localizations.LocalizedCountry>();
            Companies = new HashSet<Customers.Company>();
            PreferredAddresses = new HashSet<PreferredAddress>();
            DriverPersonalInfos = new HashSet<DriverPersonalInfo>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(50)]
        public string CurrencySymbol { get; set; }

        [StringLength(10)]
        public string PhonCode { get; set; }

        public string Flag { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<VehicleMake> VehicleMakes { get; set; }
        public virtual ICollection<Localizations.LocalizedCountry> LocalizedCountries { get; set; }
        public virtual ICollection<Customers.Company> Companies { get; set; }
        public virtual ICollection<PreferredAddress> PreferredAddresses { get; set; }
        public virtual ICollection<DriverPersonalInfo> DriverPersonalInfos { get; set; }
    }
}
