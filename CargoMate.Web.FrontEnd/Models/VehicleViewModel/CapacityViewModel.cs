using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleViewModel
{
    public class CapacityViewModel
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Length { get; set; }

        public decimal? Height { get; set; }

        public decimal? Breadth { get; set; }

        public long? MaximumQuantity { get; set; }

        public UOMViewModel BaseUOM { get; set; }

        public LengthViewModel LengthUnit { get; set; }

        public WeightViewModel WeightUnit { get; set; }
    }
}