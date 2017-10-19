namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBReset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "User.Actions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Key = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "User.MenuActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(),
                        ActionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.Actions", t => t.ActionId)
                .ForeignKey("User.Menus", t => t.MenuId)
                .Index(t => t.MenuId)
                .Index(t => t.ActionId);
            
            CreateTable(
                "User.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Link = c.String(),
                        JsFunction = c.String(),
                        Icon = c.String(),
                        IsSidebarMenu = c.Boolean(nullable: false),
                        MenuKey = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.Menus", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "User.RolePermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuActionId = c.Int(),
                        RoleId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.MenuActions", t => t.MenuActionId)
                .ForeignKey("User.Roles", t => t.RoleId)
                .Index(t => t.MenuActionId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "User.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "User.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Role_Id = c.String(maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                        Role1_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                        User_Id1 = c.String(maxLength: 128),
                        Role_Id1 = c.String(maxLength: 128),
                        Role_Id2 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("User.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("User.Roles", t => t.Role1_Id)
                .ForeignKey("User.Users", t => t.User_Id)
                .ForeignKey("User.Users", t => t.User_Id1)
                .ForeignKey("User.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("User.Roles", t => t.Role_Id1)
                .ForeignKey("User.Roles", t => t.Role_Id2)
                .Index(t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.Role1_Id)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1)
                .Index(t => t.Role_Id1)
                .Index(t => t.Role_Id2);
            
            CreateTable(
                "User.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ImageUrl = c.String(),
                        IsActive = c.Boolean(),
                        CreationDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        UserRole_UserId = c.String(maxLength: 128),
                        UserRole_RoleId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.UserRoles", t => new { t.UserRole_UserId, t.UserRole_RoleId })
                .Index(t => new { t.UserRole_UserId, t.UserRole_RoleId });
            
            CreateTable(
                "User.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "User.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("User.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Customers.Companies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Location = c.Geography(),
                        PhoneNumber = c.String(maxLength: 20),
                        CrNumber = c.Long(),
                        Logo = c.String(),
                        WebSiteUrl = c.String(maxLength: 250),
                        CountryId = c.Long(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "BasicData.Countries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CurrencySymbol = c.String(maxLength: 50),
                        PhonCode = c.String(maxLength: 10),
                        Flag = c.String(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Transporters.DriverPersonalInfos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        Name = c.String(maxLength: 250),
                        LegalName = c.String(maxLength: 250),
                        DateOfBirth = c.DateTime(storeType: "date"),
                        PhoneNumber = c.String(maxLength: 20),
                        EmailAddress = c.String(maxLength: 50),
                        ImageUrl = c.String(),
                        CountryId = c.Long(),
                        Gender = c.Boolean(),
                        IsPhoneNumberVerified = c.Boolean(),
                        Rating = c.Decimal(precision: 18, scale: 2),
                        TotalTrips = c.Long(),
                        FixedRate = c.Boolean(),
                        IdVerified = c.Boolean(),
                        MembershipDate = c.DateTime(),
                        IsValidated = c.Boolean(),
                        Location = c.Geography(),
                        Locality = c.String(),
                        SubLocality = c.String(),
                        DriverStatusId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.Countries", t => t.CountryId)
                .ForeignKey("BasicData.DriverStatuses", t => t.DriverStatusId)
                .Index(t => t.CountryId)
                .Index(t => t.DriverStatusId);
            
            CreateTable(
                "Transporters.DriverFareTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FareTypeId = c.Long(),
                        DriverPersonalInfoId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Transporters.DriverPersonalInfos", t => t.DriverPersonalInfoId)
                .ForeignKey("BasicData.FareTypes", t => t.FareTypeId)
                .Index(t => t.FareTypeId)
                .Index(t => t.DriverPersonalInfoId);
            
            CreateTable(
                "BasicData.FareTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BasicData.LocalizedFareTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        FareTypeId = c.Long(),
                        CultureCode = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.FareTypes", t => t.FareTypeId)
                .Index(t => t.FareTypeId);
            
            CreateTable(
                "Transporters.DriverLegalDocuments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        LicenseNumber = c.String(maxLength: 50),
                        LicenseExpiryDate = c.DateTime(storeType: "date"),
                        LicenseImage = c.String(),
                        ResidenceNumber = c.String(maxLength: 50),
                        ResidenceExpiryDate = c.DateTime(storeType: "date"),
                        ResidenceImage = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Transporters.DriverPersonalInfos", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "BasicData.DriverStatuses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "[BasicData.Localization].DriverStatuses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        DriverStatusId = c.Long(),
                        CultureCode = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.DriverStatuses", t => t.DriverStatusId)
                .Index(t => t.DriverStatusId);
            
            CreateTable(
                "Transporters.PreferredAddresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CountryId = c.Long(nullable: false),
                        Locality = c.String(maxLength: 50),
                        SubLocality = c.String(maxLength: 50),
                        Location = c.Geography(),
                        DriverPersonalInfoId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("Transporters.DriverPersonalInfos", t => t.DriverPersonalInfoId)
                .Index(t => t.CountryId)
                .Index(t => t.DriverPersonalInfoId);
            
            CreateTable(
                "[BasicData.Localization].LocalizedCountries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        CountryCode = c.String(maxLength: 50),
                        CurrencyLong = c.String(maxLength: 50),
                        CurrencyCode = c.String(maxLength: 50),
                        CountryId = c.Long(),
                        CultureCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "BasicData.VehicleMakes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CountryId = c.Long(),
                        ImageUrl = c.String(maxLength: 500),
                        IsActive = c.Boolean(),
                        VehicleTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.Countries", t => t.CountryId)
                .ForeignKey("BasicData.VehicleTypes", t => t.VehicleTypeId)
                .Index(t => t.CountryId)
                .Index(t => t.VehicleTypeId);
            
            CreateTable(
                "[BasicData.Localization].LocalizedMakes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        MakeId = c.Long(),
                        CultureCode = c.String(maxLength: 10),
                        VehicleMake_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.VehicleMakes", t => t.VehicleMake_Id, cascadeDelete: true)
                .Index(t => t.VehicleMake_Id);
            
            CreateTable(
                "BasicData.VehicleModel",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageURL = c.String(maxLength: 500),
                        VehicleMakeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.VehicleMakes", t => t.VehicleMakeId, cascadeDelete: true)
                .Index(t => t.VehicleMakeId);
            
            CreateTable(
                "[BasicData.Localization].LocalizedVehicleModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        VehicleModelId = c.Long(),
                        CultureCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.VehicleModel", t => t.VehicleModelId, cascadeDelete: true)
                .Index(t => t.VehicleModelId);
            
            CreateTable(
                "BasicData.ModelYearCombinations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VehicleModelId = c.Long(),
                        Year = c.Long(nullable: false),
                        ImageUrl = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.VehicleModel", t => t.VehicleModelId)
                .Index(t => t.VehicleModelId);
            
            CreateTable(
                "BasicData.VehicleTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        IsEquipment = c.Boolean(),
                        Source = c.String(maxLength: 250),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "[BasicData.Localization].LocalizedVehicleTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        Descreption = c.String(),
                        CultureCode = c.String(maxLength: 10),
                        VehicleTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.VehicleTypes", t => t.VehicleTypeId, cascadeDelete: true)
                .Index(t => t.VehicleTypeId);
            
            CreateTable(
                "BasicData.PayLoadTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VehicleTypeId = c.Long(),
                        ImageUrl = c.String(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.VehicleTypes", t => t.VehicleTypeId)
                .Index(t => t.VehicleTypeId);
            
            CreateTable(
                "[BasicData.Localization].LocalizedPayLoadType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        CultureCode = c.String(maxLength: 10),
                        PayLoadTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.PayLoadTypes", t => t.PayLoadTypeId, cascadeDelete: true)
                .Index(t => t.PayLoadTypeId);
            
            CreateTable(
                "BasicData.VehicleCapacities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Capacity = c.Long(),
                        Length = c.Long(),
                        PalletNumber = c.Long(),
                        CultureCode = c.String(maxLength: 10),
                        VehicleTypeId = c.Long(),
                        Source = c.String(maxLength: 250),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.VehicleTypes", t => t.VehicleTypeId)
                .Index(t => t.VehicleTypeId);
            
            CreateTable(
                "[BasicData.Localization].LocalizedVehicleCapacities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        CultureCode = c.String(maxLength: 10),
                        VehicleCapacityId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.VehicleCapacities", t => t.VehicleCapacityId, cascadeDelete: true)
                .Index(t => t.VehicleCapacityId);
            
            CreateTable(
                "BasicData.VehicleConfigurations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        VehicleTypeId = c.Long(),
                        Source = c.String(maxLength: 50),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.VehicleTypes", t => t.VehicleTypeId)
                .Index(t => t.VehicleTypeId);
            
            CreateTable(
                "[BasicData.Localization].LocalizedVehicleConfiguration",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        Descreption = c.String(),
                        CultureCode = c.String(maxLength: 10),
                        VehicleConfigurationId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.VehicleConfigurations", t => t.VehicleConfigurationId, cascadeDelete: true)
                .Index(t => t.VehicleConfigurationId);
            
            CreateTable(
                "Customers.Customers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        CustomerId = c.String(nullable: false, maxLength: 50),
                        EmailAddress = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 20),
                        ImageUrl = c.String(),
                        DateOfBirth = c.DateTime(storeType: "date"),
                        Gender = c.Boolean(),
                        Address = c.String(),
                        Location = c.Geography(),
                        Rating = c.Decimal(precision: 18, scale: 2),
                        CompanyId = c.Long(),
                        IsPhoneVerified = c.Boolean(),
                        CustomerStatusId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Customers.Companies", t => t.CompanyId)
                .ForeignKey("BasicData.CustomerStatuses", t => t.CustomerStatusId)
                .Index(t => t.CompanyId)
                .Index(t => t.CustomerStatusId);
            
            CreateTable(
                "BasicData.CustomerStatuses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "[BasicData.Localization].CustomerStatuses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CustomerStatusId = c.Long(),
                        CultureCode = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.CustomerStatuses", t => t.CustomerStatusId)
                .Index(t => t.CustomerStatusId);
            
            CreateTable(
                "Transporters.InsuranceCompanies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PhoneNumber = c.String(maxLength: 20),
                        FaxNumber = c.String(maxLength: 20),
                        WebSite = c.String(maxLength: 50),
                        CountryId = c.Long(),
                        Address = c.String(maxLength: 500),
                        Location = c.Geography(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "[Transporters.Localization].LocalizedInsuranceCompanies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Address = c.String(maxLength: 500),
                        CultureCode = c.String(maxLength: 20),
                        InsuranceCompanyId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Transporters.InsuranceCompanies", t => t.InsuranceCompanyId, cascadeDelete: true)
                .Index(t => t.InsuranceCompanyId);
            
            CreateTable(
                "BasicData.LengthUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsMetric = c.Boolean(),
                        LengthMultiple = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "[BasicData.Localization].LocalizedLengthUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ShortName = c.String(maxLength: 100),
                        FullName = c.String(maxLength: 500),
                        CultureCode = c.String(maxLength: 10),
                        LengthUnitId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.LengthUnits", t => t.LengthUnitId, cascadeDelete: true)
                .Index(t => t.LengthUnitId);
            
            CreateTable(
                "BasicData.TripTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "[BasicData.Localization].LocalizedTripTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        TripTypeId = c.Long(),
                        CultureCode = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.TripTypes", t => t.TripTypeId, cascadeDelete: true)
                .Index(t => t.TripTypeId);
            
            CreateTable(
                "Vehicles.VehicleImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VehicleId = c.Long(),
                        ImageUrl = c.String(),
                        ImageTitle = c.String(),
                        CreationDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Vehicles.Vehicles", t => t.VehicleId)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "Vehicles.Vehicles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ModelYearCombinationId = c.Long(),
                        VehicleConfigurationId = c.Long(),
                        VehicleCapacityId = c.Long(),
                        PlateNumber = c.String(maxLength: 50),
                        EngineNumber = c.String(maxLength: 50),
                        RegistrationNumber = c.String(maxLength: 50),
                        RegistrationImage = c.String(),
                        RegistrationExpiry = c.DateTime(),
                        IsInsured = c.Boolean(),
                        InsuranceCompanyId = c.Long(),
                        InsuranceAmount = c.Decimal(precision: 18, scale: 2),
                        PolicyNumber = c.String(),
                        InsuranceExpirey = c.DateTime(),
                        DriverPersonalInfoId = c.String(maxLength: 50),
                        IsVerified = c.Boolean(),
                        Status = c.Int(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Transporters.DriverPersonalInfos", t => t.DriverPersonalInfoId)
                .ForeignKey("Transporters.InsuranceCompanies", t => t.InsuranceCompanyId)
                .ForeignKey("BasicData.ModelYearCombinations", t => t.ModelYearCombinationId)
                .ForeignKey("BasicData.VehicleCapacities", t => t.VehicleCapacityId)
                .ForeignKey("BasicData.VehicleConfigurations", t => t.VehicleConfigurationId)
                .Index(t => t.ModelYearCombinationId)
                .Index(t => t.VehicleConfigurationId)
                .Index(t => t.VehicleCapacityId)
                .Index(t => t.InsuranceCompanyId)
                .Index(t => t.DriverPersonalInfoId);
            
            CreateTable(
                "Vehicles.VehiclePayloadTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VehicleId = c.Long(nullable: false),
                        PayLoadTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.PayLoadTypes", t => t.PayLoadTypeId)
                .ForeignKey("Vehicles.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.PayLoadTypeId);
            
            CreateTable(
                "Vehicles.VehicleTripTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VehicleId = c.Long(nullable: false),
                        TripTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.TripTypes", t => t.TripTypeId)
                .ForeignKey("Vehicles.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.TripTypeId);
            
            CreateTable(
                "BasicData.WeightUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsMetric = c.Boolean(),
                        WeightMultiple = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "[BasicData.Localization].LocalizedWeightUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ShortName = c.String(maxLength: 100),
                        FullName = c.String(maxLength: 500),
                        WeightUnitId = c.Long(),
                        CultureCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.WeightUnits", t => t.WeightUnitId, cascadeDelete: true)
                .Index(t => t.WeightUnitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("[BasicData.Localization].LocalizedWeightUnits", "WeightUnitId", "BasicData.WeightUnits");
            DropForeignKey("Vehicles.VehicleTripTypes", "VehicleId", "Vehicles.Vehicles");
            DropForeignKey("Vehicles.VehicleTripTypes", "TripTypeId", "BasicData.TripTypes");
            DropForeignKey("Vehicles.VehiclePayloadTypes", "VehicleId", "Vehicles.Vehicles");
            DropForeignKey("Vehicles.VehiclePayloadTypes", "PayLoadTypeId", "BasicData.PayLoadTypes");
            DropForeignKey("Vehicles.VehicleImages", "VehicleId", "Vehicles.Vehicles");
            DropForeignKey("Vehicles.Vehicles", "VehicleConfigurationId", "BasicData.VehicleConfigurations");
            DropForeignKey("Vehicles.Vehicles", "VehicleCapacityId", "BasicData.VehicleCapacities");
            DropForeignKey("Vehicles.Vehicles", "ModelYearCombinationId", "BasicData.ModelYearCombinations");
            DropForeignKey("Vehicles.Vehicles", "InsuranceCompanyId", "Transporters.InsuranceCompanies");
            DropForeignKey("Vehicles.Vehicles", "DriverPersonalInfoId", "Transporters.DriverPersonalInfos");
            DropForeignKey("[BasicData.Localization].LocalizedTripTypes", "TripTypeId", "BasicData.TripTypes");
            DropForeignKey("[BasicData.Localization].LocalizedLengthUnits", "LengthUnitId", "BasicData.LengthUnits");
            DropForeignKey("[Transporters.Localization].LocalizedInsuranceCompanies", "InsuranceCompanyId", "Transporters.InsuranceCompanies");
            DropForeignKey("[BasicData.Localization].CustomerStatuses", "CustomerStatusId", "BasicData.CustomerStatuses");
            DropForeignKey("Customers.Customers", "CustomerStatusId", "BasicData.CustomerStatuses");
            DropForeignKey("Customers.Customers", "CompanyId", "Customers.Companies");
            DropForeignKey("BasicData.VehicleConfigurations", "VehicleTypeId", "BasicData.VehicleTypes");
            DropForeignKey("[BasicData.Localization].LocalizedVehicleConfiguration", "VehicleConfigurationId", "BasicData.VehicleConfigurations");
            DropForeignKey("BasicData.VehicleMakes", "VehicleTypeId", "BasicData.VehicleTypes");
            DropForeignKey("BasicData.VehicleCapacities", "VehicleTypeId", "BasicData.VehicleTypes");
            DropForeignKey("[BasicData.Localization].LocalizedVehicleCapacities", "VehicleCapacityId", "BasicData.VehicleCapacities");
            DropForeignKey("BasicData.PayLoadTypes", "VehicleTypeId", "BasicData.VehicleTypes");
            DropForeignKey("[BasicData.Localization].LocalizedPayLoadType", "PayLoadTypeId", "BasicData.PayLoadTypes");
            DropForeignKey("[BasicData.Localization].LocalizedVehicleTypes", "VehicleTypeId", "BasicData.VehicleTypes");
            DropForeignKey("BasicData.VehicleModel", "VehicleMakeId", "BasicData.VehicleMakes");
            DropForeignKey("BasicData.ModelYearCombinations", "VehicleModelId", "BasicData.VehicleModel");
            DropForeignKey("[BasicData.Localization].LocalizedVehicleModels", "VehicleModelId", "BasicData.VehicleModel");
            DropForeignKey("[BasicData.Localization].LocalizedMakes", "VehicleMake_Id", "BasicData.VehicleMakes");
            DropForeignKey("BasicData.VehicleMakes", "CountryId", "BasicData.Countries");
            DropForeignKey("[BasicData.Localization].LocalizedCountries", "CountryId", "BasicData.Countries");
            DropForeignKey("Transporters.PreferredAddresses", "DriverPersonalInfoId", "Transporters.DriverPersonalInfos");
            DropForeignKey("Transporters.PreferredAddresses", "CountryId", "BasicData.Countries");
            DropForeignKey("[BasicData.Localization].DriverStatuses", "DriverStatusId", "BasicData.DriverStatuses");
            DropForeignKey("Transporters.DriverPersonalInfos", "DriverStatusId", "BasicData.DriverStatuses");
            DropForeignKey("Transporters.DriverLegalDocuments", "Id", "Transporters.DriverPersonalInfos");
            DropForeignKey("BasicData.LocalizedFareTypes", "FareTypeId", "BasicData.FareTypes");
            DropForeignKey("Transporters.DriverFareTypes", "FareTypeId", "BasicData.FareTypes");
            DropForeignKey("Transporters.DriverFareTypes", "DriverPersonalInfoId", "Transporters.DriverPersonalInfos");
            DropForeignKey("Transporters.DriverPersonalInfos", "CountryId", "BasicData.Countries");
            DropForeignKey("Customers.Companies", "CountryId", "BasicData.Countries");
            DropForeignKey("User.UserRoles", "Role_Id2", "User.Roles");
            DropForeignKey("User.UserRoles", "Role_Id1", "User.Roles");
            DropForeignKey("User.Users", new[] { "UserRole_UserId", "UserRole_RoleId" }, "User.UserRoles");
            DropForeignKey("User.UserRoles", "UserId", "User.Users");
            DropForeignKey("User.UserRoles", "User_Id1", "User.Users");
            DropForeignKey("User.UserRoles", "User_Id", "User.Users");
            DropForeignKey("User.UserLogins", "UserId", "User.Users");
            DropForeignKey("User.UserClaims", "UserId", "User.Users");
            DropForeignKey("User.UserRoles", "Role1_Id", "User.Roles");
            DropForeignKey("User.UserRoles", "RoleId", "User.Roles");
            DropForeignKey("User.RolePermissions", "RoleId", "User.Roles");
            DropForeignKey("User.RolePermissions", "MenuActionId", "User.MenuActions");
            DropForeignKey("User.MenuActions", "MenuId", "User.Menus");
            DropForeignKey("User.Menus", "ParentId", "User.Menus");
            DropForeignKey("User.MenuActions", "ActionId", "User.Actions");
            DropIndex("[BasicData.Localization].LocalizedWeightUnits", new[] { "WeightUnitId" });
            DropIndex("Vehicles.VehicleTripTypes", new[] { "TripTypeId" });
            DropIndex("Vehicles.VehicleTripTypes", new[] { "VehicleId" });
            DropIndex("Vehicles.VehiclePayloadTypes", new[] { "PayLoadTypeId" });
            DropIndex("Vehicles.VehiclePayloadTypes", new[] { "VehicleId" });
            DropIndex("Vehicles.Vehicles", new[] { "DriverPersonalInfoId" });
            DropIndex("Vehicles.Vehicles", new[] { "InsuranceCompanyId" });
            DropIndex("Vehicles.Vehicles", new[] { "VehicleCapacityId" });
            DropIndex("Vehicles.Vehicles", new[] { "VehicleConfigurationId" });
            DropIndex("Vehicles.Vehicles", new[] { "ModelYearCombinationId" });
            DropIndex("Vehicles.VehicleImages", new[] { "VehicleId" });
            DropIndex("[BasicData.Localization].LocalizedTripTypes", new[] { "TripTypeId" });
            DropIndex("[BasicData.Localization].LocalizedLengthUnits", new[] { "LengthUnitId" });
            DropIndex("[Transporters.Localization].LocalizedInsuranceCompanies", new[] { "InsuranceCompanyId" });
            DropIndex("[BasicData.Localization].CustomerStatuses", new[] { "CustomerStatusId" });
            DropIndex("Customers.Customers", new[] { "CustomerStatusId" });
            DropIndex("Customers.Customers", new[] { "CompanyId" });
            DropIndex("[BasicData.Localization].LocalizedVehicleConfiguration", new[] { "VehicleConfigurationId" });
            DropIndex("BasicData.VehicleConfigurations", new[] { "VehicleTypeId" });
            DropIndex("[BasicData.Localization].LocalizedVehicleCapacities", new[] { "VehicleCapacityId" });
            DropIndex("BasicData.VehicleCapacities", new[] { "VehicleTypeId" });
            DropIndex("[BasicData.Localization].LocalizedPayLoadType", new[] { "PayLoadTypeId" });
            DropIndex("BasicData.PayLoadTypes", new[] { "VehicleTypeId" });
            DropIndex("[BasicData.Localization].LocalizedVehicleTypes", new[] { "VehicleTypeId" });
            DropIndex("BasicData.ModelYearCombinations", new[] { "VehicleModelId" });
            DropIndex("[BasicData.Localization].LocalizedVehicleModels", new[] { "VehicleModelId" });
            DropIndex("BasicData.VehicleModel", new[] { "VehicleMakeId" });
            DropIndex("[BasicData.Localization].LocalizedMakes", new[] { "VehicleMake_Id" });
            DropIndex("BasicData.VehicleMakes", new[] { "VehicleTypeId" });
            DropIndex("BasicData.VehicleMakes", new[] { "CountryId" });
            DropIndex("[BasicData.Localization].LocalizedCountries", new[] { "CountryId" });
            DropIndex("Transporters.PreferredAddresses", new[] { "DriverPersonalInfoId" });
            DropIndex("Transporters.PreferredAddresses", new[] { "CountryId" });
            DropIndex("[BasicData.Localization].DriverStatuses", new[] { "DriverStatusId" });
            DropIndex("Transporters.DriverLegalDocuments", new[] { "Id" });
            DropIndex("BasicData.LocalizedFareTypes", new[] { "FareTypeId" });
            DropIndex("Transporters.DriverFareTypes", new[] { "DriverPersonalInfoId" });
            DropIndex("Transporters.DriverFareTypes", new[] { "FareTypeId" });
            DropIndex("Transporters.DriverPersonalInfos", new[] { "DriverStatusId" });
            DropIndex("Transporters.DriverPersonalInfos", new[] { "CountryId" });
            DropIndex("Customers.Companies", new[] { "CountryId" });
            DropIndex("User.UserLogins", new[] { "UserId" });
            DropIndex("User.UserClaims", new[] { "UserId" });
            DropIndex("User.Users", new[] { "UserRole_UserId", "UserRole_RoleId" });
            DropIndex("User.UserRoles", new[] { "Role_Id2" });
            DropIndex("User.UserRoles", new[] { "Role_Id1" });
            DropIndex("User.UserRoles", new[] { "User_Id1" });
            DropIndex("User.UserRoles", new[] { "User_Id" });
            DropIndex("User.UserRoles", new[] { "Role1_Id" });
            DropIndex("User.UserRoles", new[] { "RoleId" });
            DropIndex("User.UserRoles", new[] { "UserId" });
            DropIndex("User.RolePermissions", new[] { "RoleId" });
            DropIndex("User.RolePermissions", new[] { "MenuActionId" });
            DropIndex("User.Menus", new[] { "ParentId" });
            DropIndex("User.MenuActions", new[] { "ActionId" });
            DropIndex("User.MenuActions", new[] { "MenuId" });
            DropTable("[BasicData.Localization].LocalizedWeightUnits");
            DropTable("BasicData.WeightUnits");
            DropTable("Vehicles.VehicleTripTypes");
            DropTable("Vehicles.VehiclePayloadTypes");
            DropTable("Vehicles.Vehicles");
            DropTable("Vehicles.VehicleImages");
            DropTable("[BasicData.Localization].LocalizedTripTypes");
            DropTable("BasicData.TripTypes");
            DropTable("[BasicData.Localization].LocalizedLengthUnits");
            DropTable("BasicData.LengthUnits");
            DropTable("[Transporters.Localization].LocalizedInsuranceCompanies");
            DropTable("Transporters.InsuranceCompanies");
            DropTable("[BasicData.Localization].CustomerStatuses");
            DropTable("BasicData.CustomerStatuses");
            DropTable("Customers.Customers");
            DropTable("[BasicData.Localization].LocalizedVehicleConfiguration");
            DropTable("BasicData.VehicleConfigurations");
            DropTable("[BasicData.Localization].LocalizedVehicleCapacities");
            DropTable("BasicData.VehicleCapacities");
            DropTable("[BasicData.Localization].LocalizedPayLoadType");
            DropTable("BasicData.PayLoadTypes");
            DropTable("[BasicData.Localization].LocalizedVehicleTypes");
            DropTable("BasicData.VehicleTypes");
            DropTable("BasicData.ModelYearCombinations");
            DropTable("[BasicData.Localization].LocalizedVehicleModels");
            DropTable("BasicData.VehicleModel");
            DropTable("[BasicData.Localization].LocalizedMakes");
            DropTable("BasicData.VehicleMakes");
            DropTable("[BasicData.Localization].LocalizedCountries");
            DropTable("Transporters.PreferredAddresses");
            DropTable("[BasicData.Localization].DriverStatuses");
            DropTable("BasicData.DriverStatuses");
            DropTable("Transporters.DriverLegalDocuments");
            DropTable("BasicData.LocalizedFareTypes");
            DropTable("BasicData.FareTypes");
            DropTable("Transporters.DriverFareTypes");
            DropTable("Transporters.DriverPersonalInfos");
            DropTable("BasicData.Countries");
            DropTable("Customers.Companies");
            DropTable("User.UserLogins");
            DropTable("User.UserClaims");
            DropTable("User.Users");
            DropTable("User.UserRoles");
            DropTable("User.Roles");
            DropTable("User.RolePermissions");
            DropTable("User.Menus");
            DropTable("User.MenuActions");
            DropTable("User.Actions");
        }
    }
}
