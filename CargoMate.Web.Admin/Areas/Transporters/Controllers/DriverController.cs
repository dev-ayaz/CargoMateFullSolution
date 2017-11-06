using CargoMate.Web.Admin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.Contracts;
using CargoMate.DataAccess.Models.Transporters;
using CargoMate.Web.Admin.Areas.Transporters.Models;

namespace CargoMate.Web.Admin.Areas.Transporters.Controllers
{
    public class DriverController : BaseController
    {
        public DriverController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: Transpoters/Driver
        public ActionResult Index()
        {
            return View();
        }

        // GET: Transpoters/Driver/DriversList
        public ActionResult DriversList()
        {
            return View();
        }

        public JsonResult GetDriversList(SearchFilter searchFilter)
        {

            var total = UnitOfWork.DriverPersonalInfos.Count();

            var pageNumber = searchFilter.PageLenght > 0 ? (searchFilter.Start / searchFilter.PageLenght) + 1 : 1;

            IOrderedQueryable<DriverPersonalInfo> OrderByExpression(IQueryable<DriverPersonalInfo> r) => r.OrderByDescending(m => m.Id);

            var driverList = UnitOfWork.DriverPersonalInfos.GetAll(pageNumber, searchFilter.PageLenght, null, OrderByExpression, "DriverStatus.LocalizedDriverStatuses").ToList()
                             .Select(d => new DriverDisplayViewModel
                             {
                                 Id = d.Id,
                                 DateOfBirth = d.DateOfBirth.ToString(),
                                 EmailAddress = d.EmailAddress,
                                 ImageUrl = d.ImageUrl,
                                 LegalName = d.LegalName,
                                 Name = d.Name,
                                 PhoneNumber = d.PhoneNumber,
                                 Status = d.DriverStatus?.LocalizedDriverStatuses?.FirstOrDefault(st => st.CultureCode == SessionHandler.CultureCode)?.Name
                             }).ToList();

           

            return Json(new
            {
                sEcho = searchFilter.Draw,
                iTotalRecords = total,
                iTotalDisplayRecords = driverList.Count,
                aaData = driverList
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DriverProfile(string hashcode, string driverId) {

            var driver = UnitOfWork.DriverPersonalInfos.GetWhere(d => d.Id == driverId, "Country.LocalizedCountries")
                         .ToList()
                         .Select(d => new DriverPersonalInfoDisplayModel
                         {
                             Id = d.Id,
                             Name = d.Name,
                             LegalName = d.LegalName,
                             PhoneNumber = d.PhoneNumber,
                             DateOfBirth = d.DateOfBirth.ToString(),
                             EmailAddress = d.EmailAddress,
                             ImageUrl = d.ImageUrl,
                             Gender = d.Gender.ToString(),
                             Country = d.Country?.LocalizedCountries?.FirstOrDefault(lc => lc.CultureCode == SessionHandler.CultureCode)?.Name,
                             Rating = d.Rating.ToString(),
                             TotalTrips = d.TotalTrips.ToString(),
                             FixedRate = d.FixedRate.ToString(),
                             IdVerified = d.IdVerified.ToString(),
                             MembershipDate = d.MembershipDate.ToString(),
                             IsValidated = d.IsValidated.ToString(),
                             Locality = d.Locality,
                             SubLocality = d.SubLocality

                         }).FirstOrDefault();

            var driverDocuments = UnitOfWork.DriverLegalDocuments.GetWhere(dd => dd.Id == driverId)
                                  .ToList()
                                  .Select(dld => new DriverLegalDocumentsDisplayModel {
                                      Id = dld.Id,
                                      LicenseExpiryDate = dld.LicenseExpiryDate.ToString(),
                                      LicenseImage = dld.LicenseImage,
                                      LicenseNumber = dld.LicenseNumber,
                                      ResidenceExpiryDate = dld.ResidenceExpiryDate.ToString(),
                                      ResidenceImage = dld.ResidenceImage,
                                      ResidenceNumber = dld.ResidenceNumber
                                  })
                                  .FirstOrDefault() ?? new DriverLegalDocumentsDisplayModel{ Id = driverId };

            var vehicles = UnitOfWork.Vehicles.GetWhere(v => v.DriverPersonalInfoId == driverId)
                           .Select(v => new DriverVehiclesDisplayModel {
                               Id = v.Id.ToString(),
                               RegisterationNumber = v.RegistrationNumber.ToString(),
                               PlateNumber = v.PlateNumber,
                               RegisterationExpirey = v.RegistrationExpiry.ToString(),
                               EngineNumber = v.EngineNumber,
                               IsActive = v.IsActive.Value.ToString(),
                               IsInsured = v.IsInsured.Value.ToString()
                           })
                           .ToList();

            var driverModel = new DriverViewModel
            {
                DriverPersonalInfoDisplayModel = driver,
                DriverLegalDocumentsDisplayModel = driverDocuments,
                DriverVehicleDisplayModel = vehicles
            };

            return View(driverModel);
        }
    }
}