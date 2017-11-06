using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Areas.Transporters.Models
{
    public class DriverPersonalInfoDisplayModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string LegalName { get; set; }

        public string DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string ImageUrl { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string Rating { get; set; }

        public string TotalTrips { get; set; }

        public string FixedRate { get; set; }

        public string IdVerified { get; set; }

        public string MembershipDate { get; set; }

        public string IsValidated { get; set; }

        public bool IsPhoneNumberVerified { get; set; }

        public string Status { get; set; }

        public string Locality { get; set; }

        public string SubLocality { get; set; }
    }
}