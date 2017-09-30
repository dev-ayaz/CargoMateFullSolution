using CargoMate.DataAccess.Models.BasicData;
using CargoMate.DataAccess.Models.Customers;
using CargoMate.DataAccess.Models.Transporters;
using CargoMate.DataAccess.Models.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoMate.DataAccess.Providers
{
    public class CargoMateDbContext : DbContext
    {
        public bool EnforceConsistency
        {
            get
            {
                var isEnabled = ConfigurationManager.AppSettings["EnforceDbConsistency"];

                return isEnabled != null && Convert.ToBoolean(isEnabled);
            }
        }

        public CargoMateDbContext()
            : base("name=CargoMateDbContext")
        {
        }

        public CargoMateDbContext(string dbConnectionString)
            : base(dbConnectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            Database.CommandTimeout = 300000;

            if (!EnforceConsistency)
            {
                Database.SetInitializer<CargoMateDbContext>(null);
            }
        }


        public static CargoMateDbContext Create()
        {
            return new CargoMateDbContext();
        }



        //************************** Basic Data START ************************************************

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CustomerStatus> CustomerStatuses { get; set; }
        public virtual DbSet<DriverStatus> DriverStatuses { get; set; }
        public virtual DbSet<LengthUnit> LengthUnits { get; set; }
        public virtual DbSet<ModelYearCombination> ModelYearCombinations { get; set; }
        public virtual DbSet<PayLoadType> PayLoadTypes { get; set; }
        public virtual DbSet<TripType> TripTypes { get; set; }
        public virtual DbSet<VehicleCapacity> VehicleCapacities { get; set; }
        public virtual DbSet<VehicleConfiguration> VehicleConfigurations { get; set; }
        public virtual DbSet<VehicleMake> VehicleMakes { get; set; }
        public virtual DbSet<VehicleModel> VehicleModels { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<WeightUnit> WeightUnits { get; set; }

        //************************** Basic Data  END************************************************


        //************************** Customers Data  Start************************************************

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Company> Companies { get; set; }

        //************************** Customers Data  END************************************************

        //************************** Transporters Data  Start************************************************

        public virtual DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public virtual DbSet<DriverLegalDocument> DriverLegalDocuments { get; set; }
        public virtual DbSet<DriverPersonalInfo> DriverPersonalInfos { get; set; }

        //************************** Transporters Data  END************************************************


        //************************** User Data  Start************************************************

        public virtual DbSet<CargoMate.DataAccess.Models.Users.Action> Actions { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuAction> MenuActions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        //************************** User Data  END************************************************

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(e => e.LocalizedCountries)
                .WithOptional(e => e.Country)
                .WillCascadeOnDelete();


            modelBuilder.Entity<LengthUnit>()
                .HasMany(e => e.LocalizedLengthUnits)
                .WithOptional(e => e.LengthUnit)
                .HasForeignKey(e => e.LengthUnitId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<VehicleMake>()
                .HasMany(e => e.LocalizedVehicleMakes)
                .WithOptional(e => e.VehicleMake)
                .WillCascadeOnDelete();

            modelBuilder.Entity<VehicleMake>()
                .HasMany(e => e.VehicleModels)
                .WithOptional(e => e.VehicleMake)
                .HasForeignKey(e => e.VehicleMakeId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PayLoadType>()
                .HasMany(e => e.LocalizedPayLoadTypes)
                .WithOptional(e => e.PayLoadType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TripType>()
                .HasMany(e => e.LocalizedTripTypes)
                .WithOptional(e => e.TripType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<VehicleCapacity>()
                .HasMany(e => e.LocalizedVehicleCapacities)
                .WithOptional(e => e.VehicleCapacity)
                .HasForeignKey(e => e.VehicleCapacityId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<VehicleConfiguration>()
                .HasMany(e => e.LocalizedVehicleConfigurations)
                .WithOptional(e => e.VehicleConfiguration)
                .HasForeignKey(e => e.VehicleConfigurationId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<VehicleModel>()
                .HasMany(e => e.LocalizedVehicleModels)
                .WithOptional(e => e.VehicleModel)
                .HasForeignKey(e => e.VehicleModelId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<VehicleType>()
                .HasMany(e => e.LocalizedVehicleTypes)
                .WithOptional(e => e.VehicleType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WeightUnit>()
                .HasMany(e => e.LocalizedWeightUnits)
                .WithOptional(e => e.WeightUnit)
                .WillCascadeOnDelete();

            modelBuilder.Entity<InsuranceCompany>()
                .HasMany(e => e.LocalizedInsuranceCompanies)
                .WithOptional(e => e.InsuranceCompany)
                .HasForeignKey(e => e.InsuranceCompanyId)
                .WillCascadeOnDelete();
        }

    }
}