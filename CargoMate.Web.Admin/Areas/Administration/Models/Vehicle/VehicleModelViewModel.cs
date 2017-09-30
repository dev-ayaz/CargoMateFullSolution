using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoMate.Web.Admin.Areas.Administration.Models.Vehicle
{
    public class VehicleModelViewModel
    {
        public VehicleModel VehicleModel { get; set; }
        public List<VehicleModel> VehicleModelsList { get; set; } 
    }

    public class VehicleModel
    {
        
        public long Id { get; set; }


        public string Name { get; set; }

        public string Make { get; set; }


        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Vehicle Make")]
        [Display(Name = "Make")]
        public long? MakeId { get; set; }

        public List<SelectListItem> MakeList { get; set; }


    }
}