using CargoMate.Web.FrontEnd.Models.VehicleRegistrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Shared;
using CargoMate.DataAccess.Models.Vehicles;

namespace CargoMate.Web.FrontEnd.Controllers
{
    public class VehicleController : BaseController
    {
        public VehicleController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: Vehicle
        public ActionResult Index()
        {
            var vehicle = new VehicleViewModel
            {

                BasicInformation = new BasicInformation
                {
                    VehicleTypes = UnitOfWork.VehicleTypes.GetAll().Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name
                    }).ToList(),
                    TripTypes = new MultiSelectList(UnitOfWork.LocalizedTripTypes.GetWhere(lc => lc.CultureCode == SessionHandler.CultureCode).ToList(), "TripTypeId", "Name")
                },
                InsuranceInformation = new InsuranceInformation(),
                VehicleScan = new VehicleScan()
            };
            return View(vehicle);
        }

        public JsonResult Addvehicle(BasicInformation basicInfo)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }

            UnitOfWork.Vehicles.Insert(new Vehicle
            {
                DriverPersonalInfoId = SessionHandler.UserId,
                IsActive=false,
                IsVerified=false,
                ModelYearCombinationId=basicInfo.VehicleModelYearId,
                VehicleCapacityId=basicInfo.VehicleCapacityId,
                VehicleConfigurationId = basicInfo.VehicleConfigurationId,
                VehiclePayloadTypes = basicInfo.PayLoadTypeIds?.Select(p=>new VehiclePayloadType { PayLoadTypeId=p.Value}).ToList(),
                VehicleTripTypes = basicInfo.TripTypeIds?.Select(p => new VehicleTripType { TripTypeId = p.Value }).ToList()
            });
            return
    Json(UnitOfWork.Commit()>0
        ? CargoMateMessages.SuccessResponse
        : CargoMateMessages.FailureResponse);

        }

        public JsonResult MakeAutoComplete(long vehicletypeId)
        {
            var vehicleMakes = UnitOfWork.VehicleMakes.GetWhere(m => m.VehicleTypeId == vehicletypeId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.LocalizedVehicleMakes.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name
            }).ToList();

            return Json(vehicleMakes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModelsAutoComplete(long makeId)
        {
            var models = UnitOfWork.VehicleModels.GetWhere(c => c.VehicleMakeId == makeId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.LocalizedVehicleModels.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name
            }).ToList();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModelYearAutoComplete(long modelId)
        {
            var years = UnitOfWork.ModelYearCombinations.GetWhere(c => c.VehicleModelId == modelId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Year.ToString()
            }).ToList();

            return Json(years, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VehicleConfigurationsAutoComplete(long vehicletypeId)
        {
            var configrations = UnitOfWork.VehicleConfigurations.GetWhere(c => c.VehicleTypeId == vehicletypeId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name
            }).ToList();

            return Json(configrations, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VehicleCapacitiesAutoComplete(long vehicletypeId)
        {
            var capacities = UnitOfWork.VehicleCapacities.GetWhere(c => c.VehicleTypeId == vehicletypeId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name
            }).ToList();

            return Json(capacities, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PayloadTypesAutoComplete(long vehicletypeId)
        {
            var capacities = UnitOfWork.PayLoadTypes.GetWhere(c => c.VehicleTypeId == vehicletypeId).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.LocalizedPayLoadTypes.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name
            }).ToList();

            return Json(capacities, JsonRequestBehavior.AllowGet);
        }
    }
}