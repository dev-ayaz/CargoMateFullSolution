﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Shared;
using CargoMate.Web.FrontEnd.Models;
using CargoMate.DataAccess.Models.Transporters;
using CargoMate.Web.FrontEnd.Models.DriverViewModel;
using System.Globalization;

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

        public ActionResult Edit(string driverId)
        {
            var driver = UnitOfWork.DriverPersonalInfos.GetWhere(d => d.Id == driverId)
                        .ToList()
                        .Select(d => new DriverPersonalInfoFormModel
                        {
                            Id = d.Id,
                            Name = d.Name,
                            LegalName = d.LegalName,
                            EmailAddress = d.EmailAddress,
                            ImageUrl = d.ImageUrl,
                            PhoneNumber = d.PhoneNumber ?? string.Empty,
                            DateOfBirth = d.DateOfBirth,
                            Gender = d.Gender,
                            CountryId = d.CountryId,
                            FairTypeIds = d.DriverFareTypes.Select(ft => ft.FareTypeId).ToArray(),
                            CountriesList = UnitOfWork.LocalizedCountries
                                           .GetWhere(lc => lc.CultureCode == SessionHandler.CultureCode)
                                           .Select(c => new SelectListItem
                                           {
                                               Text = c.Name,
                                               Value = c.CountryId.ToString()
                                           }).ToList(),
                            FairTypes = UnitOfWork.LocalizedFareTypes
                                           .GetWhere(lc => lc.CultureCode == SessionHandler.CultureCode)
                                           .Select(c => new SelectListItem
                                           {
                                               Text = c.Name,
                                               Value = c.FareTypeId.ToString()
                                           }).ToList()

                        }).FirstOrDefault();

            var driverDocuments = UnitOfWork.DriverLegalDocuments.GetWhere(dd => dd.Id == driver.Id).ToList().Select(ld => new DriverLegalDocumentsFormModel
            {
                Id = driver?.Id,
                LicenseExpiryDate=ld.LicenseExpiryDate,
                LicenseImage=ld.LicenseImage,
                LicenseNumber=ld.LicenseNumber,
                ResidenceExpiryDate=ld.ResidenceExpiryDate,
                ResidenceImage=ld.ResidenceImage,
                ResidenceNumber=ld.ResidenceNumber
            }).FirstOrDefault() ?? new DriverLegalDocumentsFormModel{Id =driverId};

            var driverModel = new DriverViewModel
            {
                driverPersonalInfoFormModel = driver,
                driverLegalDocumentsFormModel = driverDocuments
            }; 
            return View(driverModel);
        }

        [HttpPost]
        public JsonResult EditDriver(DriverPersonalInfoFormModel driverForm)
        {
            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }

            UnitOfWork.DriverPersonalInfos.Update(new DriverPersonalInfo
            {
                Id = driverForm.Id,
                EmailAddress = driverForm.EmailAddress,
                DateOfBirth = Convert.ToDateTime(driverForm.DateOfBirth, CultureInfo.InvariantCulture),
                Gender = driverForm.Gender,
                ImageUrl = CargoMateImageHandler.SaveImageFromBase64(driverForm.ImageUrl, GlobalProperties.DriverImagesFolder),
                Name = driverForm.Name,
                LegalName = driverForm.LegalName,
                PhoneNumber = driverForm.PhoneNumber,
                CountryId = driverForm.CountryId
            });
            var success = UnitOfWork.Commit() > 0;

            SetFairTypes(driverForm.FairTypeIds, driverForm.Id);

            return
                Json(success
                    ? CargoMateMessages.SuccessResponse
                    : CargoMateMessages.FailureResponse);


        }

        public int SetFairTypes(long?[] fairTypes,string driverId)
        {
            var savedFairTypes = UnitOfWork.DriverFareTypes.GetWhere(ft => ft.DriverPersonalInfoId==driverId).ToList();
            foreach(var ft in savedFairTypes)
            {
                UnitOfWork.DriverFareTypes.Delete(ft);
            }

            if (!fairTypes.Any()) return UnitOfWork.Commit();

            foreach (var val in fairTypes)
            {
                if (val!=null)
                {
                    UnitOfWork.DriverFareTypes.Insert(new DriverFareType
                    {
                        FareTypeId = val,
                        DriverPersonalInfoId = driverId
                    }); 
                }
            }

            return UnitOfWork.Commit();
        }

        [HttpPost]
        public JsonResult EditDriverDocuments(DriverLegalDocumentsFormModel driverDocumentsForm) {

            if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }

            var driverDocument = new DriverLegalDocument
            {
                Id = driverDocumentsForm.Id,
                LicenseNumber = driverDocumentsForm.LicenseNumber,
                LicenseExpiryDate = driverDocumentsForm.LicenseExpiryDate,
                ResidenceNumber = driverDocumentsForm.ResidenceNumber,
                ResidenceExpiryDate = driverDocumentsForm.ResidenceExpiryDate,
                LicenseImage = CargoMateImageHandler.SaveImageFromBase64(driverDocumentsForm.LicenseImage, GlobalProperties.DriverDocumentsFolder),
                ResidenceImage = CargoMateImageHandler.SaveImageFromBase64(driverDocumentsForm.ResidenceImage, GlobalProperties.DriverDocumentsFolder)
            };

            if (!UnitOfWork.DriverLegalDocuments.GetWhere(c => c.Id == driverDocumentsForm.Id).Any())
            {
                UnitOfWork.DriverLegalDocuments.Insert(driverDocument);
            }
            else {
                
                UnitOfWork.DriverLegalDocuments.Update(driverDocument);
            }

            return
                Json(UnitOfWork.Commit() > 0
                    ? CargoMateMessages.SuccessResponse
                    : CargoMateMessages.FailureResponse);
        }

        public JsonResult IsDriverExists(string userId)
        {
            var driver = UnitOfWork.DriverPersonalInfos.GetWhere(c => c.Id == userId).FirstOrDefault();

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
            var driver = UnitOfWork.DriverPersonalInfos.GetWhere(c => c.Id == driverId).FirstOrDefault();
            if (driver != null &&
               (string.IsNullOrEmpty(driver.EmailAddress) ||
                string.IsNullOrEmpty(driver.Name) ||
                string.IsNullOrEmpty(driver.PhoneNumber) ||
                string.IsNullOrEmpty(driver.DriverLegalDocument.LicenseNumber) ||
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
            if (!UnitOfWork.DriverPersonalInfos.GetWhere(d => d.Id == driverForm.Id).Any())
            {


                UnitOfWork.DriverPersonalInfos.Insert(new DriverPersonalInfo
                {
                    EmailAddress = driverForm.Email,
                    Id = driverForm.Id,
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

        public ActionResult DriverSignIn()
        {
            return View();
        }
    }
}