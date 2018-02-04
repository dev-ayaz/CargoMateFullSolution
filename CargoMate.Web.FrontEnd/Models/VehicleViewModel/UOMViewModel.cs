using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleViewModel
{
    public class UOMViewModel
    {
        public long? Id { get; set; }

        public double? Factor { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }
    }
}