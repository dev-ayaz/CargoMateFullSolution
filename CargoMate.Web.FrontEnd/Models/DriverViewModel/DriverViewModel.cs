using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.DriverViewModel
{
    public class DriverViewModel
    {
        public  DriverPersonalInfoFormModel driverPersonalInfoFormModel { get; set; }

        public DriverLegalDocumentsFormModel driverLegalDocumentsFormModel { get; set; }
    }
}