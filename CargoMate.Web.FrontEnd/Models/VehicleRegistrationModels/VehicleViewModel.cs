using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoMate.Web.FrontEnd.Models.VehicleRegistrationModels
{
    public class VehicleViewModel
    {
        public BasicInformation BasicInformation { get; set; }

        public VehicleScan VehicleScan { get; set; }

        public InsuranceInformation InsuranceInformation { get; set; }

    }

    public class BasicInformation
    {

        public BasicInformation()
        {
            VehicleCapacities = new List<SelectListItem>();
            VehicleConfigurations = new List<SelectListItem>();
            VehicleTypes = new List<SelectListItem>();
            VehicleMakes = new List<SelectListItem>();
            VehicleModels = new List<SelectListItem>();
            VehicleModelYears = new List<SelectListItem>();
            TripTypes = new MultiSelectList(string.Empty, string.Empty);
            PayLoadTypes = new MultiSelectList(string.Empty, string.Empty);
        }

        public long VehicleId { get; set; }

        public long VehicleTypeId { get; set; }

        public List<SelectListItem> VehicleTypes { get; set; }

        public long VehicleMakeId { get; set; }

        public List<SelectListItem> VehicleMakes { get; set; }

        public long VehicleConfigurationId { get; set; }

        public List<SelectListItem> VehicleConfigurations { get; set; }

        public long VehicleModelId { get; set; }

        public List<SelectListItem> VehicleModels { get; set; }

        public long VehicleCapacityId { get; set; }

        public List<SelectListItem> VehicleCapacities { get; set; }

        public long VehicleModelYearId { get; set; }

        public List<SelectListItem> VehicleModelYears { get; set; }

        public int?[] TripTypeIds { get; set; }

        public MultiSelectList TripTypes { get; set; }

        public int?[] PayLoadTypeIds { get; set; }

        public MultiSelectList PayLoadTypes { get; set; }

    }

    public class VehicleScan
    {
        public long VehicleId { get; set; }

        public string PlateNumber { get; set; }

        public string EngineNumber { get; set; }

        public string RegistrationNumber { get; set; }

        public string RegistrationImage { get; set; }

        public DateTime? RegistrationExpiry { get; set; }
    }


    public class InsuranceInformation
    {
        public long VehicleId { get; set; }

        public long InsuranceCOmpanyId { get; set; }

        public decimal? InsuranceAmount { get; set; }

        public string PolicyNumber { get; set; }

        public DateTime? InsuranceExpirey { get; set; }

    }
}