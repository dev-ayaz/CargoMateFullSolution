using CargoMate.Web.FrontEnd.Models.DriverViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleViewModel
{
    public class VehicleviewModels
    {
    }

    public class VehicleSearchResult
    {
        public string PlateNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public string EngineNumber { get; set; }
        public DateTime? RegistrationExpiry { get; set; }
        public string RegistrationImage { get; set; }
        public List<string> VehicleImages { get; set; }
        public bool IsActive { get; set; }
        public bool Validated { get; set; }
        public List<string> TripTypes { get; set; }
        public bool isInsured { get; set; }
        public decimal? InsuranceAmount { get; set; }
        public string InsuranceCompany { get; set; }
        public DateTime? InsuranceExpiry { get; set; }
        public long? InsurancePolicyNo { get; set; }


        //TODO implement accident severities...
        //TODO totalRating implementation


        public DriverPersonalInfoDisplayModel DriverPersonalInfo { get; set; }

        public DriverLegalDocumentsDisplayModel DriverLegalDocuments { get; set; }
        //Here we have the driver details....

        public MakeViewModel Make { get; set; }
        public VehicleModelViewModel Model { get; set; }
        public long? Year { get; set; }

        public VehicleTypeViewModel VehicleType { get; set; }

        public CapacityViewModel Capacity { get; set; }
        public ConfigurationsViewModel Configurations { get; set; }

        public List<PayLoadTypeViewModel> PayloadTypes { get; set; }
    }
}