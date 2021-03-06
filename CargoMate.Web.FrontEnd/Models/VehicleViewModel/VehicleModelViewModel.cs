﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleViewModel
{
    public class VehicleModelViewModel
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }

    public class VehicleModelCompositViewModel
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public List<VehicleModelYearViewModel> Years { get; set; }
    }
}