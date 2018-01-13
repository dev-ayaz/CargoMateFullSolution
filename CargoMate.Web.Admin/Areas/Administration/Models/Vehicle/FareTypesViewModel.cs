using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Areas.Administration.Models.Vehicle
{
    public class FareTypesViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }


    }
}