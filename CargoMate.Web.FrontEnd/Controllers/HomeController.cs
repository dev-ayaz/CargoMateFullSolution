using CargoMate.Web.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoMate.Web.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new LoginViewModel());
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