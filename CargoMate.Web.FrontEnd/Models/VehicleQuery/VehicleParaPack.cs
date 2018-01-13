using CargoMate.Web.FrontEnd.Models.VehicleViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleQuery
{
    public class VehicleParaPack
    {
        public VehicleTypeViewModel vehicleType { get; set; }
        public ConfigurationsViewModel vehicleConfig { get; set; }
        public CapacityViewModel vehicleCapacity { get; set; }
    }
}