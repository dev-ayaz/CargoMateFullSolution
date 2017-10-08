using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Shared;
using CargoMate.Web.FrontEnd.Models;
using CargoMate.DataAccess.Models.Transporters;

namespace CargoMate.Web.FrontEnd.Controllers
{
    public class DriverController : BaseController
    {
        public DriverController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: Driver
        public ActionResult Index()
        {
            return View();
        }
         public ActionResult Register()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public JsonResult IsDriverExists(string userId)
        {
            var driver = UnitOfWork.DriverPersonalInfos.GetWhere(c => c.DriverId == userId).FirstOrDefault();

            if (driver == null)
            {
                return Json(new { IsExist = false, RedirectUrl = Url.Action("Register", "Driver", new { UId = userId }) }, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(driver.EmailAddress) || string.IsNullOrEmpty(driver.Name) || string.IsNullOrEmpty(driver.PhoneNumber))
            {
                return Json(new { IsExist = true, RedirectUrl = Url.Action("Edit", "Driver", new { driverId = userId }) }, JsonRequestBehavior.AllowGet);
            }

            Session[SessionKeys.DriverId] = userId;
            return Json(new { IsExists = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsProfileComplete(string driverId)
        {
            var driver = UnitOfWork.DriverPersonalInfos.GetWhere(c => c.DriverId == driverId).FirstOrDefault();
            if (driver != null && 
               (string.IsNullOrEmpty(driver.EmailAddress)||
                string.IsNullOrEmpty(driver.Name) ||
                string.IsNullOrEmpty(driver.PhoneNumber)||
                string.IsNullOrEmpty(driver.DriverLegalDocument.LicenseNumber)||
                string.IsNullOrEmpty(driver.DriverLegalDocument.ResidenceNumber)
                ))
            {
                return Json(new { IsComplete = false });
            }
            return Json(new { IsComplete = true });
        }

        [HttpPost]
        public JsonResult Register(RegisterWithFireBase driverForm)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }
            if (!UnitOfWork.DriverPersonalInfos.GetWhere(d => d.DriverId == driverForm.Id).Any())
            {


                UnitOfWork.DriverPersonalInfos.Insert(new DriverPersonalInfo
                {
                    EmailAddress = driverForm.Email,
                    DriverId = driverForm.Id,
                    ImageUrl = driverForm.ImageUrl,
                    Name = driverForm.Name,
                    PhoneNumber = driverForm.Phone

                });

                return
                    Json(UnitOfWork.Commit() > 0
                        ? CargoMateMessages.SuccessResponse
                        : CargoMateMessages.FailureResponse);
            }
            return Json(new
            {
                IsError = true,
                MessageHeader = "Operation Fail",
                Message = "Account Already Exists"
            });
        }

        public ActionResult DriverSignIn() {
            return View();
        }
    }
}