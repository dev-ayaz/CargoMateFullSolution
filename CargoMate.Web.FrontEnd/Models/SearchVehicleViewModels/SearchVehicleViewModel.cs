using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoMate.Web.FrontEnd.Models.SearchVehicleViewModels
{

    public class SearchVehicleViewModel
    {
       // [Required(ErrorMessage = "Please Enter PickUp Address")]
        [Display(Name = "PickUp Address")]
        public string PickUpAddress { get; set; }

        //[Required(ErrorMessage = "Please Enter DropUp Address")]
        [Display(Name = "DropUp Address")]
        public string DropUpAddress { get; set; }

        public string PickUpLocation { get; set; }

        public string DropUpLocation { get; set; }

        public DateTime? Date { get; set; }

        public long? CapacityId { get; set; }

        [Required(ErrorMessage = "Please Select Vehicle Type")]
        public long? VehicleTypeId { get; set; }

        public List<SelectListItem> VehicleCapacities { get; set; }

        public List<SelectListItem> VehicleTypes { get; set; }
    }
}
