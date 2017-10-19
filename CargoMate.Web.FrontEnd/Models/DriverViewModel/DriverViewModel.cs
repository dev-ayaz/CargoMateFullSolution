using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CargoMate.Web.FrontEnd.Models.VehicleRegistrationModels;

namespace CargoMate.Web.FrontEnd.Models.DriverViewModel
{
    public class DriverViewModel
    {
        public  DriverPersonalInfoFormModel DriverPersonalInfoFormModel { get; set; }

        public DriverLegalDocumentsFormModel DriverLegalDocumentsFormModel { get; set; }

        public List<VehicleDisplayModel> VehicleDisplayModels { get; set; }
    }
}