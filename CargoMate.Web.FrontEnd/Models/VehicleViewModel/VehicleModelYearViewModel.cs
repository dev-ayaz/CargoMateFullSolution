using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleViewModel
{
    public class VehicleModelYearViewModel
    {
        public long? Id { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public string ImageUrl { get; set; }

    }
}