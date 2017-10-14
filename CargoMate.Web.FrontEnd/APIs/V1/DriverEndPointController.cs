using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Models.DriverViewModel;
using CargoMate.DataAccess.Models.Transporters;
using System.Data.Entity.Spatial;
using CargoMate.Web.FrontEnd.Shared;
using System.Linq.Expressions;
using CargoMate.Web.FrontEnd.Models;

namespace CargoMate.Web.FrontEnd.APIs.V1
{
    public class DriverEndPointController : BaseController
    {
        public DriverEndPointController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public HttpResponseMessage AddDriver(DriverPersonalInfoFormModel driverPersonalInfoForm)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }

            UnitOfWork.DriverPersonalInfos.Insert(new DriverPersonalInfo
            {   
                Id= driverPersonalInfoForm.Id,
                Locality = driverPersonalInfoForm.Locality,
                SubLocality = driverPersonalInfoForm.SubLocality,
                CountryId = driverPersonalInfoForm.CountryId,
                DateOfBirth = driverPersonalInfoForm.DateOfBirth,
                DriverStatusId = driverPersonalInfoForm.DriverStatusId,
                EmailAddress = driverPersonalInfoForm.EmailAddress,
                FixedRate = driverPersonalInfoForm.FixedRate,
                LegalName = driverPersonalInfoForm.LegalName,
                Location = DbGeography.FromText($"POINT({driverPersonalInfoForm.Location})"),
                MembershipDate = DateTime.Now.Date,
                Name = driverPersonalInfoForm.Name,
                PhoneNumber = driverPersonalInfoForm.PhoneNumber,
                Gender = driverPersonalInfoForm.Gender,
                ImageUrl= CargoMateImageHandler.SaveImageFromBase64(driverPersonalInfoForm.ImageUrl, GlobalProperties.DriverImagesFolder),
                DriverFareTypes= driverPersonalInfoForm.FairTypeId?.Select(t=>new DriverFareType {FareTypeId=t}).ToList()
            });

            return Request.CreateResponse(HttpStatusCode.OK, UnitOfWork.Commit());
        }

        [HttpGet]
        public DriverPersonalInfoDisplayModel GetDriverByFilter(string driverId="", string emailAddress="", string phoneNumber="",string cultureCode="en-US")
        {
            return UnitOfWork.DriverPersonalInfos.GetWhere(DriversFilter(driverId, emailAddress, phoneNumber))
                 .ToList().Select(c => new DriverPersonalInfoDisplayModel
                 {
                     Id = c.Id,
                     Name = c.Name,
                     DateOfBirth = c.DateOfBirth.ToString(),
                     EmailAddress = c.EmailAddress,
                     Gender = c.Gender,
                     ImageUrl = c.ImageUrl,
                     PhoneNumber = c.PhoneNumber,
                     FixedRate=c.FixedRate,
                     CountryName=c.Country.LocalizedCountries.FirstOrDefault(ld=>ld.CultureCode==cultureCode).Name,
                     LegalName=c.LegalName,
                     DriverStatus=c.DriverStatus.LocalizedDriverStatuses.FirstOrDefault(ld => ld.CultureCode == cultureCode).Name,
                     Location =c.Location.ToString(),
                     Locality = c.Locality,
                     SubLocality = c.SubLocality
                 }).FirstOrDefault();
        }

        private Expression<Func<DriverPersonalInfo, bool>> DriversFilter(string driverId, string emailAddress, string phoneNumber)
        {
            var predicate = PredicateBuilder.True<DriverPersonalInfo>();


            if (!string.IsNullOrWhiteSpace(driverId))
            {
                predicate = predicate.And(c => c.Id == driverId);
            }
            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                predicate = predicate.And(c => c.EmailAddress == emailAddress);
            }
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                predicate = predicate.And(c => c.PhoneNumber == phoneNumber);
            }

            return predicate;
        }

        [HttpPost]
        public HttpResponseMessage AddDriverLegalDocuments(DriverLegalDocumentsFormModel legealDocumentsForm)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }

            UnitOfWork.DriverLegalDocuments.Insert(new DriverLegalDocument
            {
                Id=legealDocumentsForm.Id,
                LicenseExpiryDate = legealDocumentsForm.LicenseExpiryDate,
                LicenseImage = CargoMateImageHandler.SaveImageFromBase64(legealDocumentsForm.LicenseImage, GlobalProperties.DriverImagesFolder),
                LicenseNumber=legealDocumentsForm.LicenseNumber,
                ResidenceNumber = legealDocumentsForm.ResidenceNumber,
                ResidenceExpiryDate = legealDocumentsForm.ResidenceExpiryDate,
                ResidenceImage = CargoMateImageHandler.SaveImageFromBase64(legealDocumentsForm.ResidenceImage, GlobalProperties.DriverImagesFolder)
            });
            return Request.CreateResponse(HttpStatusCode.OK, UnitOfWork.Commit());
        }

        [HttpGet]
        public DriverLegalDocumentsDisplayModel GetLegalDocumentsById(string driverId)
        {
            return UnitOfWork.DriverLegalDocuments.GetWhere(ld => ld.Id == driverId).Select(ld => new DriverLegalDocumentsDisplayModel
            {
                Id=ld.Id,
                LicenseExpiryDate=ld.LicenseExpiryDate,
                LicenseImage=ld.LicenseImage,
                LicenseNumber=ld.LicenseNumber,
                ResidenceExpiryDate=ld.ResidenceExpiryDate,
                ResidenceImage=ld.ResidenceImage,
                ResidenceNumber=ld.ResidenceNumber
            }).FirstOrDefault();
        }

        [HttpGet]
        public List<KeyValuePairViewModel> FaretypesAutoComplete(string cultureCode = "en-US")
        {
            return UnitOfWork.FareTypes.GetWhere(c => c.IsActive.Value).Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.LocalizedFareTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
            }).ToList();
        }

    }
}
