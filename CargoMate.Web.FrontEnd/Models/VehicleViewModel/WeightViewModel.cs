using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleViewModel
{
    public class WeightViewModel
    {
        public long Id { get; set; }

        public bool? IsMetric { get; set; }

        public decimal? WeightMultiple { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }
    }
}