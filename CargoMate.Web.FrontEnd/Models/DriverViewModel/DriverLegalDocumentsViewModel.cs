using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.DriverViewModel
{
    public class DriverLegalDocumentsViewModel
    {
        
    }

    public class DriverLegalDocumentsFormModel
    {
        [Required]
        public string Id { get; set; }

        public string LicenseNumber { get; set; }

        public DateTime? LicenseExpiryDate { get; set; }

        public string LicenseImage { get; set; }

        public string ResidenceNumber { get; set; }

        public DateTime? ResidenceExpiryDate { get; set; }

        public string ResidenceImage { get; set; }
    }

    public class DriverLegalDocumentsDisplayModel
    {
        public string Id { get; set; }

        public string LicenseNumber { get; set; }

        public DateTime? LicenseExpiryDate { get; set; }

        public string LicenseImage { get; set; }

        public string ResidenceNumber { get; set; }

        public DateTime? ResidenceExpiryDate { get; set; }

        public string ResidenceImage { get; set; }
    }
}