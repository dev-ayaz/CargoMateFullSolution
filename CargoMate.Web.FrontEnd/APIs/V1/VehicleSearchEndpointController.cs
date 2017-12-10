using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Models.VehicleViewModel;
using CargoMate.Web.FrontEnd.Models.VehicleRegistrationModels;
using CargoMate.DataAccess.Models.Vehicles;
using CargoMate.Web.FrontEnd.Shared;
using System.Linq.Expressions;

namespace CargoMate.Web.FrontEnd.APIs.V1
{
    public class VehicleSearchEndpointController : BaseController
    {
        public VehicleSearchEndpointController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        [HttpGet]
        public List<VehicleSearchResult> VehicleSearchResult(long? vehicleId=null, string engineNumber="",string plateNumber="",string registrationNumber="", string cultureCode = "en-US")
        {
            var vehicles = UnitOfWork.Vehicles.GetWhere(VehicleFilter(vehicleId,engineNumber,plateNumber,registrationNumber),
               includeProperties: "VehicleCapacity.LocalizedVehicleCapacities," +
               "VehicleConfiguration.LocalizedVehicleConfigurations," +
               "DriverPersonalInfo.DriverLegalDocument," +
               "DriverPersonalInfo.Country.LocalizedCountries," +
               "DriverPersonalInfo.DriverStatus.LocalizedDriverStatuses," +
               "InsuranceCompany.LocalizedInsuranceCompanies," +
               "ModelYearCombination.VehicleModel.VehicleMake.LocalizedVehicleMakes," +
               "ModelYearCombination.VehicleModel.VehicleMake.Country.LocalizedCountries" +
               "ModelYearCombination.VehicleModel.LocalizedVehicleModels," +
               "PayLoadType.LocalizedPayLoadTypes," +
               "TripType.LocalizedTripTypes," +
               "VehicleType.LocalizedVehicleTypes," +
               "VehicleImages")
               .Select(v => new VehicleSearchResult
               {
                   VehicleId=v.Id,
                   Capacity = new CapacityViewModel
                   {
                       Capacity = v.VehicleCapacity.Capacity,
                       Id = v.VehicleCapacity.Id,
                       Length = v.VehicleCapacity.Length,
                       PalletNumber = v.VehicleCapacity.PalletNumber,
                       Name = v.VehicleCapacity.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
                   },
                   Configurations = new ConfigurationsViewModel
                   {
                       Id = v.VehicleConfiguration.Id,
                       ImageUrl = v.VehicleConfiguration.ImageUrl,
                       Name = v.VehicleConfiguration.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                       Description = v.VehicleConfiguration.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Descreption
                   },
                   DriverLegalDocuments = new Models.DriverViewModel.DriverLegalDocumentsDisplayModel
                   {
                       Id = v.DriverPersonalInfo.DriverLegalDocument.Id,
                       LicenseExpiryDate = v.DriverPersonalInfo.DriverLegalDocument.LicenseExpiryDate,
                       LicenseImage = v.DriverPersonalInfo.DriverLegalDocument.LicenseImage,
                       LicenseNumber = v.DriverPersonalInfo.DriverLegalDocument.LicenseNumber,
                       ResidenceExpiryDate = v.DriverPersonalInfo.DriverLegalDocument.ResidenceExpiryDate,
                       ResidenceImage = v.DriverPersonalInfo.DriverLegalDocument.ResidenceImage,
                       ResidenceNumber = v.DriverPersonalInfo.DriverLegalDocument.ResidenceNumber
                   },
                   DriverPersonalInfo = new Models.DriverViewModel.DriverPersonalInfoDisplayModel
                   {
                       Address = "",
                       CountryName = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                       Country = new Models.CountryViewModel
                       {
                           CountryCode = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CountryCode,
                           CurrencyLong = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CurrencyLong,
                           Flag = v.DriverPersonalInfo.Country.Flag,
                           CurrencyCode = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CurrencyCode,
                           CurrencySymbol = v.DriverPersonalInfo.Country.CurrencySymbol,
                           Id = v.DriverPersonalInfo.Country.Id,
                           PhonCode = v.DriverPersonalInfo.Country.PhonCode,
                           Name = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
                       },
                       DateOfBirth = v.DriverPersonalInfo.DateOfBirth,
                       DriverStatus = v.DriverPersonalInfo.DriverStatus.LocalizedDriverStatuses.FirstOrDefault(ld => ld.CultureCode == cultureCode).Name,
                       EmailAddress = v.DriverPersonalInfo.EmailAddress,
                       FixedRate = v.DriverPersonalInfo.FixedRate,
                       Gender = v.DriverPersonalInfo.Gender,
                       Id = v.DriverPersonalInfo.Id,
                       ImageUrl = v.DriverPersonalInfo.ImageUrl,
                       LegalName = v.DriverPersonalInfo.LegalName,
                       Locality = v.DriverPersonalInfo.Locality,
                       Location = v.DriverPersonalInfo.Location.Latitude + "," + v.DriverPersonalInfo.Location.Longitude,
                       Name = v.DriverPersonalInfo.Name,
                       PhoneNumber = v.DriverPersonalInfo.PhoneNumber,
                       SubLocality = v.DriverPersonalInfo.SubLocality
                   },
                   EngineNumber = v.EngineNumber,
                   InsuranceAmount = v.InsuranceAmount,
                   InsuranceCompany = v.InsuranceCompany.LocalizedInsuranceCompanies.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                   InsuranceExpiry = v.InsuranceExpirey,
                   InsurancePolicyNo = v.PolicyNumber,
                   IsActive = v.IsActive,
                   isInsured = v.IsInsured,
                   PlateNumber = v.PlateNumber,
                   Year = new VehicleModelYearViewModel
                   {
                       Id = v.ModelYearCombinationId,
                       ImageUrl = v.ModelYearCombination.ImageUrl,
                       Model = v.ModelYearCombination.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lm => lm.CultureCode == cultureCode).Name,
                       Year = v.ModelYearCombination.Year.ToString()
                   },
                   Make = new MakeCompositeViewModel
                   {
                       Id = v.ModelYearCombination.VehicleModel.VehicleMake.Id,
                       ImageUrl = v.ModelYearCombination.VehicleModel.VehicleMake.ImageUrl,
                       Name = v.ModelYearCombination.VehicleModel.VehicleMake.LocalizedVehicleMakes.FirstOrDefault(lm => lm.CultureCode == cultureCode).Name,
                       CountryName = v.ModelYearCombination.VehicleModel.VehicleMake.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                       MakeCountry = new MakeCountry
                       {
                           Id = v.ModelYearCombination.VehicleModel.VehicleMake.CountryId,
                           Flag = v.ModelYearCombination.VehicleModel.VehicleMake.Country.Flag,
                           Name = v.ModelYearCombination.VehicleModel.VehicleMake.Country.LocalizedCountries.FirstOrDefault(lm => lm.CultureCode == cultureCode).Name
                       }

                   },
                   Model = new VehicleModelViewModel
                   {
                       Id = v.ModelYearCombination.VehicleModel.Id,
                       ImageUrl = v.ModelYearCombination.VehicleModel.ImageURL,
                       Name = v.ModelYearCombination.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lv => lv.CultureCode == cultureCode).Name
                   },
                   PayloadTypes = v.VehiclePayloadTypes.Select(p => new PayLoadTypeViewModel
                   {
                       Id = p.PayLoadTypeId.Value,
                       ImageUrl = p.PayLoadType.ImageUrl,
                       Name = p.PayLoadType.LocalizedPayLoadTypes.FirstOrDefault(lp => lp.CultureCode == cultureCode).Name
                   }).ToList(),
                   RegistrationExpiry = v.RegistrationExpiry,
                   RegistrationImage = v.RegistrationImage,
                   RegistrationNumber = v.RegistrationNumber,
                   TripTypes = v.VehicleTripTypes.Select(tt => new TripTypesViewModel
                   {
                       Id = tt.TripTypeId,
                       Name = tt.TripType.LocalizedTripTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Name
                   }).ToList(),
                   VehicleType = new VehicleTypeViewModel
                   {
                       Id = v.VehicleTypeId.Value,
                       IsEquipment = v.VehicleType.IsEquipment,
                       ImageUrl = v.VehicleType.ImageUrl,
                       Description = v.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Descreption,
                       Name = v.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Name
                   },
                   VehicleImages = v.VehicleImages.Select(i => i.ImageUrl).ToList()

               }).ToList();
            return vehicles;
        }


        [HttpGet]
        public List<VehicleSearchResult> VehicleListByIds(string vehicleIds ,string cultureCode = "en-US")
        {

            var separated = vehicleIds.Split(new char[] { ',' });
            List<long> vehicleIdsList = separated.Select(s => long.Parse(s)).ToList();

            var vehicles = UnitOfWork.Vehicles.GetWhere(v=> vehicleIdsList.Contains(v.Id),
               includeProperties: "VehicleCapacity.LocalizedVehicleCapacities," +
               "VehicleConfiguration.LocalizedVehicleConfigurations," +
               "DriverPersonalInfo.DriverLegalDocument," +
               "DriverPersonalInfo.Country.LocalizedCountries," +
               "DriverPersonalInfo.DriverStatus.LocalizedDriverStatuses," +
               "InsuranceCompany.LocalizedInsuranceCompanies," +
               "ModelYearCombination.VehicleModel.VehicleMake.LocalizedVehicleMakes," +
               "ModelYearCombination.VehicleModel.VehicleMake.Country.LocalizedCountries" +
               "ModelYearCombination.VehicleModel.LocalizedVehicleModels," +
               "PayLoadType.LocalizedPayLoadTypes," +
               "TripType.LocalizedTripTypes," +
               "VehicleType.LocalizedVehicleTypes," +
               "VehicleImages")
               .Select(v => new VehicleSearchResult
               {
                   VehicleId = v.Id,
                   Capacity = new CapacityViewModel
                   {
                       Capacity = v.VehicleCapacity.Capacity,
                       Id = v.VehicleCapacity.Id,
                       Length = v.VehicleCapacity.Length,
                       PalletNumber = v.VehicleCapacity.PalletNumber,
                       Name = v.VehicleCapacity.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
                   },
                   Configurations = new ConfigurationsViewModel
                   {
                       Id = v.VehicleConfiguration.Id,
                       ImageUrl = v.VehicleConfiguration.ImageUrl,
                       Name = v.VehicleConfiguration.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                       Description = v.VehicleConfiguration.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Descreption
                   },
                   DriverLegalDocuments = new Models.DriverViewModel.DriverLegalDocumentsDisplayModel
                   {
                       Id = v.DriverPersonalInfo.DriverLegalDocument.Id,
                       LicenseExpiryDate = v.DriverPersonalInfo.DriverLegalDocument.LicenseExpiryDate,
                       LicenseImage = v.DriverPersonalInfo.DriverLegalDocument.LicenseImage,
                       LicenseNumber = v.DriverPersonalInfo.DriverLegalDocument.LicenseNumber,
                       ResidenceExpiryDate = v.DriverPersonalInfo.DriverLegalDocument.ResidenceExpiryDate,
                       ResidenceImage = v.DriverPersonalInfo.DriverLegalDocument.ResidenceImage,
                       ResidenceNumber = v.DriverPersonalInfo.DriverLegalDocument.ResidenceNumber
                   },
                   DriverPersonalInfo = new Models.DriverViewModel.DriverPersonalInfoDisplayModel
                   {
                       Address = "",
                       CountryName = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                       Country = new Models.CountryViewModel
                       {
                           CountryCode = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CountryCode,
                           CurrencyLong = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CurrencyLong,
                           Flag = v.DriverPersonalInfo.Country.Flag,
                           CurrencyCode = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CurrencyCode,
                           CurrencySymbol = v.DriverPersonalInfo.Country.CurrencySymbol,
                           Id = v.DriverPersonalInfo.Country.Id,
                           PhonCode = v.DriverPersonalInfo.Country.PhonCode,
                           Name = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
                       },
                       DateOfBirth = v.DriverPersonalInfo.DateOfBirth,
                       DriverStatus = v.DriverPersonalInfo.DriverStatus.LocalizedDriverStatuses.FirstOrDefault(ld => ld.CultureCode == cultureCode).Name,
                       EmailAddress = v.DriverPersonalInfo.EmailAddress,
                       FixedRate = v.DriverPersonalInfo.FixedRate,
                       Gender = v.DriverPersonalInfo.Gender,
                       Id = v.DriverPersonalInfo.Id,
                       ImageUrl = v.DriverPersonalInfo.ImageUrl,
                       LegalName = v.DriverPersonalInfo.LegalName,
                       Locality = v.DriverPersonalInfo.Locality,
                       Location = v.DriverPersonalInfo.Location.Latitude + "," + v.DriverPersonalInfo.Location.Longitude,
                       Name = v.DriverPersonalInfo.Name,
                       PhoneNumber = v.DriverPersonalInfo.PhoneNumber,
                       SubLocality = v.DriverPersonalInfo.SubLocality
                   },
                   EngineNumber = v.EngineNumber,
                   InsuranceAmount = v.InsuranceAmount,
                   InsuranceCompany = v.InsuranceCompany.LocalizedInsuranceCompanies.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                   InsuranceExpiry = v.InsuranceExpirey,
                   InsurancePolicyNo = v.PolicyNumber,
                   IsActive = v.IsActive,
                   isInsured = v.IsInsured,
                   PlateNumber = v.PlateNumber,
                   Year = new VehicleModelYearViewModel
                   {
                       Id = v.ModelYearCombinationId,
                       ImageUrl = v.ModelYearCombination.ImageUrl,
                       Model = v.ModelYearCombination.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lm => lm.CultureCode == cultureCode).Name,
                       Year = v.ModelYearCombination.Year.ToString()
                   },
                   Make = new MakeCompositeViewModel
                   {
                       Id = v.ModelYearCombination.VehicleModel.VehicleMake.Id,
                       ImageUrl = v.ModelYearCombination.VehicleModel.VehicleMake.ImageUrl,
                       Name = v.ModelYearCombination.VehicleModel.VehicleMake.LocalizedVehicleMakes.FirstOrDefault(lm => lm.CultureCode == cultureCode).Name,
                       CountryName = v.ModelYearCombination.VehicleModel.VehicleMake.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                       MakeCountry = new MakeCountry
                       {
                           Id = v.ModelYearCombination.VehicleModel.VehicleMake.CountryId,
                           Flag = v.ModelYearCombination.VehicleModel.VehicleMake.Country.Flag,
                           Name = v.ModelYearCombination.VehicleModel.VehicleMake.Country.LocalizedCountries.FirstOrDefault(lm => lm.CultureCode == cultureCode).Name
                       }

                   },
                   Model = new VehicleModelViewModel
                   {
                       Id = v.ModelYearCombination.VehicleModel.Id,
                       ImageUrl = v.ModelYearCombination.VehicleModel.ImageURL,
                       Name = v.ModelYearCombination.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lv => lv.CultureCode == cultureCode).Name
                   },
                   PayloadTypes = v.VehiclePayloadTypes.Select(p => new PayLoadTypeViewModel
                   {
                       Id = p.PayLoadTypeId.Value,
                       ImageUrl = p.PayLoadType.ImageUrl,
                       Name = p.PayLoadType.LocalizedPayLoadTypes.FirstOrDefault(lp => lp.CultureCode == cultureCode).Name
                   }).ToList(),
                   RegistrationExpiry = v.RegistrationExpiry,
                   RegistrationImage = v.RegistrationImage,
                   RegistrationNumber = v.RegistrationNumber,
                   TripTypes = v.VehicleTripTypes.Select(tt => new TripTypesViewModel
                   {
                       Id = tt.TripTypeId,
                       Name = tt.TripType.LocalizedTripTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Name
                   }).ToList(),
                   VehicleType = new VehicleTypeViewModel
                   {
                       Id = v.VehicleTypeId.Value,
                       IsEquipment = v.VehicleType.IsEquipment,
                       ImageUrl = v.VehicleType.ImageUrl,
                       Description = v.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Descreption,
                       Name = v.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Name
                   },
                   VehicleImages = v.VehicleImages.Select(i => i.ImageUrl).ToList()

               }).ToList();
            return vehicles;
        }

        private Expression<Func<Vehicle, bool>> VehicleFilter(long? vehicleId, string engineNumber, string plateNumber, string registrationNumber)
        {
            var predicate = PredicateBuilder.True<Vehicle>();

            if (!string.IsNullOrWhiteSpace(engineNumber))
            {
                predicate = predicate.And(c => c.EngineNumber == engineNumber);
            }
            if (!string.IsNullOrWhiteSpace(plateNumber))
            {
                predicate = predicate.And(c => c.PlateNumber == plateNumber);
            }
            if (!string.IsNullOrWhiteSpace(registrationNumber))
            {
                predicate = predicate.And(c => c.RegistrationNumber == registrationNumber);
            }
            if (vehicleId.HasValue)
            {
                predicate = predicate.And(c => c.Id == vehicleId);
            }
            return predicate;
        }

        [HttpGet]
        public List<VehicleSearchResultShort> GetVehicleListByDriver(string driverId,string cultureCode="en-US")
        {


            var vehicles = UnitOfWork.Vehicles.GetWhere(v => v.DriverPersonalInfoId == driverId,
                includeProperties: "VehicleCapacity.LocalizedVehicleCapacities," +
                "VehicleConfiguration.LocalizedVehicleConfigurations," +
                "ModelYearCombination.VehicleModel.VehicleMake.Country.LocalizedCountries," +
                "InsuranceCompany.LocalizedInsuranceCompanies," +
                "ModelYearCombination.VehicleModel.VehicleMake.LocalizedVehicleMakes," +
                "ModelYearCombination.VehicleModel.LocalizedVehicleModels," +
                "PayLoadType.LocalizedPayLoadTypes," +
                "TripType.LocalizedTripTypes," +
                "VehicleType.LocalizedVehicleTypes," +
                "VehicleImages")
                .Select(v => new VehicleSearchResultShort
                {
                    VehicleId=v.Id,
                Capacity=new CapacityViewModel
                {
                    Capacity=v.VehicleCapacity.Capacity,
                    Id=v.VehicleCapacity.Id,
                    Length=v.VehicleCapacity.Length,
                    PalletNumber=v.VehicleCapacity.PalletNumber,
                    Name=v.VehicleCapacity.LocalizedVehicleCapacities.FirstOrDefault(lc=>lc.CultureCode==cultureCode).Name
                },
                Configurations = new ConfigurationsViewModel
                {
                    Id=v.VehicleConfiguration.Id,
                    ImageUrl=v.VehicleConfiguration.ImageUrl,
                    Name=v.VehicleConfiguration.LocalizedVehicleConfigurations.FirstOrDefault(lc=>lc.CultureCode==cultureCode).Name,
                    Description= v.VehicleConfiguration.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Descreption
                },
                EngineNumber=v.EngineNumber,
                InsuranceAmount=v.InsuranceAmount,
                 InsuranceCompany=v.InsuranceCompany.LocalizedInsuranceCompanies.FirstOrDefault(lc=>lc.CultureCode==cultureCode).Name,
                 InsuranceExpiry=v.InsuranceExpirey,
                 InsurancePolicyNo=v.PolicyNumber,
                 IsActive=v.IsActive,
                 isInsured=v.IsInsured,
                 PlateNumber=v.PlateNumber,
                 Year=new VehicleModelYearViewModel {
                     Id=v.ModelYearCombinationId,
                     ImageUrl=v.ModelYearCombination.ImageUrl,
                     Model=v.ModelYearCombination.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lm=>lm.CultureCode==cultureCode).Name,
                     Year=v.ModelYearCombination.Year.ToString()
                 },
                 Make=new MakeCompositeViewModel
                 {
                     Id=v.ModelYearCombination.VehicleModel.VehicleMake.Id,
                     ImageUrl= v.ModelYearCombination.VehicleModel.VehicleMake.ImageUrl,
                     Name= v.ModelYearCombination.VehicleModel.VehicleMake.LocalizedVehicleMakes.FirstOrDefault(lm=>lm.CultureCode==cultureCode).Name,
                     CountryName= v.ModelYearCombination.VehicleModel.VehicleMake.Country.LocalizedCountries.FirstOrDefault(lc=>lc.CultureCode==cultureCode).Name,
                     MakeCountry=new MakeCountry
                     {
                         Id=v.ModelYearCombination.VehicleModel.VehicleMake.CountryId,
                         Flag=v.ModelYearCombination.VehicleModel.VehicleMake.Country.Flag,
                         Name=v.ModelYearCombination.VehicleModel.VehicleMake.Country.LocalizedCountries.FirstOrDefault(lm=>lm.CultureCode==cultureCode).Name
                     }
                     
                 },
                 Model= new VehicleModelViewModel
                 {
                     Id=v.ModelYearCombination.VehicleModel.Id,
                     ImageUrl=v.ModelYearCombination.VehicleModel.ImageURL,
                     Name = v.ModelYearCombination.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lv=>lv.CultureCode==cultureCode).Name
                 },
                 PayloadTypes= v.VehiclePayloadTypes.Select(p=>new PayLoadTypeViewModel{
                         Id=p.PayLoadTypeId.Value,
                         ImageUrl=p.PayLoadType.ImageUrl,
                         Name=p.PayLoadType.LocalizedPayLoadTypes.FirstOrDefault(lp=>lp.CultureCode==cultureCode).Name
                 }).ToList(),
                  RegistrationExpiry=v.RegistrationExpiry,
                  RegistrationImage=v.RegistrationImage,
                  RegistrationNumber=v.RegistrationNumber,
                  TripTypes=v.VehicleTripTypes.Select(tt=>new TripTypesViewModel {
                      Id=tt.TripTypeId,
                      Name=tt.TripType.LocalizedTripTypes.FirstOrDefault(lt=>lt.CultureCode==cultureCode).Name
                  }).ToList(),
                  VehicleType=new VehicleTypeViewModel
                  {
                      Id=v.VehicleTypeId.Value,
                      IsEquipment=v.VehicleType.IsEquipment,
                      ImageUrl=v.VehicleType.ImageUrl,
                      Description=v.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt=>lt.CultureCode==cultureCode).Descreption,
                      Name= v.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Name
                  },
                  VehicleImages=v.VehicleImages.Select(i=>i.ImageUrl).ToList()
                 
            }).ToList();
            return vehicles;
        }

        [HttpPost]
        public HttpResponseMessage Addvehicle(BasicInformation basicInfo,string cultureCode = "en-US")
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }

            var vehicle = UnitOfWork.Vehicles.Insert(new Vehicle
            {
                DriverPersonalInfoId = basicInfo.DriverId,
                IsActive = false,
                IsVerified = false,
                ModelYearCombinationId = basicInfo.VehicleModelYearId,
                VehicleCapacityId = basicInfo.VehicleCapacityId,
                VehicleConfigurationId = basicInfo.VehicleConfigurationId,
                VehiclePayloadTypes = basicInfo.PayLoadTypeIds?.Select(p => new VehiclePayloadType { PayLoadTypeId = p.Value }).ToList(),
                VehicleTripTypes = basicInfo.TripTypeIds?.Select(p => new VehicleTripType { TripTypeId = p.Value }).ToList()
            });
            var result = UnitOfWork.Commit();
            if (result== 0)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, new{Error="Unable to save data" });
            }
            var vehicles = UnitOfWork.Vehicles.GetWhere(v => v.Id==vehicle.Id,
               includeProperties: "VehicleCapacity.LocalizedVehicleCapacities," +
               "VehicleConfiguration.LocalizedVehicleConfigurations," +
               "DriverPersonalInfo.DriverLegalDocument," +
               "DriverPersonalInfo.Country.LocalizedCountries," +
               "DriverPersonalInfo.DriverStatus.LocalizedDriverStatuses," +
               "InsuranceCompany.LocalizedInsuranceCompanies," +
               "ModelYearCombination.VehicleModel.VehicleMake.LocalizedVehicleMakes," +
               "ModelYearCombination.VehicleModel.VehicleMake.Country.LocalizedCountries" +
               "ModelYearCombination.VehicleModel.LocalizedVehicleModels," +
               "PayLoadType.LocalizedPayLoadTypes," +
               "TripType.LocalizedTripTypes," +
               "VehicleType.LocalizedVehicleTypes," +
               "VehicleImages")
               .Select(v => new VehicleSearchResult
               {
                   VehicleId = v.Id,
                   Capacity = new CapacityViewModel
                   {
                       Capacity = v.VehicleCapacity.Capacity,
                       Id = v.VehicleCapacity.Id,
                       Length = v.VehicleCapacity.Length,
                       PalletNumber = v.VehicleCapacity.PalletNumber,
                       Name = v.VehicleCapacity.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
                   },
                   Configurations = new ConfigurationsViewModel
                   {
                       Id = v.VehicleConfiguration.Id,
                       ImageUrl = v.VehicleConfiguration.ImageUrl,
                       Name = v.VehicleConfiguration.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                       Description = v.VehicleConfiguration.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Descreption
                   },
                   DriverLegalDocuments = new Models.DriverViewModel.DriverLegalDocumentsDisplayModel
                   {
                       Id = v.DriverPersonalInfo.DriverLegalDocument.Id,
                       LicenseExpiryDate = v.DriverPersonalInfo.DriverLegalDocument.LicenseExpiryDate,
                       LicenseImage = v.DriverPersonalInfo.DriverLegalDocument.LicenseImage,
                       LicenseNumber = v.DriverPersonalInfo.DriverLegalDocument.LicenseNumber,
                       ResidenceExpiryDate = v.DriverPersonalInfo.DriverLegalDocument.ResidenceExpiryDate,
                       ResidenceImage = v.DriverPersonalInfo.DriverLegalDocument.ResidenceImage,
                       ResidenceNumber = v.DriverPersonalInfo.DriverLegalDocument.ResidenceNumber
                   },
                   DriverPersonalInfo = new Models.DriverViewModel.DriverPersonalInfoDisplayModel
                   {
                       Address = "",
                       CountryName = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                       Country = new Models.CountryViewModel
                       {
                           CountryCode = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CountryCode,
                           CurrencyLong = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CurrencyLong,
                           Flag = v.DriverPersonalInfo.Country.Flag,
                           CurrencyCode = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CurrencyCode,
                           CurrencySymbol = v.DriverPersonalInfo.Country.CurrencySymbol,
                           Id = v.DriverPersonalInfo.Country.Id,
                           PhonCode = v.DriverPersonalInfo.Country.PhonCode,
                           Name = v.DriverPersonalInfo.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
                       },
                       DateOfBirth = v.DriverPersonalInfo.DateOfBirth,
                       DriverStatus = v.DriverPersonalInfo.DriverStatus.LocalizedDriverStatuses.FirstOrDefault(ld => ld.CultureCode == cultureCode).Name,
                       EmailAddress = v.DriverPersonalInfo.EmailAddress,
                       FixedRate = v.DriverPersonalInfo.FixedRate,
                       Gender = v.DriverPersonalInfo.Gender,
                       Id = v.DriverPersonalInfo.Id,
                       ImageUrl = v.DriverPersonalInfo.ImageUrl,
                       LegalName = v.DriverPersonalInfo.LegalName,
                       Locality = v.DriverPersonalInfo.Locality,
                       Location = v.DriverPersonalInfo.Location.Latitude + "," + v.DriverPersonalInfo.Location.Longitude,
                       Name = v.DriverPersonalInfo.Name,
                       PhoneNumber = v.DriverPersonalInfo.PhoneNumber,
                       SubLocality = v.DriverPersonalInfo.SubLocality
                   },
                   EngineNumber = v.EngineNumber,
                   InsuranceAmount = v.InsuranceAmount,
                   InsuranceCompany = v.InsuranceCompany.LocalizedInsuranceCompanies.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                   InsuranceExpiry = v.InsuranceExpirey,
                   InsurancePolicyNo = v.PolicyNumber,
                   IsActive = v.IsActive,
                   isInsured = v.IsInsured,
                   PlateNumber = v.PlateNumber,
                   Year = new VehicleModelYearViewModel
                   {
                       Id = v.ModelYearCombinationId,
                       ImageUrl = v.ModelYearCombination.ImageUrl,
                       Model = v.ModelYearCombination.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lm => lm.CultureCode == cultureCode).Name,
                       Year = v.ModelYearCombination.Year.ToString()
                   },
                   Make = new MakeCompositeViewModel
                   {
                       Id = v.ModelYearCombination.VehicleModel.VehicleMake.Id,
                       ImageUrl = v.ModelYearCombination.VehicleModel.VehicleMake.ImageUrl,
                       Name = v.ModelYearCombination.VehicleModel.VehicleMake.LocalizedVehicleMakes.FirstOrDefault(lm => lm.CultureCode == cultureCode).Name,
                       CountryName = v.ModelYearCombination.VehicleModel.VehicleMake.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                       MakeCountry = new MakeCountry
                       {
                           Id = v.ModelYearCombination.VehicleModel.VehicleMake.CountryId,
                           Flag = v.ModelYearCombination.VehicleModel.VehicleMake.Country.Flag,
                           Name = v.ModelYearCombination.VehicleModel.VehicleMake.Country.LocalizedCountries.FirstOrDefault(lm => lm.CultureCode == cultureCode).Name
                       }

                   },
                   Model = new VehicleModelViewModel
                   {
                       Id = v.ModelYearCombination.VehicleModel.Id,
                       ImageUrl = v.ModelYearCombination.VehicleModel.ImageURL,
                       Name = v.ModelYearCombination.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lv => lv.CultureCode == cultureCode).Name
                   },
                   PayloadTypes = v.VehiclePayloadTypes.Select(p => new PayLoadTypeViewModel
                   {
                       Id = p.PayLoadTypeId.Value,
                       ImageUrl = p.PayLoadType.ImageUrl,
                       Name = p.PayLoadType.LocalizedPayLoadTypes.FirstOrDefault(lp => lp.CultureCode == cultureCode).Name
                   }).ToList(),
                   RegistrationExpiry = v.RegistrationExpiry,
                   RegistrationImage = v.RegistrationImage,
                   RegistrationNumber = v.RegistrationNumber,
                   TripTypes = v.VehicleTripTypes.Select(tt => new TripTypesViewModel
                   {
                       Id = tt.TripTypeId,
                       Name = tt.TripType.LocalizedTripTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Name
                   }).ToList(),
                   VehicleType = new VehicleTypeViewModel
                   {
                       Id = v.VehicleTypeId.Value,
                       IsEquipment = v.VehicleType.IsEquipment,
                       ImageUrl = v.VehicleType.ImageUrl,
                       Description = v.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Descreption,
                       Name = v.VehicleType.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Name
                   },
                   VehicleImages = v.VehicleImages.Select(i => i.ImageUrl).ToList()

               }).FirstOrDefault();

            return Request.CreateResponse(HttpStatusCode.OK, new {vehicleId = vehicle.Id });

        }

        [HttpPost]
        public HttpResponseMessage AddvehicleScan(VehicleScan vehicleScan)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }

            var vehicle = UnitOfWork.Vehicles.GetWhere(v => v.Id == vehicleScan.VehicleId).FirstOrDefault();

            if (vehicle == null)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, "Vehicle Not Found");
            }

            vehicle.RegistrationExpiry = vehicleScan.RegistrationExpiry;
            vehicle.RegistrationImage = CargoMateImageHandler.SaveImageFromBase64(vehicleScan.RegistrationImage, GlobalProperties.VehicleImagesFolder);
            vehicle.RegistrationNumber = vehicleScan.RegistrationNumber;
            vehicle.EngineNumber = vehicleScan.EngineNumber;
            vehicle.PlateNumber = vehicleScan.PlateNumber;
            UnitOfWork.Vehicles.Update(vehicle);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage AddvehicleInsurance(InsuranceInformation insuranceInformation)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }

            long? VehicleId = insuranceInformation.VehicleId > 0 ? insuranceInformation.VehicleId : SessionHandler.VehicleId;
            var vehicle = UnitOfWork.Vehicles.GetWhere(v => v.Id == VehicleId).FirstOrDefault();

            if (vehicle == null)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, "Vehicle No Found");
            }

            vehicle.InsuranceCompanyId = insuranceInformation.InsuranceCompanyId;
            vehicle.InsuranceAmount = insuranceInformation.InsuranceAmount;
            vehicle.InsuranceExpirey = insuranceInformation.InsuranceExpirey;
            vehicle.PolicyNumber = insuranceInformation.PolicyNumber;
            vehicle.IsInsured = true;
            UnitOfWork.Vehicles.Update(vehicle);
            return Request.CreateResponse(HttpStatusCode.OK);
        }



       

    }
}
