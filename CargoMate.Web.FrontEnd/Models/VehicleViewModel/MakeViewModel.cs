﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleViewModel
{
    public class MakeViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string CountryName { get; set; }

        public string ImageUrl { get; set; }
    }
}