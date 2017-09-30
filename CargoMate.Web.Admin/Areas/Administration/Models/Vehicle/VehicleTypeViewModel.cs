using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CargoMate.Web.Admin.Areas.Administration.Models.Vehicle
{
    public class VehicleTypeViewModel
    {

        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Descreption")]
        public string Descreption { get; set; }

        public string ImageUrl { get; set; }

        public bool IsEquipment { get; set; }


    }
}