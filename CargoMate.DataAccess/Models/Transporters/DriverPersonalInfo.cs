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
    [Table("Transporters.DriverPersonalInfos")]
    public class DriverPersonalInfo
    {
        public DriverPersonalInfo()
        {
            PreferredAddresses = new HashSet<PreferredAddress>();
            DriverFareTypes = new HashSet<DriverFareType>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DriverId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string LegalName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string EmailAddress { get; set; }

        public string ImageUrl { get; set; }

        public long? CountryId { get; set; }

        public bool? Gender { get; set; }

        public bool? IsPhoneNumberVerified { get; set; }

        public decimal? Rating { get; set; }

        public long? TotalTrips { get; set; }

        public bool? FixedRate { get; set; }

        public bool? IdVerified { get; set; }

        public DateTime? MembershipDate { get; set; }

        public bool? IsValidated { get; set; }

        public DbGeography Location { get; set; }

        public string Locality { get; set; }

        public string SubLocality { get; set; }

        public long? DriverStatusId { get; set; }

        public virtual DriverLegalDocument DriverLegalDocument { get; set; }

        public virtual DriverStatus DriverStatus { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<PreferredAddress> PreferredAddresses { get; set; }

        public virtual ICollection<DriverFareType> DriverFareTypes { get; set; }

    }
}
