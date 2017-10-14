using CargoMate.Web.FrontEnd.Models.VehicleRegistrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoMate.Web.FrontEnd.Controllers
{
    public class VehicleController : Controller
    {
        // GET: Vehicle
        public ActionResult Index()
        {
            var vehicle = new VehicleViewModel
            {
                BasicInformation = new BasicInformation(),
                InsuranceInformation = new InsuranceInformation(),
                TripInformation = new TripInformation(),
                VehicleScan = new VehicleScan()
            };
            return View(vehicle);
        }
    }
}