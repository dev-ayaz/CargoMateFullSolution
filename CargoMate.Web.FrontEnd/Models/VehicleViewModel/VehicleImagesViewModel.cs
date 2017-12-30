using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleViewModel
{
    public class VehicleImagesViewModel
    {
        [Required]
        public long? VehicleId { get; set; }

        public List<string> Images { get; set; }
    }
}