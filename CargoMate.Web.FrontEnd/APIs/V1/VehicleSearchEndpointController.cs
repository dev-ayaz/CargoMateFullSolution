using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Models.VehicleViewModel;
using CargoMate.Web.FrontEnd.Models.VehicleRegistrationModels;
using CargoMate.DataAccess.Models.Vehicles;
using CargoMate.Web.FrontEnd.Shared;

namespace CargoMate.Web.FrontEnd.APIs.V1
{
    public class VehicleSearchEndpointController : BaseController
    {
        public VehicleSearchEndpointController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        [HttpGet]
        public List<VehicleSearchResult> VehicleSearchResult()
        {
            return new List<VehicleSearchResult>();
        }

        public HttpResponseMessage Addvehicle(BasicInformation basicInfo)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }

            var vehicle = UnitOfWork.Vehicles.Insert(new Vehicle
            {
                DriverPersonalInfoId = basicInfo.DriverId,
                IsActive = false,
                IsVerified = false,
                ModelYearCombinationId = basicInfo.VehicleModelYearId,
                VehicleCapacityId = basicInfo.VehicleCapacityId,
                VehicleConfigurationId = basicInfo.VehicleConfigurationId,
                VehiclePayloadTypes = basicInfo.PayLoadTypeIds?.Select(p => new VehiclePayloadType { PayLoadTypeId = p.Value }).ToList(),
                VehicleTripTypes = basicInfo.TripTypeIds?.Select(p => new VehicleTripType { TripTypeId = p.Value }).ToList()
            });
            var result = UnitOfWork.Commit();
            return Request.CreateResponse(HttpStatusCode.OK, new {vehicleId = vehicle.Id });

        }

        public HttpResponseMessage AddvehicleScan(VehicleScan vehicleScan)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }

            var vehicle = UnitOfWork.Vehicles.GetWhere(v => v.Id == vehicleScan.VehicleId).FirstOrDefault();

            if (vehicle == null)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, "Vehicle Not Found");
            }

            vehicle.RegistrationExpiry = vehicleScan.RegistrationExpiry;
            vehicle.RegistrationImage = CargoMateImageHandler.SaveImageFromBase64(vehicleScan.RegistrationImage, GlobalProperties.VehicleImagesFolder);
            vehicle.RegistrationNumber = vehicleScan.RegistrationNumber;
            vehicle.EngineNumber = vehicleScan.EngineNumber;
            vehicle.PlateNumber = vehicleScan.PlateNumber;
            UnitOfWork.Vehicles.Update(vehicle);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage AddvehicleInsurance(InsuranceInformation insuranceInformation)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }

            long? VehicleId = insuranceInformation.VehicleId > 0 ? insuranceInformation.VehicleId : SessionHandler.VehicleId;
            var vehicle = UnitOfWork.Vehicles.GetWhere(v => v.Id == VehicleId).FirstOrDefault();

            if (vehicle == null)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, "Vehicle No Found");
            }

            vehicle.InsuranceCompanyId = insuranceInformation.InsuranceCompanyId;
            vehicle.InsuranceAmount = insuranceInformation.InsuranceAmount;
            vehicle.InsuranceExpirey = insuranceInformation.InsuranceExpirey;
            vehicle.PolicyNumber = insuranceInformation.PolicyNumber;
            vehicle.IsInsured = true;
            UnitOfWork.Vehicles.Update(vehicle);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
