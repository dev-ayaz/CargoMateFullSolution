using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Areas.Transporters.Models
{
    public class DriverLegalDocumentsDisplayModel
    {
        public string Id { get; set; }

        public string LicenseNumber { get; set; }

        public string LicenseExpiryDate { get; set; }

        public string LicenseImage { get; set; }

        public string ResidenceNumber { get; set; }

        public string ResidenceExpiryDate { get; set; }

        public string ResidenceImage { get; set; }
    }
}