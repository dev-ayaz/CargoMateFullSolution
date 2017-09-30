using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleViewModel
{
    public class CapacityViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long? Capacity { get; set; }

        public long? Length { get; set; }

        public long? PalletNumber { get; set; }
    }
}