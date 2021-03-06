﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Models;
using CargoMate.Web.FrontEnd.Models.VehicleViewModel;
using CargoMate.Web.FrontEnd.Shared;

namespace CargoMate.Web.FrontEnd.APIs.V1
{
    public class VehicleEndpointController : BaseController
    {
        public VehicleEndpointController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        #region +++++++++++++++++ Capacities Region ++++++++++++++++++++++++++++
        public List<KeyValuePairViewModel> GetCapacitiesAutoComplete(long vehicleTypeId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleCapacities.GetWhere(c => c.VehicleTypeId == vehicleTypeId).Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
            }).ToList();
        }

        public List<CapacityViewModel> GetCapacitiesList(long vehicleTypeId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleCapacities.GetWhere(c => c.VehicleTypeId == vehicleTypeId).Select(c => new CapacityViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                Weight = c.Weight,
                Length = c.Length,
                Breadth = c.Breadth,
                Height = c.Height,
                BaseUOM = c.UOM == null ? null : new UOMViewModel
                {
                    Id = c.UOMId,
                    Description = c.UOM.LocalizedUOMs.FirstOrDefault(u => u.CultureCode == cultureCode).Description,
                    Factor = c.UOM.Factor,
                    Name = c.UOM.LocalizedUOMs.FirstOrDefault(u => u.CultureCode == cultureCode).Name,
                    ShortName = c.UOM.LocalizedUOMs.FirstOrDefault(u => u.CultureCode == cultureCode).ShortName
                },
                LengthUnit = c.LengthUnit == null ? null : new LengthViewModel
                {
                    Id = c.LengthUnitId,
                    IsMetric = c.LengthUnit.IsMetric,
                    LengthMultiple = c.LengthUnit.LengthMultiple,
                    ShortName = c.LengthUnit.LocalizedLengthUnits.FirstOrDefault(lu => lu.CultureCode == cultureCode).ShortName,
                    FullName = c.LengthUnit.LocalizedLengthUnits.FirstOrDefault(lu => lu.CultureCode == cultureCode).FullName
                },

                WeightUnit = c.WeightUnit == null ? null : new WeightViewModel
                {
                    Id = c.WeightUnitId,
                    IsMetric = c.LengthUnit.IsMetric,
                    WeightMultiple = c.WeightUnit.WeightMultiple,
                    ShortName = c.WeightUnit.LocalizedWeightUnits.FirstOrDefault(lu => lu.CultureCode == cultureCode).ShortName,
                    FullName = c.WeightUnit.LocalizedWeightUnits.FirstOrDefault(lu => lu.CultureCode == cultureCode).FullName
                },
                MaximumQuantity = c.MaximumQuantity
            }).ToList();
        }

        public CapacityViewModel GetCapacityById(long capacityId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleCapacities.GetWhere(c => c.Id==capacityId).Select(c => new CapacityViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleCapacities.FirstOrDefault(lc=>lc.CultureCode==cultureCode).Name,
                Weight = c.Weight,
                Length = c.Length,
                Breadth= c.Breadth,
                Height = c.Height,
                BaseUOM = c.UOM==null?null: new UOMViewModel
                {
                    Id=c.UOMId,
                    Description=c.UOM.LocalizedUOMs.FirstOrDefault(u=>u.CultureCode==cultureCode).Description,
                    Factor=c.UOM.Factor,
                    Name = c.UOM.LocalizedUOMs.FirstOrDefault(u => u.CultureCode == cultureCode).Name,
                    ShortName = c.UOM.LocalizedUOMs.FirstOrDefault(u => u.CultureCode == cultureCode).ShortName
                },
                LengthUnit = c.LengthUnit==null?null: new LengthViewModel
                {
                    Id=c.LengthUnitId,
                    IsMetric=c.LengthUnit.IsMetric,
                    LengthMultiple = c.LengthUnit.LengthMultiple,
                    ShortName = c.LengthUnit.LocalizedLengthUnits.FirstOrDefault(lu => lu.CultureCode==cultureCode).ShortName,
                    FullName = c.LengthUnit.LocalizedLengthUnits.FirstOrDefault(lu => lu.CultureCode == cultureCode).FullName
                },

                WeightUnit = c.WeightUnit == null?null: new WeightViewModel
                {
                    Id = c.WeightUnitId,
                    IsMetric = c.LengthUnit.IsMetric,
                    WeightMultiple = c.WeightUnit.WeightMultiple,
                    ShortName = c.WeightUnit.LocalizedWeightUnits.FirstOrDefault(lu => lu.CultureCode == cultureCode).ShortName,
                    FullName = c.WeightUnit.LocalizedWeightUnits.FirstOrDefault(lu => lu.CultureCode == cultureCode).FullName
                },
                MaximumQuantity = c.MaximumQuantity
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
                ImageUrl =  c.ImageUrl
            }).ToList();
        }

        public ConfigurationsViewModel GetConfigurationById(long configurationId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleConfigurations.GetWhere(c => c.Id == configurationId).Select(c => new ConfigurationsViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                Description = c.LocalizedVehicleConfigurations.FirstOrDefault(lc => lc.CultureCode == cultureCode).Descreption,
                ImageUrl =  c.ImageUrl
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
                LengthMultiple = c.LengthMultiple
            }).ToList();
        }

        public LengthViewModel GetLengthById(long lengthId, string cultureCode = "en-US")
        {
            return UnitOfWork.LengthUnits.GetWhere(l => l.Id == lengthId).Select(c => new LengthViewModel
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

        public List<MakeTreeStructureViewModel> GetMakesList(string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleMakes.GetWhere(includeProperties: "LocalizedVehicleMakes,Country.LocalizedCountries,VehicleModels.LocalizedVehicleModels,VehicleModels.ModelYearCombinations").Select(c => new MakeTreeStructureViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleMakes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                MakeCountry=new MakeCountry
                {
                    Id= c.Country.Id,
                    Flag=c.Country.Flag,
                    Name= c.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
                },
                Models=c.VehicleModels.Select(m=>new VehicleModelCompositViewModel {
                    Id=m.Id,
                    ImageUrl=m.ImageURL,
                    Name=m.LocalizedVehicleModels.FirstOrDefault(ml=>ml.CultureCode==cultureCode).Name,
                    Years=m.ModelYearCombinations.Select(mc=>new VehicleModelYearViewModel {
                         Id=mc.Id,
                         ImageUrl=mc.ImageUrl,
                         Year=mc.Year+""
                    }).ToList()
                }).ToList(),
                ImageUrl =  c.ImageUrl
            }).ToList();
        }

        public MakeViewModel GetMakeById(long makeId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleMakes.GetWhere(l => l.Id == makeId).Select(c => new MakeViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleMakes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                CountryName = c.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl =  c.ImageUrl
            }).FirstOrDefault();
        }
        #endregion


        #region +++++++++++++++++ PayLoad Region ++++++++++++++++++++++++++++
        public List<KeyValuePairViewModel> GetPayLoadAutoComplete(long vehicleTypeId, string cultureCode = "en-US")
        {
            return UnitOfWork.PayLoadTypes.GetWhere(c => c.VehicleTypeId == vehicleTypeId, includeProperties: "LocalizedPayLoadTypes").Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.LocalizedPayLoadTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
            }).ToList();
        }

        public List<PayLoadTypeViewModel> GetPayLoadList(long vehicleTypeId, string cultureCode = "en-US")
        {
            return UnitOfWork.PayLoadTypes.GetWhere(c => c.VehicleTypeId == vehicleTypeId, includeProperties: "LocalizedPayLoadTypes").Select(c => new PayLoadTypeViewModel
            {
                Id = c.Id,
                Name = c.LocalizedPayLoadTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl =  c.ImageUrl
            }).ToList();
        }

        public PayLoadTypeViewModel GetPayLoadTypeById(long payLoadTypeId, string cultureCode = "en-US")
        {
            return UnitOfWork.PayLoadTypes.GetWhere(p => p.Id == payLoadTypeId, "LocalizedPayLoadTypes").Select(c => new PayLoadTypeViewModel
            {
                Id = c.Id,
                Name = c.LocalizedPayLoadTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl =  c.ImageUrl
            }).FirstOrDefault();
        }
        #endregion


        #region +++++++++++++++++ VehicleModels Region ++++++++++++++++++++++++++++
        public List<KeyValuePairViewModel> GetVehicleModelsAutoComplete(long makeId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleModels.GetWhere(c => c.VehicleMakeId == makeId, includeProperties: "LocalizedVehicleModels").Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleModels.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
            }).ToList();
        }

        public List<VehicleModelViewModel> GetVehicleModelsList(long makeId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleModels.GetWhere(c => c.VehicleMakeId == makeId, includeProperties: "LocalizedVehicleModels,VehicleMake.LocalizedVehicleMakes").Select(c => new VehicleModelViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleModels.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl =  c.ImageURL
            }).ToList();
        }

        public VehicleModelViewModel GetVehicleModelById(long modelId, string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleModels.GetWhere(c => c.Id == modelId, includeProperties: "LocalizedVehicleModels,VehicleMake.LocalizedVehicleMakes").Select(c => new VehicleModelViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleModels.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl =  c.ImageURL
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
                ImageUrl =  c.ImageUrl,
                Year = c.Year.ToString()
            }).ToList();
        }

        public VehicleModelYearViewModel GetVehicleModelYearsById(long modelId, string cultureCode = "en-US")
        {
            return UnitOfWork.ModelYearCombinations.GetWhere(c => c.VehicleModelId == modelId, includeProperties: "VehicleModel.LocalizedVehicleModels").Select(c => new VehicleModelYearViewModel
            {
                Id = c.Id,
                Model = c.VehicleModel.LocalizedVehicleModels.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl =  c.ImageUrl,
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
                Name = c.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name
            }).ToList();
        }

        //public List<VehicleTypeViewModel> GetVehicleTypesList(string cultureCode = "en-US")
        //{
        //    return UnitOfWork.VehicleTypes.GetAll(includeProperties: "LocalizedVehicleTypes").Select(c => new VehicleTypeViewModel
        //    {
        //        Id = c.Id,
        //        Name = c.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
        //        ImageUrl = c.ImageUrl,
        //        Description = c.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Descreption,
        //        IsEquipment = c.IsEquipment
        //    }).ToList();
        //}

        public List<VehicleTypeCompositeViewModel> GetVehicleTypesList(string cultureCode = "en-US", bool tree = true)
        {

            if (tree)
            {
                return UnitOfWork.VehicleTypes
                    .GetAll(includeProperties: "LocalizedVehicleTypes,VehicleCapacities.LocalizedVehicleCapacities,VehicleTypeConfigurations.LocalizedVehicleConfigurations")
                    .Select(c => new VehicleTypeCompositeViewModel
                    {
                        Id = c.Id,
                        Name = c.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                        ImageUrl =  c.ImageUrl,
                        Description = c.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Descreption,
                        IsEquipment = c.IsEquipment,
                        CapacityViewModel = c.VehicleCapacities.Select(cp => new CapacityViewModel
                        {
                            Id = cp.Id,
                            Weight = cp.Weight,
                            Length = cp.Length,
                            Name = cp.LocalizedVehicleCapacities.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                            MaximumQuantity = cp.MaximumQuantity
                        }).ToList(),

                        ConfigurationsViewModel = c.VehicleTypeConfigurations.Select(vc => new ConfigurationsViewModel
                        {
                            Id = vc.Id,
                            ImageUrl =  vc.ImageUrl,
                            Name = vc.LocalizedVehicleConfigurations.FirstOrDefault(vlc => vlc.CultureCode == cultureCode).Name,
                            Description = vc.LocalizedVehicleConfigurations.FirstOrDefault(vlc => vlc.CultureCode == cultureCode).Descreption
                        }).ToList()
                    }).ToList();
            }
            else
            {
                return UnitOfWork.VehicleTypes
                       .GetAll(includeProperties: "LocalizedVehicleTypes")
                       .Select(c => new VehicleTypeCompositeViewModel
                       {
                           Id = c.Id,
                           Name = c.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                           ImageUrl =  c.ImageUrl,
                           Description = c.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Descreption,
                           IsEquipment = c.IsEquipment
                       }).ToList();

            }
        }


        public VehicleTypeViewModel GetVehicleTypeById(string cultureCode = "en-US")
        {
            return UnitOfWork.VehicleTypes.GetAll(includeProperties: "LocalizedVehicleTypes").Select(c => new VehicleTypeViewModel
            {
                Id = c.Id,
                Name = c.LocalizedVehicleTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl =  c.ImageUrl,
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
            return UnitOfWork.WeightUnits.GetWhere(c => c.Id == weightId).Select(c => new WeightViewModel
            {
                Id = c.Id,
                ShortName = c.LocalizedWeightUnits.FirstOrDefault(lc => lc.CultureCode == cultureCode).ShortName,
                FullName = c.LocalizedWeightUnits.FirstOrDefault(lc => lc.CultureCode == cultureCode).FullName,
                IsMetric = c.IsMetric,
                WeightMultiple = c.WeightMultiple
            }).FirstOrDefault();
        }
        #endregion


        public List<TripTypesViewModel> GetVehicleTripeTypes(string cultureCode = "en-US")
        {
            return UnitOfWork.TripTypes.GetAll().Select(c => new TripTypesViewModel
            {
                Id = c.Id,
                Name = c.LocalizedTripTypes.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                ImageUrl=c.ImageUrl
            }).ToList();
        }
    }
}
