using CargoMate.Web.Admin.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CargoMate.Web.Admin.Areas.Administration.Models.Vehicle
{
    public class MakeViewModel
    {
        public MakeModel MakeModel { get; set; }

        public List<MakeModel> MakeModelsList { get; set; } 
    }

    public class MakeModel
    {
        
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
 
        public string CountryName { get; set; }
        [Display(Name = "Country")]

        [Required]
        public long CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }
        
    }
}