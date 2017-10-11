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
        public long Id { get; set; }

        [StringLength(50)]
        public string PlateNumber { get; set; }

        [StringLength(50)]
        public string EngineNumber { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        public string RegistrationImage { get; set; }

        public DateTime? RegistrationExpiry { get; set; }

        public bool? IsInsured { get; set; }

        public long? CapacityId { get; set; }

        public long? ConfigurationId { get; set; }

        public long Year { get; set; }

        public long? DriverId { get; set; }

        public long? CountryId { get; set; }

        public bool? IsVerified { get; set; }

        public int? Status { get; set; }

        public bool? IsActive { get; set; }
    }
}
