namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleTablesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Vehicles.VehicleImages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VehicleId = c.Long(nullable: false),
                        ImageUrl = c.String(),
                        ImageTitle = c.String(),
                        CreationDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Vehicles.Vehicles", t => t.VehicleId, cascadeDelete: true)
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
                        DriverPersonalInfoId = c.Long(),
                        IsVerified = c.Boolean(),
                        Status = c.Int(),
                        IsActive = c.Boolean(),
                        DriverPersonalInfo_Id = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Transporters.DriverPersonalInfos", t => t.DriverPersonalInfo_Id)
                .ForeignKey("Transporters.InsuranceCompanies", t => t.InsuranceCompanyId)
                .ForeignKey("BasicData.ModelYearCombinations", t => t.ModelYearCombinationId)
                .ForeignKey("BasicData.VehicleCapacities", t => t.VehicleCapacityId)
                .ForeignKey("BasicData.VehicleConfigurations", t => t.VehicleConfigurationId)
                .Index(t => t.ModelYearCombinationId)
                .Index(t => t.VehicleConfigurationId)
                .Index(t => t.VehicleCapacityId)
                .Index(t => t.InsuranceCompanyId)
                .Index(t => t.DriverPersonalInfo_Id);
            
            CreateTable(
                "Transporters.VehiclePayloadTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VehicleId = c.Long(nullable: false),
                        PayLoadTypeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.PayLoadTypes", t => t.PayLoadTypeId, cascadeDelete: true)
                .ForeignKey("Vehicles.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.PayLoadTypeId);
            
            CreateTable(
                "Transporters.VehicleTripTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VehicleId = c.Long(nullable: false),
                        TripTypeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.TripTypes", t => t.TripTypeId, cascadeDelete: true)
                .ForeignKey("Vehicles.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.TripTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Transporters.VehicleTripTypes", "VehicleId", "Vehicles.Vehicles");
            DropForeignKey("Transporters.VehicleTripTypes", "TripTypeId", "BasicData.TripTypes");
            DropForeignKey("Transporters.VehiclePayloadTypes", "VehicleId", "Vehicles.Vehicles");
            DropForeignKey("Transporters.VehiclePayloadTypes", "PayLoadTypeId", "BasicData.PayLoadTypes");
            DropForeignKey("Vehicles.VehicleImages", "VehicleId", "Vehicles.Vehicles");
            DropForeignKey("Vehicles.Vehicles", "VehicleConfigurationId", "BasicData.VehicleConfigurations");
            DropForeignKey("Vehicles.Vehicles", "VehicleCapacityId", "BasicData.VehicleCapacities");
            DropForeignKey("Vehicles.Vehicles", "ModelYearCombinationId", "BasicData.ModelYearCombinations");
            DropForeignKey("Vehicles.Vehicles", "InsuranceCompanyId", "Transporters.InsuranceCompanies");
            DropForeignKey("Vehicles.Vehicles", "DriverPersonalInfo_Id", "Transporters.DriverPersonalInfos");
            DropIndex("Transporters.VehicleTripTypes", new[] { "TripTypeId" });
            DropIndex("Transporters.VehicleTripTypes", new[] { "VehicleId" });
            DropIndex("Transporters.VehiclePayloadTypes", new[] { "PayLoadTypeId" });
            DropIndex("Transporters.VehiclePayloadTypes", new[] { "VehicleId" });
            DropIndex("Vehicles.Vehicles", new[] { "DriverPersonalInfo_Id" });
            DropIndex("Vehicles.Vehicles", new[] { "InsuranceCompanyId" });
            DropIndex("Vehicles.Vehicles", new[] { "VehicleCapacityId" });
            DropIndex("Vehicles.Vehicles", new[] { "VehicleConfigurationId" });
            DropIndex("Vehicles.Vehicles", new[] { "ModelYearCombinationId" });
            DropIndex("Vehicles.VehicleImages", new[] { "VehicleId" });
            DropTable("Transporters.VehicleTripTypes");
            DropTable("Transporters.VehiclePayloadTypes");
            DropTable("Vehicles.Vehicles");
            DropTable("Vehicles.VehicleImages");
        }
    }
}
