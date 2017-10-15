using CargoMate.DataAccess.Contracts;
using CargoMate.DataAccess.Models.BasicData;
using CargoMate.DataAccess.Models.BasicData.Localizations;
using CargoMate.DataAccess.Models.Customers;
using CargoMate.DataAccess.Models.Transporters;
using CargoMate.DataAccess.Models.Users;
using CargoMate.DataAccess.Models.Vehicles;
using CargoMate.DataAccess.Providers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Providers
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {

        public UnitOfWork(IRepositoryProvider repositoryProvider) : this(repositoryProvider, "CargoMateDbContext") { }

        public UnitOfWork(IRepositoryProvider repositoryProvider, string dbConnectionString) : base(repositoryProvider, dbConnectionString)
        {
        }

        protected override void CreateDbContext(string dbConnectionString)
        {
            DbContext = string.IsNullOrEmpty(dbConnectionString) ? new CargoMateDbContext() : new CargoMateDbContext(dbConnectionString);
        }

        //************************** Basic Data START ************************************************

        public IRepository<Country> Countries => GetStandardRepository<Country>();
        public IRepository<CustomerStatus> CustomerStatuses => GetStandardRepository<CustomerStatus>();
        public IRepository<DriverStatus> DriverStatuses => GetStandardRepository<DriverStatus>();
        public IRepository<LengthUnit> LengthUnits => GetStandardRepository<LengthUnit>();
        public IRepository<ModelYearCombination> ModelYearCombinations => GetStandardRepository<ModelYearCombination>();
        public IRepository<PayLoadType> PayLoadTypes => GetStandardRepository<PayLoadType>();
        public IRepository<TripType> TripTypes => GetStandardRepository<TripType>();
        public IRepository<VehicleCapacity> VehicleCapacities => GetStandardRepository<VehicleCapacity>();
        public IRepository<VehicleConfiguration> VehicleConfigurations => GetStandardRepository<VehicleConfiguration>();
        public IRepository<VehicleMake> VehicleMakes => GetStandardRepository<VehicleMake>();
        public IRepository<VehicleModel> VehicleModels => GetStandardRepository<VehicleModel>();
        public IRepository<VehicleType> VehicleTypes => GetStandardRepository<VehicleType>();
        public IRepository<WeightUnit> WeightUnits => GetStandardRepository<WeightUnit>();
        public IRepository<FareType> FareTypes => GetStandardRepository<FareType>();


        public IRepository<LocalizedCountry> LocalizedCountries => GetStandardRepository<LocalizedCountry>();
        public IRepository<LocalizedCustomerStatus> LocalizedCustomerStatuses => GetStandardRepository<LocalizedCustomerStatus>();
        public IRepository<LocalizedDriverStatus> LocalizedDriverStatuses => GetStandardRepository<LocalizedDriverStatus>();
        public IRepository<LocalizedLengthUnit> LocalizedLengthUnits => GetStandardRepository<LocalizedLengthUnit>();
        public IRepository<LocalizedPayLoadType> LocalizedPayLoadTypes => GetStandardRepository<LocalizedPayLoadType>();
        public IRepository<LocalizedTripType> LocalizedTripTypes => GetStandardRepository<LocalizedTripType>();
        public IRepository<LocalizedVehicleCapacity> LocalizedVehicleCapacities => GetStandardRepository<LocalizedVehicleCapacity>();
        public IRepository<LocalizedVehicleConfiguration> LocalizedVehicleConfigurations => GetStandardRepository<LocalizedVehicleConfiguration>();
        public IRepository<LocalizedVehicleMake> LocalizedVehicleMakes => GetStandardRepository<LocalizedVehicleMake>();
        public IRepository<LocalizedVehicleModel> LocalizedVehicleModels => GetStandardRepository<LocalizedVehicleModel>();
        public IRepository<LocalizedVehicleType> LocalizedVehicleTypes => GetStandardRepository<LocalizedVehicleType>();
        public IRepository<LocalizedWeightUnit> LocalizedWeightUnits => GetStandardRepository<LocalizedWeightUnit>();
        public IRepository<LocalizedFareType> LocalizedFareTypes => GetStandardRepository<LocalizedFareType>();
        //************************** Basic Data  END************************************************


        //************************** Customers Data  Start************************************************

        public IRepository<Customer> Customers => GetStandardRepository<Customer>();
        public IRepository<Company> Companies => GetStandardRepository<Company>();

        //************************** Customers Data  END************************************************

        //************************** Transporters Data  Start************************************************

        public IRepository<InsuranceCompany> InsuranceCompanies => GetStandardRepository<InsuranceCompany>();
        public IRepository<DriverLegalDocument> DriverLegalDocuments => GetStandardRepository<DriverLegalDocument>();
        public IRepository<DriverPersonalInfo> DriverPersonalInfos => GetStandardRepository<DriverPersonalInfo>();
        public IRepository<DriverFareType> DriverFareTypes => GetStandardRepository<DriverFareType>();

        public IRepository<Vehicle> Vehicles => GetStandardRepository<Vehicle>();
        public IRepository<VehicleImage> VehicleImages => GetStandardRepository<VehicleImage>();
        public IRepository<VehiclePayloadType> VehiclePayloadTypes => GetStandardRepository<VehiclePayloadType>();
        public IRepository<VehicleTripType> VehicleTripTypes => GetStandardRepository<VehicleTripType>();

        //************************** Transporters Data  END************************************************

        public IRepository<UserRole> UserRoles => GetStandardRepository<UserRole>();
        public IRepository<User> Users => GetStandardRepository<User>();
        public IRepository<Role> Roles => GetStandardRepository<Role>();
        public IRepository<Menu> Menus => GetStandardRepository<Menu>();
        public IRepository<Models.Users.Action> Actions => GetStandardRepository<Models.Users.Action>();
        public IRepository<MenuAction> MenuActions => GetStandardRepository<MenuAction>();
        public IRepository<RolePermission> RolePermissions => GetStandardRepository<RolePermission>();

    }
}
