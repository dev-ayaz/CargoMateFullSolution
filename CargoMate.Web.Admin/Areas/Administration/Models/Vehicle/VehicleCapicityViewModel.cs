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

            WeightUnitsListItems = new List<SelectListItem>();

            LengthUnitsListItems = new List<SelectListItem>();

            UOMListItems = new List<SelectListItem>();
        }
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Weight")]
        public decimal?  Weight { get; set; }

        [Required(ErrorMessage = "Please enter length")]
        public decimal? Length { get; set; }

        [Required(ErrorMessage = "Please enter Height")]
        public decimal? Height { get; set; }

        [Required(ErrorMessage = "Please enter Breadth")]
        public decimal? Breadth { get; set; }

        [Required(ErrorMessage = "Please enter Maximum Quantity")]
        public long? MaximumQuantity { get; set; }

        public string VehicleType { get; set; }

        [Required(ErrorMessage = "Please select vehicle type")]
        [Display(Name= "VehicleType")]
        public long? VehicleTypeId { get; set; }

        [Display(Name = "Weight Unit")]
        public long? WeightUnitId { get; set; }

        [Display(Name = "Length Unit")]
        public long? LengthUnitId { get; set; }

        [Display(Name = "UOM")]
        public long? UOMId { get; set; }

        public List<SelectListItem> VehicleTypesListItems { get; set; }

        public List<SelectListItem> WeightUnitsListItems { get; set; }

        public List<SelectListItem> LengthUnitsListItems { get; set; }

        public List<SelectListItem> UOMListItems { get; set; }
    }

    public class VehicleCapacityListModel
    {


        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Length { get; set; }
        public long? MaximumQuantity { get; set; }
        public string VehicleType { get; set; }
    }
}