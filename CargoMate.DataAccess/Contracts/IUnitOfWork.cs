using CargoMate.DataAccess.Models.BasicData;
using CargoMate.DataAccess.Models.BasicData.Localizations;
using CargoMate.DataAccess.Models.Customers;
using CargoMate.DataAccess.Models.Transporters;
using CargoMate.DataAccess.Models.Transporters.Localization;
using CargoMate.DataAccess.Models.Users;
using CargoMate.DataAccess.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Contracts
{
    public interface IUnitOfWork : IBaseUnitOfWork, IDisposable
    {
        //************************** Basic Data START ************************************************
        IRepository<Country> Countries { get; }
        IRepository<CustomerStatus> CustomerStatuses { get; }
        IRepository<DriverStatus> DriverStatuses { get; }
        IRepository<LengthUnit> LengthUnits { get; }
        IRepository<ModelYearCombination> ModelYearCombinations { get; }
        IRepository<PayLoadType> PayLoadTypes { get; }
        IRepository<TripType> TripTypes { get; }
        IRepository<VehicleCapacity> VehicleCapacities { get; }
        IRepository<VehicleConfiguration> VehicleConfigurations { get; }
        IRepository<VehicleMake> VehicleMakes { get; }
        IRepository<VehicleModel> VehicleModels { get; }
        IRepository<VehicleType> VehicleTypes { get; }
        IRepository<WeightUnit> WeightUnits { get; }
        IRepository<FareType> FareTypes { get; }


        IRepository<LocalizedCountry> LocalizedCountries { get; }
        IRepository<LocalizedCustomerStatus> LocalizedCustomerStatuses { get; }
        IRepository<LocalizedDriverStatus> LocalizedDriverStatuses { get; }
        IRepository<LocalizedLengthUnit> LocalizedLengthUnits { get; }
        IRepository<LocalizedPayLoadType> LocalizedPayLoadTypes { get; }
        IRepository<LocalizedTripType> LocalizedTripTypes { get; }
        IRepository<LocalizedVehicleCapacity> LocalizedVehicleCapacities { get; }
        IRepository<LocalizedVehicleConfiguration> LocalizedVehicleConfigurations { get; }
        IRepository<LocalizedVehicleMake> LocalizedVehicleMakes { get; }
        IRepository<LocalizedVehicleModel> LocalizedVehicleModels { get; }
        IRepository<LocalizedVehicleType> LocalizedVehicleTypes { get; }
        IRepository<LocalizedWeightUnit> LocalizedWeightUnits { get; }
        IRepository<LocalizedFareType> LocalizedFareTypes { get; }
        //************************** Basic Data  END************************************************


        //************************** Customers Data  Start************************************************

        IRepository<Customer> Customers { get; }
        IRepository<Company> Companies { get; }

        //************************** Customers Data  END************************************************

        //************************** Transporters Data  Start************************************************

        IRepository<InsuranceCompany> InsuranceCompanies { get; }
        IRepository<LocalizedInsuranceCompany> LocalizedInsuranceCompanies { get; }        
        IRepository<DriverLegalDocument> DriverLegalDocuments { get; }
        IRepository<DriverPersonalInfo> DriverPersonalInfos { get; }
        IRepository<DriverFareType> DriverFareTypes { get; }

        IRepository<Vehicle> Vehicles { get; }
        IRepository<VehicleImage> VehicleImages { get; }
        IRepository<VehiclePayloadType> VehiclePayloadTypes { get; }
        IRepository<VehicleTripType> VehicleTripTypes { get; }
        //************************** Transporters Data  END************************************************

        IRepository<UserRole> UserRoles { get; }
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<Menu> Menus { get; }

        IRepository<CargoMate.DataAccess.Models.Users.Action> Actions { get; }

        IRepository<MenuAction> MenuActions { get; }

        IRepository<RolePermission> RolePermissions { get; }

    }

}
