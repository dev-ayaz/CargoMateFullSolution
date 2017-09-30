using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.DriverViewModel
{
    public class DriverPersonalInfoViewModel
    {
    }

    public class DriverPersonalInfoFormModel
    {
        public long Id { get; set; }

        [Required]
        public string DriverId { get; set; }

        public string Name { get; set; }

        public string LegalName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string ImageUrl { get; set; }

        public long? CountryId { get; set; }

        public bool? Gender { get; set; }

        public bool? FixedRate { get; set; }

        public string Location { get; set; }

        public long? DriverStatusId { get; set; }

        public string Locality { get; set; }

        public string SubLocality { get; set; }

        public long[] FairTypeIds { get; set; }
    }

    public class DriverPersonalInfoDisplayModel
    {
        public long Id { get; set; }

        public string DriverId { get; set; }

        public string Name { get; set; }

        public string LegalName { get; set; }

        public string DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string ImageUrl { get; set; }

        public string CountryName { get; set; }

        public bool? Gender { get; set; }

        public bool? FixedRate { get; set; }

        public string Location { get; set; }

        public string Locality { get; set; }

        public string SubLocality { get; set; }

        public string DriverStatus { get; set; }
    }
}