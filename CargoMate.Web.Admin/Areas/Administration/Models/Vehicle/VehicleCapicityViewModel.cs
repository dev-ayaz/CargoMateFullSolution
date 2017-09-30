using CargoMate.Web.Admin.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CargoMate.Web.Admin.Areas.Administration.Models.Vehicle
{
    public class VehicleCapacityViewModel
    {
        public VehicleCapacityViewModel()
        {
            VehicleTypesListItems = new List<SelectListItem>();
        }
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Capacity")]
        public long  Capacity { get; set; }

        [Required(ErrorMessage = "Please enter length")]
        public long Length { get; set; }

        [Required(ErrorMessage = "Please enter Pallet number")]
        public long PalletNumber { get; set; }

        public string VehicleType { get; set; }

        [Required(ErrorMessage = "Please select vehicle type")]
        public long VehicleTypeId { get; set; }
 
        public List<SelectListItem> VehicleTypesListItems { get; set; }
    }

    public class VehicleCapacityListModel
    {


        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public long Capacity { get; set; }
        public long Length { get; set; }
        public long PalletNumber { get; set; }
        public string VehicleType { get; set; }
    }
}