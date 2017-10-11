using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.Admin.Shared;
using CargoMate.DataAccess.Models.BasicData;
using CargoMate.DataAccess.Models.BasicData.Localizations;
using CargoMate.Web.Admin.Areas.Administration.Models.Vehicle;

namespace CargoMate.Web.Admin.Areas.Administration.Controllers
{
    public class VehicleController : BaseController
    {
        public VehicleController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: Administration/Vehicle

        public ActionResult Index()
        {
            var vehicleModel = new VehicleViewModel
            {

                VehicleCapcitiesList = UnitOfWork.VehicleCapacities.GetAll(includeProperties: "VehicleType.LocalizedVehicleTypes,LocalizedCapacities").Select(c => new VehicleCapacityListModel
                {
                    Id = c.Id,
                    Name = c.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name,
                    Capacity = c.Capacity.Value,
                    Length = c.Length.Value,
                    PalletNumber = c.PalletNumber.Value,
                    VehicleType = c.VehicleType.LocalizedVehicleTypes.FirstOrDefault(t => t.CultureCode == SessionHandler.CultureCode).Name
                }).ToList(),

                VehicleTypesList = UnitOfWork.VehicleTypes.GetAll(includeProperties: "LocalizedVehicleTypes").Select(t => new VehicleTypeViewModel
                {
                    Id = t.Id,
                    Name = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name,
                    Descreption = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Descreption,
                    IsEquipment = t.IsEquipment.Value,
                    ImageUrl = t.ImageUrl
                }).ToList(),
                ConfigurationsList = UnitOfWork.VehicleConfigurations.GetAll(includeProperties: "LocalizedVehicleTypesConfigurations,VehicleType.LocalizedVehicleTypes").Select(c => new VehicleTypeConfigurationListModel
                {
                    Id = c.Id,
                    Name = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name,
                    Descreption = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Descreption,
                    ImageUrl = c.ImageUrl,
                    VehicleTypeName = c.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name
                }).ToList(),
                CapicityViewModel = new VehicleCapacityViewModel
                {

                    VehicleTypesListItems = UnitOfWork.VehicleTypes.GetAll(includeProperties: "LocalizedVehicleTypes").Select(t => new SelectListItem()
                    {
                        Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name,
                        Value = t.Id.ToString()
                    }).ToList()
                },
                ConfigurationViewModel = new VehicleTypeConfigurationsViewModel
                {
                    VehicleTypesListItems = UnitOfWork.VehicleTypes.GetAll(includeProperties: "LocalizedVehicleTypes").Select(t => new SelectListItem()
                    {
                        Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name,
                        Value = t.Id.ToString()
                    }).ToList(),

                }
            };
            return View(vehicleModel);
        }


        //*********************Vehicle Types *******************************//
        // Vehicle Types
        [HttpPost]
        public JsonResult AddVehicleType(VehicleTypeViewModel vehicleTypeForm)
        {

            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }

            var vehicleType = new VehicleType
            {
                ImageUrl = ImageUploader.SaveImageFromBase64(vehicleTypeForm.ImageUrl, SessionKeys.VehicleImagesPath),
                IsEquipment = vehicleTypeForm.IsEquipment,
                IsActive = true,
                LocalizedVehicleTypes = new List<LocalizedVehicleType>
                {
                     new LocalizedVehicleType
                    {
                         Name = vehicleTypeForm.Name,
                         Descreption = vehicleTypeForm.Descreption,
                         CultureCode = SessionHandler.CultureCode
                     }
                }
            };

            UnitOfWork.VehicleTypes.Insert(vehicleType);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
        }

        public JsonResult UpdateVehicleType(VehicleTypeViewModel vehicleTypeForm)
        {

            var savedVehicleType = UnitOfWork.VehicleTypes.GetWhere(t => t.Id == vehicleTypeForm.Id).FirstOrDefault();

            if (savedVehicleType == null)
            {
                return Json(AlertMessages.ModelError);
            }


            var localizedSavedVehicleType = savedVehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode);

            if (localizedSavedVehicleType != null)
            {
                localizedSavedVehicleType.Name = vehicleTypeForm.Name;
                localizedSavedVehicleType.Descreption = vehicleTypeForm.Descreption;
            }

            savedVehicleType.IsEquipment = vehicleTypeForm.IsEquipment;

            if (!string.IsNullOrEmpty(vehicleTypeForm.ImageUrl))
            {
                savedVehicleType.ImageUrl = ImageUploader.SaveImageFromBase64(vehicleTypeForm.ImageUrl, SessionKeys.VehicleImagesPath);
            }

            savedVehicleType.LocalizedVehicleTypes.Add(localizedSavedVehicleType);

            UnitOfWork.VehicleTypes.Update(savedVehicleType);


            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
        }



        public JsonResult DeleteVehicleType(long typeId)
        {
            var type = UnitOfWork.VehicleTypes.GetWhere(t => t.Id == typeId).FirstOrDefault();

            if (type == null)
            {
                return Json(AlertMessages.ModelError, JsonRequestBehavior.AllowGet);
            }

            UnitOfWork.VehicleTypes.Delete(type);
            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse, JsonRequestBehavior.AllowGet);

        }

        public ActionResult EditVehicleType(long typeId)
        {
            var vehicleType = UnitOfWork.VehicleTypes.GetWhere(t => t.Id == typeId, "LocalizedVehicleTypes").Select(t => new VehicleTypeViewModel
            {
                Id = t.Id,
                Name = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name,
                Descreption = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Descreption,
                ImageUrl = t.ImageUrl,
                IsEquipment = t.IsEquipment.Value
            }).FirstOrDefault();

            return View("Partials/_AddVehicleType", vehicleType);
        }

        public ActionResult VehicletypeList()
        {
            var vehicleTypes = UnitOfWork.VehicleTypes.GetAll(includeProperties: "LocalizedVehicleTypes").Select(t => new VehicleTypeViewModel
            {
                Id = t.Id,
                Name = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name,
                Descreption = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Descreption,
                ImageUrl = t.ImageUrl,
                IsEquipment = t.IsEquipment.Value
            }).ToList();
            return View("Partials/_VehicleTypes", vehicleTypes);
        }


        ////******************* Vehicle Capacities ******************************//

        public JsonResult AddVehicleCapacity(VehicleCapacityViewModel capacityViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }

            var capacityModel = new VehicleCapacity
            {
                Length = capacityViewModel.Length,
                PalletNumber = capacityViewModel.PalletNumber,
                VehicleTypeId = capacityViewModel.VehicleTypeId,
                Capacity = capacityViewModel.Capacity,
                LocalizedVehicleCapacities = new List<LocalizedVehicleCapacity>
                {
                   new LocalizedVehicleCapacity {
                                     Name = capacityViewModel.Name,
                                     CultureCode = SessionHandler.CultureCode
                                                 }
                }
            };

            UnitOfWork.VehicleCapacities.Insert(capacityModel);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);

        }

        public object UpdateVehicleCapacity(VehicleCapacityViewModel capacityViewModel)
        {
            var savedCapacity = UnitOfWork.VehicleCapacities.GetWhere(c => c.Id == capacityViewModel.Id).FirstOrDefault();

            if (savedCapacity == null)
            {
                return AlertMessages.ModelError;
            }

            savedCapacity.Capacity = capacityViewModel.Capacity;
            savedCapacity.Length = capacityViewModel.Length;
            savedCapacity.PalletNumber = capacityViewModel.PalletNumber;
            var localizedCapacity = savedCapacity.LocalizedVehicleCapacities.FirstOrDefault(c => c.CultureCode == SessionHandler.CultureCode);

            if (localizedCapacity != null)
            {
                localizedCapacity.CultureCode = SessionHandler.CultureCode;
                localizedCapacity.Name = capacityViewModel.Name;
                savedCapacity.LocalizedVehicleCapacities.Add(localizedCapacity);
            }

            UnitOfWork.VehicleCapacities.Update(savedCapacity);

            return (UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);

        }

        public ActionResult EditVehicleCapacity(long capacityId)
        {
            var capacityModel = UnitOfWork.VehicleCapacities.GetWhere(c => c.Id == capacityId, "LocalizedCapacities").Select(c => new VehicleCapacityViewModel
            {
                Id = c.Id,
                Capacity = c.Capacity.Value,
                Name = c.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name,
                VehicleTypeId = c.VehicleTypeId.Value,
                PalletNumber = c.PalletNumber.Value,
                Length = c.Length.Value

            }).FirstOrDefault();

            if (capacityModel == null)
            {
                return View("Partials/_AddVehicleCapicity", new VehicleCapacityViewModel());
            }

            capacityModel.VehicleTypesListItems = UnitOfWork.VehicleTypes
                                                 .GetAll(includeProperties: "LocalizedVehicleTypes")
                                                 .Select(t => new SelectListItem()
                                                 {
                                                     Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name,
                                                     Value = t.Id.ToString()
                                                 }).ToList();

            return View("Partials/_AddVehicleCapicity", capacityModel);

        }

        public ActionResult VehicleCapacitiesList()
        {
            var vehicleCapcitiesList = UnitOfWork.VehicleCapacities.
                                        GetAll(includeProperties: "VehicleType.LocalizedVehicleTypes,LocalizedCapacities")
                                       .Select(c => new VehicleCapacityListModel
                                       {
                                           Id = c.Id,
                                           Name = c.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name,
                                           Capacity = c.Capacity.Value,
                                           Length = c.Length.Value,
                                           PalletNumber = c.PalletNumber.Value,
                                           VehicleType = c.VehicleType.LocalizedVehicleTypes
                                                        .FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name
                                       }).ToList();
            return View("Partials/_VehicleCapicities", vehicleCapcitiesList);

        }


        public JsonResult VehicleCapacityDelete(long capacityId)
        {
            var capacity = UnitOfWork.VehicleCapacities.GetWhere(c => c.Id == capacityId).FirstOrDefault();

            if (capacity == null)
            {
                return Json(AlertMessages.ModelError, JsonRequestBehavior.AllowGet);
            }

            UnitOfWork.VehicleCapacities.Delete(capacity);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }


        ////****************** Vehicle Configurations *****************************//
        public JsonResult AddVehicleConfiguration(VehicleTypeConfigurationsViewModel configurationsViewModel)
        {

            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }

            var configurationModel = new VehicleConfiguration
            {
                ImageUrl = ImageUploader.SaveImageFromBase64(configurationsViewModel.ImageUrl, SessionKeys.VehicleImagesPath),
                VehicleTypeId = configurationsViewModel.TypeId,
                IsActive = true,
                LocalizedVehicleConfigurations = new List<LocalizedVehicleConfiguration>
                {
                    new LocalizedVehicleConfiguration
                        {
                            CultureCode = SessionHandler.CultureCode,
                            Name = configurationsViewModel.Name,
                            Descreption = configurationsViewModel.Descreption,
                    }
                }
            };

            UnitOfWork.VehicleConfigurations.Insert(configurationModel);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
        }

        public JsonResult UpdateVehicleConfiguration(VehicleTypeConfigurationsViewModel configurationsViewModel)
        {

            var savedConfiguration = UnitOfWork.VehicleConfigurations.GetWhere(c => c.Id == configurationsViewModel.Id, "LocalizedVehicleConfigurations").FirstOrDefault();

            if (savedConfiguration == null)
            {
                return Json(AlertMessages.ModelError);
            }

            savedConfiguration.VehicleTypeId = configurationsViewModel.TypeId;

            var localizedConfiguration = savedConfiguration.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode);

            if (localizedConfiguration != null)
            {
                localizedConfiguration.Name = configurationsViewModel.Name;
                localizedConfiguration.CultureCode = SessionHandler.CultureCode;
                localizedConfiguration.Descreption = configurationsViewModel.Descreption;

                savedConfiguration.LocalizedVehicleConfigurations.Add(localizedConfiguration);
            }

            UnitOfWork.VehicleConfigurations.Update(savedConfiguration);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);

        }

        public ActionResult EditVehicleConfiguration(long configurationId)
        {
            var configurationModel =
                UnitOfWork.VehicleConfigurations.GetWhere(c => c.Id == configurationId).Select(c => new VehicleTypeConfigurationsViewModel
                {
                    Descreption = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Descreption,
                    Name = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name,
                    TypeId = c.VehicleTypeId.Value,
                    ImageUrl = c.ImageUrl,
                    Id = c.Id
                }).FirstOrDefault();

            if (configurationModel != null)
            {
                configurationModel.VehicleTypesListItems = UnitOfWork.VehicleTypes
                                                           .GetAll(includeProperties: "LocalizedVehicleTypes")
                                                           .Select(t => new SelectListItem()
                                                           {
                                                               Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name,
                                                               Value = t.Id.ToString()
                                                           }).ToList();

                return View("Partials/_AddVehicleConfiguration", configurationModel);
            }

            return View("Partials/_AddVehicleConfiguration", new VehicleTypeConfigurationsViewModel());

        }
        public ActionResult VehicleConfigurationList()
        {
            var configurationsList = UnitOfWork.VehicleConfigurations
                                    .GetAll(includeProperties: "LocalizedVehicleTypesConfigurations,VehicleType.LocalizedVehicleTypes")
                                    .Select(c => new VehicleTypeConfigurationListModel
                                    {
                                        Id = c.Id,

                                        Name = c.LocalizedVehicleConfigurations
                                                        .FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name,

                                        Descreption = c.LocalizedVehicleConfigurations
                                                        .FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Descreption,
                                        ImageUrl = c.ImageUrl,

                                        VehicleTypeName = c.VehicleType.LocalizedVehicleTypes
                                                           .FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name
                                    }).ToList();

            return View("Partials/_VehicleConfigurationList", configurationsList);
        }

        public JsonResult DeleteVehicleConfiguration(long configurationId)
        {
            var configuration = UnitOfWork.VehicleConfigurations.GetWhere(t => t.Id == configurationId).FirstOrDefault();

            if (configuration == null)
            {
                return Json(AlertMessages.ModelError, JsonRequestBehavior.AllowGet);
            }

            UnitOfWork.VehicleConfigurations.Delete(configuration);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }


        public ActionResult VehicleMakes()
        {
            var makeViewModel = new MakeViewModel
            {
                MakeFormModel = new MakeFormModel
                {
                    Countries = UnitOfWork.LocalizedCountries.GetWhere(lc => lc.CultureCode == SessionHandler.CultureCode).Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.CountryId.ToString()
                    }).ToList(),
                    VehicleTypes = UnitOfWork.LocalizedVehicleTypes.GetWhere(c => c.CultureCode == SessionHandler.CultureCode).Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.VehicleTypeId.ToString()
                    }).ToList()

                },
                MakeDisplayModelList = UnitOfWork.VehicleMakes.GetWhere(m => m.IsActive == true).Select(m => new MakeDisplayModel
                {
                    Name = m.LocalizedVehicleMakes.FirstOrDefault(ml => ml.CultureCode == SessionHandler.CultureCode).Name,
                    ImageUrl = m.ImageUrl,
                    Id = m.Id,
                    Country = m.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name,
                    VehicleType = m.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name

                }).ToList()

            };
            return View("VehicleMakes", makeViewModel);
        }

        public JsonResult AddVehicleMake(MakeFormModel makeModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }

            var make = new VehicleMake
            {
                CountryId = makeModel.CountryId,
                ImageUrl = ImageUploader.SaveImageFromBase64(makeModel.ImageUrl, SessionKeys.VehicleImagesPath),
                IsActive = true,
                VehicleTypeId = makeModel.VehicleTypeId,
                LocalizedVehicleMakes = new List<LocalizedVehicleMake>()
                {
                    new LocalizedVehicleMake
                    {
                        CultureCode = SessionHandler.CultureCode,
                        Name = makeModel.Name
                    }
                }
            };

            UnitOfWork.VehicleMakes.Insert(make);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);

        }

        public ActionResult MakeList()
        {
            var makeList = UnitOfWork.VehicleMakes.GetAll().Select(m => new MakeDisplayModel
            {
                Name = m.LocalizedVehicleMakes.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name,
                Country = m.Country.LocalizedCountries.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name,
                VehicleType = m.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name,
                Id = m.Id,
                ImageUrl = m.ImageUrl
            }).ToList();

            return View("Partials/VehicleMakesList", makeList);
        }

        public JsonResult DeletMake(long makeId)
        {
            var make = UnitOfWork.VehicleMakes.GetWhere(m => m.Id == makeId).FirstOrDefault();

            if (make == null)
            {
                return Json(AlertMessages.ModelError, JsonRequestBehavior.AllowGet);
            }

            UnitOfWork.VehicleMakes.Delete(make);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditVehicleMake(long makeId)
        {
            var vehicleMake = UnitOfWork.VehicleMakes.GetWhere(m => m.Id == makeId).ToList().Select(m => new MakeFormModel
            {
                Name = m.LocalizedVehicleMakes.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name,
                CountryId = m.CountryId.Value,
                ImageUrl = m.ImageUrl,
                Id = m.Id,
                VehicleTypeId = m.VehicleTypeId,
                Countries = UnitOfWork.Countries.GetWhere(c => c.IsActive == true).Select(c => new SelectListItem
                {
                    Text = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name,
                    Value = c.Id.ToString()
                }).ToList()
            }).FirstOrDefault();

            return View("Partials/_AddMake", vehicleMake);
        }

        public JsonResult UpdateVehicleMake(MakeFormModel makeModel)
        {

            var savedMake = UnitOfWork.VehicleMakes.GetWhere(m => m.Id == makeModel.Id).FirstOrDefault();

            if (savedMake == null)
            {
                return Json(AlertMessages.ModelError);
            }

            if (!string.IsNullOrEmpty(makeModel.ImageUrl))
            {
                savedMake.ImageUrl = ImageUploader.SaveImageFromBase64(makeModel.ImageUrl, SessionKeys.VehicleImagesPath);
            }
            savedMake.CountryId = makeModel.CountryId;
            savedMake.VehicleTypeId = makeModel.VehicleTypeId;

            var localizedMake = UnitOfWork.VehicleMakes.GetWhere(lm => lm.Id == makeModel.Id)
                               .FirstOrDefault().LocalizedVehicleMakes.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode);

            if (localizedMake != null)
            {
                localizedMake.Name = makeModel.Name;
                savedMake.LocalizedVehicleMakes.Add(localizedMake);
            }

            UnitOfWork.VehicleMakes.Update(savedMake);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);

        }

        public ActionResult VehicleModels()
        {
            var vehicleModel = new VehicleModelViewModel
            {
                VehicleModel = new Models.Vehicle.VehicleModel
                {
                    MakeList = UnitOfWork.VehicleMakes.GetAll().Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.LocalizedVehicleMakes.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name
                    }).ToList(),

                },
                VehicleModelsList = UnitOfWork.VehicleModels.GetAll().Select(m => new Models.Vehicle.VehicleModel
                {
                    Id = m.Id,
                    ImageUrl = m.ImageURL,
                    Make = m.VehicleMake.LocalizedVehicleMakes.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name,
                    Name = m.LocalizedVehicleModels.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name
                }).ToList()
            };
            return View(vehicleModel);
        }

        public JsonResult AddVehicleModel(Models.Vehicle.VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }

            var model = new CargoMate.DataAccess.Models.BasicData.VehicleModel
            {
                VehicleMakeId = vehicleModel.MakeId,
                ImageURL = ImageUploader.SaveImageFromBase64(vehicleModel.ImageUrl, SessionKeys.VehicleImagesPath),
                LocalizedVehicleModels = new List<LocalizedVehicleModel>
                {
                    new LocalizedVehicleModel
                    {
                        CultureCode = SessionHandler.CultureCode,
                        Name = vehicleModel.Name
                    }
                }
            };

            UnitOfWork.VehicleModels.Insert(model);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
        }

        public ActionResult ModelList()
        {
            var vehicleModelsList = UnitOfWork.VehicleModels.GetAll().Select(m => new Models.Vehicle.VehicleModel
            {
                Id = m.Id,
                ImageUrl = m.ImageURL,
                Make = m.VehicleMake.LocalizedVehicleMakes.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name,
                Name = m.LocalizedVehicleModels.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name
            }).ToList();

            return View("Partials/_Modelslist", vehicleModelsList);

        }

        public ActionResult EditVehicleModel(long modelId)
        {
            var savedModel = UnitOfWork.VehicleModels.GetWhere(m => m.Id == modelId, includeProperties: "LocalizedVehicleModels").ToList().Select(m => new Models.Vehicle.VehicleModel
            {
                MakeList = UnitOfWork.VehicleMakes.GetAll(includeProperties: "LocalizedVehicleMakes").Select(ml => new SelectListItem
                {
                    Value = ml.Id.ToString(),
                    Text = ml.LocalizedVehicleMakes.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name
                }).ToList(),

                Id = m.Id,
                MakeId = m.VehicleMakeId,
                ImageUrl = m.ImageURL,
                Name = m.LocalizedVehicleModels.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name

            }).FirstOrDefault();

            return View("Partials/_AddModel", savedModel);
        }

        public JsonResult UpdateVehiclModel(Models.Vehicle.VehicleModel vehicleModel)
        {

            var savedModel = UnitOfWork.VehicleModels.GetWhere(m => m.Id == vehicleModel.Id).FirstOrDefault();
            if (savedModel == null)
            {
                return Json(AlertMessages.ModelError);
            }



            if (string.IsNullOrEmpty(vehicleModel.ImageUrl))
            {
                savedModel.ImageURL = ImageUploader.SaveImageFromBase64(vehicleModel.ImageUrl, SessionKeys.VehicleImagesPath);
            }

            savedModel.VehicleMakeId = vehicleModel.MakeId;
            var localizedModel = savedModel.LocalizedVehicleModels.FirstOrDefault(lm => lm.VehicleModelId == vehicleModel.Id);

            if (localizedModel != null)
            {
                localizedModel.Name = vehicleModel.Name;
                localizedModel.CultureCode = SessionHandler.CultureCode;

                savedModel.LocalizedVehicleModels.Add(localizedModel);
            }

            UnitOfWork.VehicleModels.Update(savedModel);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);

        }
        public JsonResult DeletModel(long modelId)
        {
            var model = UnitOfWork.VehicleModels.GetWhere(m => m.Id == modelId).FirstOrDefault();

            if (model == null)
            {
                return Json(AlertMessages.ModelError, JsonRequestBehavior.AllowGet);
            }

            UnitOfWork.VehicleModels.Delete(model);
            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VehicleModelYear()
        {
            var vehicleModelyears = new VehicleModelYearViewModel
            {
                VehicleModelYear = new VehicleModelYear
                {
                    ModelList = UnitOfWork.VehicleModels.GetAll().Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.LocalizedVehicleModels.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name
                    }).ToList()

                },
                VehicleModelYearList = UnitOfWork.ModelYearCombinations.GetAll().Select(m => new VehicleModelYear
                {
                    Id = m.Id,
                    Model = m.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name,
                    Year = m.Year.ToString(),
                    ImageUrl = m.ImageUrl
                }).ToList()
            };
            return View("VehiclModelYear", vehicleModelyears);
        }

        public ActionResult VehicleModelYearList()
        {
            var vehicleModelYearList = UnitOfWork.ModelYearCombinations.GetAll().Select(m => new VehicleModelYear
            {
                Id = m.Id,
                Model = m.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name,
                Year = m.Year.ToString(),
                ImageUrl = m.ImageUrl
            }).ToList();

            return View("Partials/_VehicleModelYearsList", vehicleModelYearList);
        }

        public JsonResult AddVehicelModelYear(VehicleModelYear modelYear)
        {
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }

            var modelYearCombination = new ModelYearCombination
            {
                VehicleModelId = modelYear.ModelId,
                Year = modelYear.YearId.Value,
                ImageUrl = ImageUploader.SaveImageFromBase64(modelYear.ImageUrl, SessionKeys.VehicleImagesPath)
            };

            UnitOfWork.ModelYearCombinations.Insert(modelYearCombination);
            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
        }

        public JsonResult DeleteVehicelModelYear(long modelYearId)
        {
            var model = UnitOfWork.ModelYearCombinations.GetWhere(m => m.Id == modelYearId).FirstOrDefault();

            if (model == null)
            {
                return Json(AlertMessages.ModelError, JsonRequestBehavior.AllowGet);
            }

            UnitOfWork.ModelYearCombinations.Delete(model);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditVehicleModelYear(long modelYearId)
        {
            var savedModelYear = UnitOfWork.ModelYearCombinations.GetWhere(m => m.Id == modelYearId).Select(m => new VehicleModelYear
            {

                Id = m.Id,
                ModelId = m.VehicleModelId,
                ImageUrl = m.ImageUrl,
                YearId = m.Year

            }).FirstOrDefault();
            if (savedModelYear != null)
            {
                savedModelYear.ModelList = UnitOfWork.VehicleModels.GetAll().Select(ml => new SelectListItem
                {
                    Value = ml.Id.ToString(),
                    Text = ml.LocalizedVehicleModels.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name
                }).ToList();

            }
            return View("Partials/_AddModelYear", savedModelYear);
        }

        public JsonResult UpdateVehiclModelYear(VehicleModelYear vehicleModelyear)
        {
            var imagePath = string.Empty;
            var imageName = string.Empty;

            var savedModelYear = UnitOfWork.ModelYearCombinations.GetWhere(m => m.Id == vehicleModelyear.Id).FirstOrDefault();

            if (savedModelYear == null)
            {
                return Json(AlertMessages.ModelError);
            }



            if (!string.IsNullOrEmpty(vehicleModelyear.ImageUrl))
            {
                savedModelYear.ImageUrl = ImageUploader.SaveImageFromBase64(vehicleModelyear.ImageUrl, SessionKeys.VehicleImagesPath);
            }

            savedModelYear.VehicleModelId = vehicleModelyear.ModelId;
            savedModelYear.Year = vehicleModelyear.YearId.Value;

            UnitOfWork.ModelYearCombinations.Update(savedModelYear);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);

        }

        public ActionResult PayLoadTypes()
        {
            var payLoadTypes = new PayLoadTypesViewModel
            {
                PayLoadModel = new PayLoadModel
                {
                    VehicleTypesList = UnitOfWork.VehicleTypes.GetAll(includeProperties: "LocalizedVehicleTypes").Select(t => new SelectListItem
                    {
                        Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name,
                        Value = t.Id.ToString()
                    }).ToList()
                },
                PayLoadModelList = UnitOfWork.PayLoadTypes.GetAll(includeProperties: "LocalizedPayLoadTypes").Select(pt => new PayLoadModel
                {
                    Id = pt.Id,
                    TypeId = pt.VehicleTypeId.Value,
                    ImageUrl = pt.ImageUrl,
                    Name = pt.LocalizedPayLoadTypes.FirstOrDefault(ptl => ptl.CultureCode == SessionHandler.CultureCode).Name,
                    TypeName = pt.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name
                }).ToList()

            };
            return View(payLoadTypes);
        }

        public JsonResult AddPayLoadtype(PayLoadModel payLoadModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }

            var payLoad = new PayLoadType
            {
                IsActive = true,
                VehicleTypeId = payLoadModel.TypeId,
                ImageUrl = ImageUploader.SaveImageFromBase64(payLoadModel.ImageUrl, SessionKeys.VehicleImagesPath),
                LocalizedPayLoadTypes = new List<LocalizedPayLoadType>
                {
                    new LocalizedPayLoadType
                     {
                        Name = payLoadModel.Name,
                        CultureCode = SessionHandler.CultureCode
                     }
                }
            };


            UnitOfWork.PayLoadTypes.Insert(payLoad);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);

        }

        public ActionResult PayLoadTypeList()
        {
            var payLoadModelList = UnitOfWork.PayLoadTypes.GetAll(includeProperties: "LocalizedPayLoadTypes").Select(pt => new PayLoadModel
            {
                Id = pt.Id,
                TypeId = pt.VehicleTypeId.Value,
                ImageUrl = pt.ImageUrl,
                Name = pt.LocalizedPayLoadTypes.FirstOrDefault(ptl => ptl.CultureCode == SessionHandler.CultureCode).Name,
                TypeName = pt.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name
            }).ToList();
            return View("Partials/_PayLoadtypeList", payLoadModelList);
        }

        public JsonResult DeletePayLoadTypes(long payloadId)
        {
            var model = UnitOfWork.PayLoadTypes.GetWhere(pt => pt.Id == payloadId).FirstOrDefault();
            if (model == null)
            {
                return Json(AlertMessages.ModelError, JsonRequestBehavior.AllowGet);
            }
            UnitOfWork.PayLoadTypes.Delete(model);
            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PayLoadEdit(long payloadId)
        {
            var model = UnitOfWork.PayLoadTypes.GetAll(includeProperties: "LocalizedPayLoadTypes").Where(pt => pt.Id == payloadId).Select(pt => new PayLoadModel
            {
                Id = pt.Id,
                ImageUrl = pt.ImageUrl,
                TypeId = pt.VehicleTypeId.Value,
                Name = pt.LocalizedPayLoadTypes.FirstOrDefault(ptl => ptl.CultureCode == SessionHandler.CultureCode).Name
            }).FirstOrDefault();

            if (model != null)
            {
                model.VehicleTypesList = UnitOfWork.VehicleTypes.GetAll(includeProperties: "LocalizedVehicleTypes").Select(t => new SelectListItem
                {
                    Text = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name,
                    Value = t.Id.ToString()
                }).ToList();
            }
            return View("Partials/_AddPayLoadType", model);
        }

        public JsonResult UpdatePayLoadType(PayLoadModel payLoadModel)
        {


            var savedModel = UnitOfWork.PayLoadTypes.GetWhere(m => m.Id == payLoadModel.Id).FirstOrDefault();
            if (savedModel == null)
            {
                return Json(AlertMessages.ModelError);
            }



            if (!string.IsNullOrEmpty(payLoadModel.ImageUrl))
            {
                savedModel.ImageUrl = ImageUploader.SaveImageFromBase64(payLoadModel.ImageUrl, SessionKeys.VehicleImagesPath);
            }

            savedModel.VehicleTypeId = payLoadModel.TypeId;

            var localizedModel = savedModel.LocalizedPayLoadTypes.FirstOrDefault(lpt => lpt.CultureCode==SessionHandler.CultureCode);
            if (localizedModel != null)
            {
                localizedModel.Name = payLoadModel.Name;
                localizedModel.CultureCode = SessionHandler.CultureCode;

                savedModel.LocalizedPayLoadTypes.Add(localizedModel);
            }

            UnitOfWork.PayLoadTypes.Update(savedModel);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);

        }

        public ActionResult Units()
        {
            var unitViewModel = new UnitViewModel
            {
                LengthModel = new LengthModel(),
                LengthModelList = UnitOfWork.LengthUnits.GetAll().Select(l => new LengthModel
                {
                    Id = l.Id,
                    IsMetric = l.IsMetric.Value,
                    LengthMultiple = l.LengthMultiple,
                    ShortName = l.LocalizedLengthUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode).ShortName,
                    FullName = l.LocalizedLengthUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode).FullName
                }).ToList(),

                WeightModel = new WeightModel(),
                WeightModelList = UnitOfWork.WeightUnits.GetAll().Select(w => new WeightModel
                {
                    Id = w.Id,
                    IsMetric = w.IsMetric.Value,
                    WeightMultiple = w.WeightMultiple,
                    ShortName = w.LocalizedWeightUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode).ShortName,
                    FullName = w.LocalizedWeightUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode).FullName

                }).ToList()
            };
            return View(unitViewModel);
        }

        public ActionResult AddWeight(WeightModel weightModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }


            var weight = new WeightUnit
            {
                IsMetric = weightModel.IsMetric,
                WeightMultiple = weightModel.WeightMultiple
            };
            var localizedWeight = new LocalizedWeightUnit
            {
                ShortName = weightModel.ShortName,
                CultureCode = SessionHandler.CultureCode,
                FullName = weightModel.FullName
            };

            weight.LocalizedWeightUnits.Add(localizedWeight);
            UnitOfWork.WeightUnits.Insert(weight);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
        }

        public ActionResult WeightList()
        {
            var weightModelList = UnitOfWork.WeightUnits.GetAll().Select(w => new WeightModel
            {
                Id = w.Id,
                IsMetric = w.IsMetric.Value,
                WeightMultiple = w.WeightMultiple,
                ShortName = w.LocalizedWeightUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode).ShortName,
                FullName = w.LocalizedWeightUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode).FullName

            }).ToList();

            return View("Partials/_WeightList", weightModelList);

        }

        public JsonResult DeleteWeight(long weightId)
        {
            var weight = UnitOfWork.WeightUnits.GetWhere(w => w.Id == weightId).FirstOrDefault();
            if (weight == null)
            {
                return Json(AlertMessages.ModelError, JsonRequestBehavior.AllowGet);
            }
            UnitOfWork.WeightUnits.Delete(weight);
            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse, JsonRequestBehavior.AllowGet);

        }

        public ActionResult EditWeight(long weightId)
        {
            var weight = UnitOfWork.WeightUnits.GetWhere(w => w.Id == weightId).Select(w => new WeightModel
            {
                Id = w.Id,
                IsMetric = w.IsMetric.Value,
                WeightMultiple = w.WeightMultiple,
                ShortName = w.LocalizedWeightUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode).ShortName,
                FullName = w.LocalizedWeightUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode).FullName
            }).FirstOrDefault();

            return View("Partials/_AddWeight", weight);
        }

        public JsonResult UpdateWeight(WeightModel weightModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }
            var localizedWeight = UnitOfWork.WeightUnits.GetWhere(w => w.Id == weightModel.Id)
                                  .FirstOrDefault().LocalizedWeightUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode);
            if (localizedWeight != null)
            {
                localizedWeight.FullName = weightModel.FullName;
                localizedWeight.ShortName = weightModel.ShortName;
            }
            var savedWeight = UnitOfWork.WeightUnits.GetWhere(w => w.Id == weightModel.Id).FirstOrDefault();
            if (savedWeight != null)
            {
                savedWeight.IsMetric = weightModel.IsMetric;
                savedWeight.WeightMultiple = weightModel.WeightMultiple;
                savedWeight.LocalizedWeightUnits.Add(localizedWeight);
            }

            UnitOfWork.WeightUnits.Update(savedWeight);
            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
        }

        public ActionResult AddLength(LengthModel lengthModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }


            var length = new LengthUnit
            {
                IsMetric = lengthModel.IsMetric,
                LengthMultiple = lengthModel.LengthMultiple,
                LocalizedLengthUnits = new List<LocalizedLengthUnit>
                {
                    new LocalizedLengthUnit
                 {
                ShortName = lengthModel.ShortName,
                CultureCode = SessionHandler.CultureCode,
                FullName = lengthModel.FullName
                 }
                }
            };
            UnitOfWork.LengthUnits.Insert(length);
            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
        }

        public ActionResult LengthList()
        {
            var lengthList = UnitOfWork.LengthUnits.GetAll().Select(l => new LengthModel
            {
                Id = l.Id,
                IsMetric = l.IsMetric.Value,
                LengthMultiple = l.LengthMultiple,
                ShortName = l.LocalizedLengthUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode).ShortName,
                FullName = l.LocalizedLengthUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode).FullName

            }).ToList();

            return View("Partials/_LengthList", lengthList);

        }

        public JsonResult DeleteLength(long lengthId)
        {
            var length = UnitOfWork.LengthUnits.GetWhere(l => l.Id == lengthId).FirstOrDefault();

            if (length == null)
            {
                return Json(AlertMessages.ModelError, JsonRequestBehavior.AllowGet);
            }

            UnitOfWork.LengthUnits.Delete(length);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse, JsonRequestBehavior.AllowGet);

        }

        public ActionResult EditLength(long lengthId)
        {
            var length = UnitOfWork.LengthUnits.GetWhere(l => l.Id == lengthId).Select(l => new LengthModel
            {
                Id = l.Id,
                IsMetric = l.IsMetric.Value,
                LengthMultiple = l.LengthMultiple,
                ShortName = l.LocalizedLengthUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode).ShortName,
                FullName = l.LocalizedLengthUnits.FirstOrDefault(lw => lw.CultureCode == SessionHandler.CultureCode).FullName
            }).FirstOrDefault();
            return View("Partials/_AddLength", length);
        }

        public JsonResult UpdateLength(LengthModel lengthModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(AlertMessages.ModelError);
            }
            var localizedLength = UnitOfWork.LengthUnits.GetWhere(l => l.Id == lengthModel.Id)
                                  .FirstOrDefault().LocalizedLengthUnits.FirstOrDefault(l => l.CultureCode == SessionHandler.CultureCode);
            if (localizedLength != null)
            {
                localizedLength.FullName = lengthModel.FullName;
                localizedLength.ShortName = lengthModel.ShortName;
            }
            var savedLength = UnitOfWork.LengthUnits.GetWhere(l => l.Id == lengthModel.Id).FirstOrDefault();
            if (savedLength != null)
            {
                savedLength.IsMetric = lengthModel.IsMetric;
                savedLength.LengthMultiple = lengthModel.LengthMultiple;
                savedLength.LocalizedLengthUnits.Add(localizedLength);
            }

            UnitOfWork.LengthUnits.Update(savedLength);

            return Json(UnitOfWork.Commit() > 0 ? AlertMessages.SuccessResponse : AlertMessages.FailureResponse);
        }
    }
}