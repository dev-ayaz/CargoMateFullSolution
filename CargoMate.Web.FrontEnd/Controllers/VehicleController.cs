﻿using CargoMate.Web.FrontEnd.Models.VehicleRegistrationModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Shared;
using CargoMate.DataAccess.Models.Vehicles;
using CargoMate.DataAccess.Models.BasicData.Localizations;

namespace CargoMate.Web.FrontEnd.Controllers
{
    public class VehicleController : BaseController
    {
        public VehicleController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: Vehicle
        public ActionResult Index(long? vehicleId)
        {
            if (vehicleId.HasValue)
            {
                var editvehicl = UnitOfWork.Vehicles.GetWhere(v => v.Id == vehicleId.Value, "ModelYearCombination.VehicleModel.VehicleMake").ToList().Select(
                    v => new VehicleViewModel
                    {
                        BasicInformation = new BasicInformation
                        {
                            VehicleId = v.Id,
                            PayLoadTypeIds = v.VehiclePayloadTypes.Select(p => p.PayLoadTypeId).ToArray(),
                            TripTypeIds = v.VehicleTripTypes.Select(t => t.TripTypeId).ToArray(),
                            VehicleCapacityId = v.VehicleCapacityId,
                            VehicleConfigurationId = v.VehicleConfigurationId,
                            VehicleMakeId = v.ModelYearCombination.VehicleModel.VehicleMakeId,
                            VehicleModelId = v.ModelYearCombination.VehicleModelId,
                            VehicleModelYearId = v.ModelYearCombination.Id,
                            VehicleTypeId = v.VehicleTypeId,
                            VehicleTypes = GetVehicleTypes(),
                            TripTypes = new MultiSelectList(GetVehicleTripTypes(), "TripTypeId", "Name"),

                            VehicleMakes = GetVehicleMakes(),

                            PayLoadTypes = new MultiSelectList(UnitOfWork.PayLoadTypes.GetWhere(c => c.VehicleTypeId == v.VehicleTypeId).Select(c => new SelectListItem
                            {
                                Value = c.Id.ToString(),
                                Text = c.LocalizedPayLoadTypes.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name
                            }).ToList(), "Value", "Text"),

                            VehicleCapacities = UnitOfWork.VehicleCapacities.GetWhere(c => c.VehicleTypeId == v.VehicleTypeId).Select(c => new SelectListItem
                            {
                                Value = c.Id.ToString(),
                                Text = c.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name
                            }).ToList(),
                            VehicleConfigurations = UnitOfWork.VehicleConfigurations.GetWhere(c => c.VehicleTypeId == v.VehicleTypeId).Select(c => new SelectListItem
                            {
                                Value = c.Id.ToString(),
                                Text = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name
                            }).ToList(),
                            VehicleModels = UnitOfWork.VehicleModels.GetWhere(c => c.VehicleMakeId == v.ModelYearCombination.VehicleModel.VehicleMakeId).Select(c => new SelectListItem
                            {
                                Value = c.Id.ToString(),
                                Text = c.LocalizedVehicleModels.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name
                            }).ToList(),
                            VehicleModelYears = UnitOfWork.ModelYearCombinations.GetWhere(c => c.VehicleModelId == v.ModelYearCombination.VehicleModelId).Select(c => new SelectListItem
                            {
                                Value = c.Id.ToString(),
                                Text = c.Year.ToString()
                            }).ToList()
                        },
                        VehicleScan = new VehicleScan
                        {
                            VehicleId = v.Id,
                            EngineNumber = v.EngineNumber,
                            PlateNumber = v.PlateNumber,
                            RegistrationExpiry = v.RegistrationExpiry,
                            RegistrationImage = v.RegistrationImage,
                            RegistrationNumber = v.RegistrationNumber
                        },
                        InsuranceInformation = new InsuranceInformation
                        {
                            VehicleId = v.Id,
                            InsuranceAmount = v.InsuranceAmount,
                            InsuranceCompanyId = v.InsuranceCompanyId,
                            InsuranceExpirey = v.InsuranceExpirey,
                            PolicyNumber = v.PolicyNumber
                        }
                    }).FirstOrDefault();

                SessionHandler.VehicleId = vehicleId;

                return View(editvehicl);
            }


            var vehicle = new VehicleViewModel
            {

                BasicInformation = new BasicInformation
                {
                    VehicleTypes = GetVehicleTypes(),
                    VehicleMakes = GetVehicleMakes(),
                    TripTypes = new MultiSelectList(GetVehicleTripTypes(), "TripTypeId", "Name")
                },
                InsuranceInformation = new InsuranceInformation(),
                VehicleScan = new VehicleScan()
            };
            return View(vehicle);
        }

        private List<SelectListItem> GetVehicleTypes()
        {
            return UnitOfWork.VehicleTypes.GetAll().Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name
            }).ToList();
        }

        private List<SelectListItem> GetVehicleMakes()
        {
            return UnitOfWork.LocalizedVehicleMakes.GetWhere(lt => lt.CultureCode == SessionHandler.CultureCode).Select(t => new SelectListItem
            {
                Value = t.MakeId.ToString(),
                Text = t.Name
            }).ToList();
        }

        private IEnumerable<LocalizedTripType> GetVehicleTripTypes()
        {
            return UnitOfWork.LocalizedTripTypes.GetWhere(lc => lc.CultureCode == SessionHandler.CultureCode).ToList();
        }

        public JsonResult Addvehicle(BasicInformation basicInfo)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }

            if (basicInfo.VehicleId.HasValue)
            {
                var editVehicle = UnitOfWork.Vehicles.GetWhere(v => v.Id == basicInfo.VehicleId).FirstOrDefault();
                if (editVehicle != null)
                {
                    editVehicle.DriverPersonalInfoId = SessionHandler.UserId;
                    editVehicle.ModelYearCombinationId = basicInfo.VehicleModelYearId;
                    editVehicle.VehicleCapacityId = basicInfo.VehicleCapacityId;
                    editVehicle.VehicleConfigurationId = basicInfo.VehicleConfigurationId;
                    editVehicle.VehicleTypeId = basicInfo.VehicleTypeId;
                    editVehicle.VehiclePayloadTypes = basicInfo.PayLoadTypeIds?.Select(p => new VehiclePayloadType { PayLoadTypeId = p.Value }).ToList();
                    editVehicle.VehicleTripTypes = basicInfo.TripTypeIds?.Select(p => new VehicleTripType { TripTypeId = p.Value }).ToList();
                }

                UnitOfWork.Vehicles.Update(editVehicle);
                return Json(UnitOfWork.Commit() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);

            }
            var vehicle = UnitOfWork.Vehicles.Insert(new Vehicle
            {
                DriverPersonalInfoId = SessionHandler.UserId,
                IsActive = false,
                IsVerified = false,
                ModelYearCombinationId = basicInfo.VehicleModelYearId,
                VehicleCapacityId = basicInfo.VehicleCapacityId,
                VehicleConfigurationId = basicInfo.VehicleConfigurationId,
                VehicleTypeId = basicInfo.VehicleTypeId,
                VehiclePayloadTypes = basicInfo.PayLoadTypeIds?.Select(p => new VehiclePayloadType { PayLoadTypeId = p.Value }).ToList(),
                VehicleTripTypes = basicInfo.TripTypeIds?.Select(p => new VehicleTripType { TripTypeId = p.Value }).ToList()
            });
            var result = UnitOfWork.Commit();
            SessionHandler.VehicleId = vehicle.Id;
            return Json(result > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);

        }

        public JsonResult AddvehicleScan(VehicleScan vehicleScan)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }

            long? VehicleId = vehicleScan.VehicleId > 0 ? vehicleScan.VehicleId : SessionHandler.VehicleId;
            var vehicle = UnitOfWork.Vehicles.GetWhere(v => v.Id == VehicleId).FirstOrDefault();

            if (vehicle == null)
            {
                return Json(CargoMateMessages.FailureResponse);
            }

            vehicle.RegistrationExpiry = vehicleScan.RegistrationExpiry;
            vehicle.RegistrationImage = CargoMateImageHandler.SaveImageFromBase64(vehicleScan.RegistrationImage, GlobalProperties.VehicleImagesFolder);
            vehicle.RegistrationNumber = vehicleScan.RegistrationNumber;
            vehicle.EngineNumber = vehicleScan.EngineNumber;
            vehicle.PlateNumber = vehicleScan.PlateNumber;
            UnitOfWork.Vehicles.Update(vehicle);
            return Json(UnitOfWork.Commit() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);
        }

        public JsonResult AddvehicleInsurance(InsuranceInformation insuranceInformation)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }

            long? VehicleId = insuranceInformation.VehicleId > 0 ? insuranceInformation.VehicleId : SessionHandler.VehicleId;
            var vehicle = UnitOfWork.Vehicles.GetWhere(v => v.Id == VehicleId).FirstOrDefault();

            if (vehicle == null)
            {
                return Json(CargoMateMessages.FailureResponse);
            }

            vehicle.InsuranceCompanyId = insuranceInformation.InsuranceCompanyId;
            vehicle.InsuranceAmount = insuranceInformation.InsuranceAmount;
            vehicle.InsuranceExpirey = insuranceInformation.InsuranceExpirey;
            vehicle.PolicyNumber = insuranceInformation.PolicyNumber;
            vehicle.IsInsured = true;
            UnitOfWork.Vehicles.Update(vehicle);
            return Json(UnitOfWork.Commit() > 0 ? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);
        }


        public JsonResult MakeAutoComplete()
        {
            var vehicleMakes = UnitOfWork.VehicleMakes.GetAll().Select(c => new SelectListItem
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

        public JsonResult VehicleImagesAdd()
        {
            var isSavedSuccessfully = true;
            var filename = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null)
                    {
                        filename = file.FileName;
                        if (file.ContentLength > 0)
                        {

                            var originalDirectory = new DirectoryInfo($"{Server.MapPath(@"\")}SystemImages\\VehicleImages");

                            //string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                            var isExists = System.IO.Directory.Exists(originalDirectory.ToString());

                            if (!isExists)
                            {
                                System.IO.Directory.CreateDirectory(originalDirectory.ToString());
                            }


                            if (SessionHandler.VehicleId != null)
                            {
                                var path = $"{originalDirectory}\\{"VehicleImage" + SessionHandler.VehicleId.Value + file.FileName}";

                                file.SaveAs(path);

                                var vehicleImage = new VehicleImage
                                {
                                    VehicleId = SessionHandler.VehicleId.Value,
                                    ImageUrl = "VehicleImage" + SessionHandler.VehicleId.Value + file.FileName,
                                    CreationDate = DateTime.Now.Date,
                                    ImageTitle = filename.Split('.')[0],
                                    IsActive = true
                                };
                                UnitOfWork.VehicleImages.Insert(vehicleImage);
                            }
                        }
                    }
                }
                isSavedSuccessfully = UnitOfWork.Commit() > 0;

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            return Json(isSavedSuccessfully ? new { Message = filename } : new { Message = "Error in saving file" });
        }

        [HttpGet]
        public JsonResult VehicleImagesGet()
        {
            var images = UnitOfWork.VehicleImages.GetWhere(i => i.VehicleId == SessionHandler.VehicleId.Value).Select(c => c.ImageUrl).ToList();
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult VehicleImagesDelete(string name)
        {
            var picList = Directory.GetFiles($"{Server.MapPath(@"\")}SystemImages\\VehicleImages", name);
            foreach (var file in picList)
            {
                System.IO.File.Delete(file);
            }
            var image = UnitOfWork.VehicleImages.GetWhere(i => i.ImageUrl == name && i.VehicleId == SessionHandler.VehicleId.Value).FirstOrDefault();
            UnitOfWork.VehicleImages.Delete(image);

            UnitOfWork.Commit();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CompaniesDropDownSource(string term)
        {
            var companies = UnitOfWork.LocalizedInsuranceCompanies.GetWhere(c => c.Name.Contains(term))
                .Select(c => new
                {
                    id = c.InsuranceCompanyId,
                    Name = c.Name
                }).Take(10)
                .ToList();

            return Json(companies, JsonRequestBehavior.AllowGet);
        }
    }
}