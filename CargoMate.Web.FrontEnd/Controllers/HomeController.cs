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
            var searchModel = new SearchVehicleViewModel() {
                PayLoadTypes = UnitOfWork.LocalizedPayLoadTypes
                               .GetWhere(p => p.CultureCode == SessionHandler.CultureCode)
                               .Select(p=> new SelectListItem {
                                    Value = p.PayLoadTypeId.ToString(),
                                    Text=p.Name
                               }).ToList(),

                VehicleTypes = UnitOfWork.LocalizedVehicleTypes
                               .GetWhere(p => p.CultureCode == SessionHandler.CultureCode)
                               .Select(p => new SelectListItem {
                                    Value = p.VehicleTypeId.ToString(),
                                    Text = p.Name
                               }).ToList()
            };

            return View(searchModel);
        }

        [HttpPost]
        public ActionResult SearchResults(SearchVehicleViewModel searchModel) {

            if (!ModelState.IsValid)
            {
                return View("Index", searchModel);
            }

            var searchResults = UnitOfWork.Vehicles
                                .GetWhere(s => s.VehicleTypeId == searchModel.VehicleTypeId)
                                .ToList();

            return View(searchResults);
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

        public ActionResult Register() {
            return View();
        }

        public ActionResult AccountDetails() {
            return View();
        }

        public ActionResult CompanyRegisteration() {
            return View();
        }

        public ActionResult DriverDetails() {
            return View();
        }

        public ActionResult VehicleDetails() {
            return View();
        }

        public ActionResult SearchResults() {
            return View();
        }
    }
}