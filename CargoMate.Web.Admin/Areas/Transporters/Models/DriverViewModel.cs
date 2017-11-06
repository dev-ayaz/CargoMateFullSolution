using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Areas.Transporters.Models
{
    public class DriverViewModel
    {

        public DriverPersonalInfoDisplayModel DriverPersonalInfoDisplayModel { get; set; }

        public DriverLegalDocumentsDisplayModel DriverLegalDocumentsDisplayModel { get; set; }

        public List<DriverVehiclesDisplayModel> DriverVehicleDisplayModel { get; set; }

    }
}