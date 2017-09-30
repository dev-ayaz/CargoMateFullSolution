using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CargoMate.Web.Admin.Areas.Administration.Models.Vehicle
{
    public class VehicleTypeConfigurationsViewModel
    {
        public VehicleTypeConfigurationsViewModel()
        {
            VehicleTypesListItems = new List<SelectListItem>();
        }

        public List<SelectListItem> VehicleTypesListItems { get; set; } 

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Descreption { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Vehicle Type")]
        public string VehicleTypeName { get; set; }

        [Display(Name = "Vehicle Type")]
        public long TypeId { get; set; }

    }
    public class VehicleTypeConfigurationListModel
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Descreption { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Vehicle Type")]
        public string VehicleTypeName { get; set; }

        [Display(Name = "Vehicle Type")]
        public long TypeId { get; set; }

    }
}