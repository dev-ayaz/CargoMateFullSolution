using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleViewModel
{
    public class VehicleTypeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsEquipment { get; set; }
    }

    public class VehicleTypeCompositeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsEquipment { get; set; }

        public List<ConfigurationsViewModel> ConfigurationsViewModel { get; set; }
        public List<CapacityViewModel> CapacityViewModel { get; set; }
    }
}