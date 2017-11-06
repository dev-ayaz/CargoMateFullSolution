using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Areas.Transporters.Models
{
    public class DriverVehiclesDisplayModel
    {
        public string Id { get; set; }

        public string RegisterationNumber { get; set; }

        public string PlateNumber { get; set; }

        public string RegisterationExpirey { get; set; }

        public string EngineNumber { get; set; }

        public string IsActive { get; set; }

        public string IsInsured { get; set; }
    }
}