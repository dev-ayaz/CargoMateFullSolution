using CargoMate.Web.FrontEnd.Models;
using CargoMate.Web.FrontEnd.Models.SearchVehicleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Shared;

namespace CargoMate.Web.FrontEnd.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ActionResult Index()
        {
            var searchModel = new SearchVehicleViewModel()
            {
                VehicleCapacities = UnitOfWork.LocalizedVehicleCapacities
                               .GetWhere(p => p.CultureCode == SessionHandler.CultureCode)
                               .Select(p => new SelectListItem
                               {
                                   Value = p.VehicleCapacityId.ToString(),
                                   Text = p.Name
                               }).ToList(),
                VehicleTypes = UnitOfWork.LocalizedVehicleTypes
                               .GetWhere(p => p.CultureCode == SessionHandler.CultureCode)
                               .Select(p => new SelectListItem
                               {
                                   Value = p.VehicleTypeId.ToString(),
                                   Text = p.Name
                               }).ToList()
            };

            return View(searchModel);
        }


        [HttpGet]
        public ActionResult SearchResults([System.Web.Http.FromUri] SearchVehicleViewModel searchModel)
        {

            //if (!ModelState.IsValid)
            //{
            //    return View("Index", searchModel);
            //}

            //var searchResults = UnitOfWork.Vehicles
            //                    .GetAll(includeProperties: "VehicleCapacity,VehicleConfiguration.LocalizedVehicleConfigurations," +
            //                    "VehicleImages,ModelYearCombination.VehicleModel.VehicleMake.LocalizedVehicleMakes," +
            //                    "ModelYearCombination.VehicleModel.LocalizedVehicleModels,VehiclePayloadTypes," +
            //                    "VehicleType.LocalizedVehicleTypes,VehicleTripTypes,DriverPersonalInfo.Country.LocalizedCountries").Select(v => new VehicleSearchSumeryViewModel
            //                    {
            //                        Capacity = v.VehicleCapacity.Capacity.ToString(),
            //                        Configuration = v.VehicleConfiguration.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name,
            //                        Images = v.VehicleImages.Select(i => i.ImageUrl).ToList(),
            //                        IsInsured = v.IsInsured,
            //                        Make = v.ModelYearCombination.VehicleModel.VehicleMake.LocalizedVehicleMakes.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name,
            //                        MakeImage = v.ModelYearCombination.VehicleModel.VehicleMake.ImageUrl,
            //                        Model = v.ModelYearCombination.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name,
            //                        ModelImage = v.ModelYearCombination.VehicleModel.ImageURL,
            //                        PayLoadTypes = v.VehiclePayloadTypes.Select(p => p.PayLoadType.LocalizedPayLoadTypes.FirstOrDefault(lp => lp.CultureCode == SessionHandler.CultureCode).Name).ToList(),
            //                        PricePerKM = 50,
            //                        Year = v.ModelYearCombination.Year.ToString(),
            //                        VehicleId = v.Id,
            //                        Type = v.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name,
            //                        TypeImage = v.VehicleType.ImageUrl,
            //                        TypeDescreption = v.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Descreption,
            //                        TripeTypes = v.VehicleTripTypes.Select(lt => lt.TripType.LocalizedTripTypes.FirstOrDefault(l => l.CultureCode == SessionHandler.CultureCode).Name).ToList(),
            //                        DriverName = v.DriverPersonalInfo.LegalName,
            //                        DriverNationality = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name
            //                    })
            //                    .ToList();

            return View();
        }

        [HttpGet]
        public JsonResult GetVehicleById(long? vehicleId)
        {
            var vehicle = UnitOfWork.Vehicles
                        .GetWhere(v => v.Id == vehicleId, includeProperties:
                        "VehicleType.LocalizedVehicleTypes,DriverPersonalInfo.Country.LocalizedCountries," +
                        "ModelYearCombination.VehicleModel.VehicleMake.LocalizedVehicleMakes," +
                        "ModelYearCombination.VehicleModel.LocalizedVehicleModels")
                .Select(v => new VehiclePopupViewModel
                {
                    VehicleId = v.Id,
                    TypeImage = GlobalProperties.BasicDataImagesPath + v.VehicleType.ImageUrl,
                    Type = v.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Name,
                    TypeDescreption = v.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == SessionHandler.CultureCode).Descreption,
                    DriverName = v.DriverPersonalInfo.Name,
                    DriverNationality = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode).Name,
                    DriverPhone = v.DriverPersonalInfo.PhoneNumber,
                    Make = v.ModelYearCombination.VehicleModel.VehicleMake.LocalizedVehicleMakes.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name,
                    Model = v.ModelYearCombination.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lm => lm.CultureCode == SessionHandler.CultureCode).Name,
                    Year = v.ModelYearCombination.Year.ToString()
                }).FirstOrDefault();

            return Json(vehicle, JsonRequestBehavior.AllowGet);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult AccountDetails()
        {
            return View();
        }

        public ActionResult CompanyRegisteration()
        {
            return View();
        }

        public ActionResult DriverDetails()
        {
            return View();
        }

        public ActionResult VehicleDetails()
        {
            return View();
        }

        public ActionResult SearchResults()
        {
            return View();
        }
    }
}