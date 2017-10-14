using CargoMate.Web.FrontEnd.Models.VehicleRegistrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Shared;

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
                BasicInformation = new BasicInformation {
                    VehicleTypes = UnitOfWork.VehicleTypes.GetAll().Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.LocalizedVehicleTypes.FirstOrDefault(lt=>lt.CultureCode== SessionHandler.CultureCode).Name
                    }).ToList()
                },
                InsuranceInformation = new InsuranceInformation(),
                TripInformation = new TripInformation(),
                VehicleScan = new VehicleScan()
            };
            return View(vehicle);
        }
    }
}