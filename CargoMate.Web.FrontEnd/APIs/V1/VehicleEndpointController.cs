using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Models;
using CargoMate.Web.FrontEnd.Models.VehicleViewModel;

namespace CargoMate.Web.FrontEnd.APIs.V1
{
    public class VehicleEndpointController : BaseController
    {
        public VehicleEndpointController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        #region +++++++++++++++++ Capacities Region ++++++++++++++++++++++++++++
        public List<KeyValuePairViewModel> GetCapacitiesAutoComplete(long vehicleTypeId,string cultureCode="en-US")
        {
            return UnitOfWork.VehicleCapacities.GetWhere(c => c.VehicleTypeId == vehicleTypeId).Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleCapacities.FirstOrDefault(lc=>lc.CultureCode==cultureCode).Name
            }).ToList();
        }

        public List<CapacityViewModel> GetCapacitiesList(long vehicleTypeId,string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleCapacities.GetWhere(c=>c.VehicleTypeId==vehicleTypeId).Select(c => new CapacityViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                Capacity=c.Capacity,
                Length=c.Length,
                PalletNumber=c.PalletNumber
            }).ToList();
        }

        public CapacityViewModel GetCapacityById(long capacityId ,string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleCapacities.GetWhere(c=>c.CultureCode==cultureCode).Select(c => new CapacityViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                Capacity = c.Capacity,
                Length = c.Length,
                PalletNumber = c.PalletNumber
            }).FirstOrDefault();
        }
        #endregion


        #region +++++++++++++++++ Capacities Region ++++++++++++++++++++++++++++
        public List<KeyValuePairViewModel> GetConfigurationsAutoComplete(long vehicleTypeId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleConfigurations.GetWhere(c => c.VehicleTypeId == vehicleTypeId).Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
            }).ToList();
        }

        public List<ConfigurationsViewModel> GetConfigurationsList(long vehicleTypeId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleConfigurations.GetWhere(c => c.VehicleTypeId == vehicleTypeId).Select(c => new ConfigurationsViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                Description = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Descreption,
                ImageUrl=c.ImageUrl
            }).ToList();
        }

        public ConfigurationsViewModel GetConfigurationById(long configurationId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleConfigurations.GetWhere(c=>c.Id==configurationId).Select(c => new ConfigurationsViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                Description = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Descreption,
                ImageUrl = c.ImageUrl
            }).FirstOrDefault();
        }
        #endregion


        #region +++++++++++++++++ Length Region ++++++++++++++++++++++++++++
        public List<KeyValuePairViewModel> GetLengthAutoComplete(string cultureCode = "en-US")
        {
            return UnitOfWork.LengthUnits.GetAll().Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.LocalizedLengthUnits.FirstOrDefault(lc => lc.CultureCode == cultureCode).FullName
            }).ToList();
        }

        public List<LengthViewModel> GetLengthList(string cultureCode = "en-US")
        {
            return UnitOfWork.LengthUnits.GetAll().Select(c => new LengthViewModel
            {
                Id = c.Id,
                ShortName = c.LocalizedLengthUnits.FirstOrDefault(lc => lc.CultureCode == cultureCode).ShortName,
                FullName = c.LocalizedLengthUnits.FirstOrDefault(lc => lc.CultureCode == cultureCode).FullName,
                IsMetric = c.IsMetric,
                LengthMultiple=c.LengthMultiple
            }).ToList();
        }

        public LengthViewModel GetLengthById(long lengthId, string cultureCode = "en-US")
        {
            return UnitOfWork.LengthUnits.GetWhere(l=>l.Id==lengthId).Select(c => new LengthViewModel
            {
                Id = c.Id,
                ShortName = c.LocalizedLengthUnits.FirstOrDefault(lc => lc.CultureCode == cultureCode).ShortName,
                FullName = c.LocalizedLengthUnits.FirstOrDefault(lc => lc.CultureCode == cultureCode).FullName,
                IsMetric = c.IsMetric,
                LengthMultiple = c.LengthMultiple
            }).FirstOrDefault();
        }
        #endregion


        #region +++++++++++++++++ Makes Region ++++++++++++++++++++++++++++
        public List<KeyValuePairViewModel> GetMakeAutoComplete(long countryId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleMakes.GetWhere(c => c.CountryId == countryId).Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleMakes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
            }).ToList();
        }

        public List<MakeViewModel> GetMakesList(long countryId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleMakes.GetWhere(c => c.CountryId == countryId,includeProperties: "LocalizedVehicleMakes,Country.LocalizedCountries").Select(c => new MakeViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleMakes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                CountryName = c.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl = c.ImageUrl
            }).ToList();
        }

        public MakeViewModel GetMakeById(long makeId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleMakes.GetWhere(l => l.Id == makeId).Select(c => new MakeViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleMakes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                CountryName = c.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl = c.ImageUrl
            }).FirstOrDefault();
        }
        #endregion


        #region +++++++++++++++++ PayLoad Region ++++++++++++++++++++++++++++
        public List<KeyValuePairViewModel> GetPayLoadAutoComplete(long vehicleTypeId,string cultureCode = "en-US")
        {
            return UnitOfWork.PayLoadTypes.GetWhere(c=>c.VehicleTypeId==vehicleTypeId,includeProperties: "LocalizedPayLoadTypes").Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.LocalizedPayLoadTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
            }).ToList();
        }

        public List<PayLoadTypeViewModel> GetPayLoadList(long vehicleTypeId,string cultureCode = "en-US")
        {
            return UnitOfWork.PayLoadTypes.GetWhere(c=>c.VehicleTypeId==vehicleTypeId,includeProperties: "LocalizedPayLoadTypes").Select(c => new PayLoadTypeViewModel
            {
                Id = c.Id,
                Name = c.LocalizedPayLoadTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl = c.ImageUrl
            }).ToList();
        }

        public PayLoadTypeViewModel GetPayLoadTypeById(long payLoadTypeId, string cultureCode = "en-US")
        {
            return UnitOfWork.PayLoadTypes.GetWhere(p=>p.Id==payLoadTypeId, "LocalizedPayLoadTypes").Select(c => new PayLoadTypeViewModel
            {
                Id = c.Id,
                Name = c.LocalizedPayLoadTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl = c.ImageUrl
            }).FirstOrDefault();
        }
        #endregion


        #region +++++++++++++++++ VehicleModels Region ++++++++++++++++++++++++++++
        public List<KeyValuePairViewModel> GetVehicleModelsAutoComplete(long makeId,string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleModels.GetWhere(c=>c.VehicleMakeId==makeId,includeProperties: "LocalizedVehicleModels").Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleModels.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
            }).ToList();
        }

        public List<VehicleModelViewModel> GetVehicleModelsList(long makeId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleModels.GetWhere(c=>c.VehicleMakeId==makeId,includeProperties: "LocalizedVehicleModels,VehicleMake.LocalizedVehicleMakes").Select(c => new VehicleModelViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleModels.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl = c.ImageURL,
                Make =c.VehicleMake.LocalizedVehicleMakes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
            }).ToList();
        }

        public VehicleModelViewModel GetVehicleModelById(long modelId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleModels.GetWhere(c=>c.Id==modelId, includeProperties: "LocalizedVehicleModels,VehicleMake.LocalizedVehicleMakes").Select(c => new VehicleModelViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleModels.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl = c.ImageURL,
                Make = c.VehicleMake.LocalizedVehicleMakes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
            }).FirstOrDefault();
        }
        #endregion

        #region +++++++++++++++++ VehicleModelYears Region ++++++++++++++++++++++++++++
        public List<KeyValuePairViewModel> GetVehicleModelYearsAutoComplete(long modelId, string cultureCode = "en-US")
        {
            return UnitOfWork.ModelYearCombinations.GetWhere(c => c.VehicleModelId == modelId).Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.Year.ToString()
            }).ToList();
        }

        public List<VehicleModelYearViewModel> GetVehicleModelYearsList(long modelId, string cultureCode = "en-US")
        {
            return UnitOfWork.ModelYearCombinations.GetWhere(c => c.VehicleModelId == modelId, includeProperties: "VehicleModel.LocalizedVehicleModels").Select(c => new VehicleModelYearViewModel
            {
                Id = c.Id,
                Model = c.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl = c.ImageUrl,
                Year = c.Year.ToString()
            }).ToList();
        }

        public VehicleModelYearViewModel GetVehicleModelYearsById(long modelId, string cultureCode = "en-US")
        {
            return UnitOfWork.ModelYearCombinations.GetWhere(c => c.VehicleModelId == modelId, includeProperties: "VehicleModel.LocalizedVehicleModels").Select(c => new VehicleModelYearViewModel
            {
                Id = c.Id,
                Model = c.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl = c.ImageUrl,
                Year = c.Year.ToString()
            }).FirstOrDefault();
        }
        #endregion


        #region +++++++++++++++++ VehiclTypes Region ++++++++++++++++++++++++++++
        public List<KeyValuePairViewModel> GetVehiclTypesAutoComplete(string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleTypes.GetAll().Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleTypes.FirstOrDefault(lc=>lc.CultureCode==cultureCode).Name
            }).ToList();
        }

        public List<VehicleTypeViewModel> GetVehicleTypesList(string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleTypes.GetAll(includeProperties: "LocalizedVehicleTypes").Select(c => new VehicleTypeViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl = c.ImageUrl,
                Description=c.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Descreption,
                IsEquipment=c.IsEquipment
            }).ToList();
        }

        public VehicleTypeViewModel GetVehicleTypeById(string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleTypes.GetAll(includeProperties: "LocalizedVehicleTypes").Select(c => new VehicleTypeViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl = c.ImageUrl,
                Description = c.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Descreption,
                IsEquipment = c.IsEquipment
            }).FirstOrDefault();
        }
        #endregion


        #region +++++++++++++++++ Weight Region ++++++++++++++++++++++++++++
        public List<KeyValuePairViewModel> GetWeightAutoComplete(string cultureCode = "en-US")
        {
            return UnitOfWork.WeightUnits.GetAll().Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.LocalizedWeightUnits.FirstOrDefault(lc => lc.CultureCode == cultureCode).FullName
            }).ToList();
        }

        public List<WeightViewModel> GetWeightList(string cultureCode = "en-US")
        {
            return UnitOfWork.WeightUnits.GetAll().Select(c => new WeightViewModel
            {
                Id = c.Id,
                ShortName = c.LocalizedWeightUnits.FirstOrDefault(lc => lc.CultureCode == cultureCode).ShortName,
                FullName = c.LocalizedWeightUnits.FirstOrDefault(lc => lc.CultureCode == cultureCode).FullName,
                IsMetric = c.IsMetric,
                WeightMultiple = c.WeightMultiple
            }).ToList();
        }

        public WeightViewModel GetWeightById(long weightId, string cultureCode = "en-US")
        {
            return UnitOfWork.WeightUnits.GetWhere(c=>c.Id== weightId).Select(c => new WeightViewModel
            {
                Id = c.Id,
                ShortName = c.LocalizedWeightUnits.FirstOrDefault(lc => lc.CultureCode == cultureCode).ShortName,
                FullName = c.LocalizedWeightUnits.FirstOrDefault(lc => lc.CultureCode == cultureCode).FullName,
                IsMetric = c.IsMetric,
                WeightMultiple = c.WeightMultiple
            }).FirstOrDefault();
        }
        #endregion
    }
}
