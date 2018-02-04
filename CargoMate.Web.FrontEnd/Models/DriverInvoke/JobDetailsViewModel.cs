using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoMate.Web.FrontEnd.Models.DriverInvoke
{
    public class JobDetailsViewModel
    {
        [Required]
        public decimal CargoWeight { get; set; }


        public List<SelectListItem> VehicleConfigurations { get; set; }

        public List<SelectListItem> VehicleCapacities { get; set; }

        public List<SelectListItem> WeightUnits { get; set; }
    }
}