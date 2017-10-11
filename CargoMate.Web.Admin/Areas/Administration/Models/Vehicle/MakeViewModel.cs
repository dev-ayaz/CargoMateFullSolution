using CargoMate.Web.Admin.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CargoMate.Web.Admin.Areas.Administration.Models.Vehicle
{
    public class MakeViewModel
    {
        public MakeFormModel MakeFormModel { get; set; }

        public List<MakeDisplayModel> MakeDisplayModelList { get; set; } 
    }

    public class MakeDisplayModel
    {
        
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
 
        public string Country { get; set; }

        public string VehicleType { get; set; }

    }

    public class MakeFormModel
    {
        public MakeFormModel()
        {
            Countries = new List<SelectListItem>();
            VehicleTypes = new List<SelectListItem>();
        }

        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Country")]
        public long CountryId { get; set; }

        [Required]
        [Display(Name = "Vehicle Type")]
        public long? VehicleTypeId { get; set; }

        public List<SelectListItem> VehicleTypes { get; set; }

        public List<SelectListItem> Countries { get; set; }
    }
}