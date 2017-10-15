using CargoMate.DataAccess.Models.BasicData;
using CargoMate.DataAccess.Models.Transporters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Models.Vehicles
{
    [Table("Vehicles.Vehicles")]
    public class Vehicle
    {
        public Vehicle()
        {
            VehicleTripTypes = new HashSet<VehicleTripType>();
            VehiclePayloadTypes = new HashSet<VehiclePayloadType>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long? ModelYearCombinationId { get; set; }

        public virtual ModelYearCombination ModelYearCombination { get; set; }

        public long? VehicleConfigurationId { get; set; }

        public virtual VehicleConfiguration VehicleConfiguration { get; set; }

        public long? VehicleCapacityId { get; set; }

        public virtual VehicleCapacity VehicleCapacity { get; set; }

        [StringLength(50)]
        public string PlateNumber { get; set; }

        [StringLength(50)]
        public string EngineNumber { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        public string RegistrationImage { get; set; }

        public DateTime? RegistrationExpiry { get; set; }

        public bool? IsInsured { get; set; }

        public long? InsuranceCompanyId { get; set; }

        public virtual InsuranceCompany InsuranceCompany { get; set; }

        public decimal? InsuranceAmount { get; set; }

        public string PolicyNumber { get; set; }

        public DateTime? InsuranceExpirey { get; set; }

        public string DriverPersonalInfoId { get; set; }

        public virtual DriverPersonalInfo DriverPersonalInfo { get; set; }

        public bool? IsVerified { get; set; }

        public int? Status { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<VehicleTripType> VehicleTripTypes { get; set; }

        public virtual ICollection<VehiclePayloadType> VehiclePayloadTypes { get; set; }

        public virtual ICollection<VehicleImage> VehicleImages { get; set; }

    }
}
