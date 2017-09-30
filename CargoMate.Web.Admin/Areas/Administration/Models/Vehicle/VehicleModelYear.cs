using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoMate.Web.Admin.Areas.Administration.Models.Vehicle
{

    public class VehicleModelYearViewModel
    {
        public VehicleModelYear VehicleModelYear { get; set; }

        public List<VehicleModelYear> VehicleModelYearList { get; set; } 
    }
    public class VehicleModelYear
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Please select Model")]
        [Display(Name = "Model")]
        public long? ModelId { get; set; }

        [Required(ErrorMessage = "Please select Year")]
        [Display(Name = "Year")]
        public long? YearId { get; set; }
        public string Year { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public List<SelectListItem> ModelList { get; set; }
        public List<SelectListItem> YearsList { get; set; }
    }
}