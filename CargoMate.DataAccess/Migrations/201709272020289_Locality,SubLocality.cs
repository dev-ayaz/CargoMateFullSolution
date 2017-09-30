namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocalitySubLocality : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Transporters.DriverFareTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FareTypeId = c.Long(nullable: false),
                        DriverPersonalInfoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Transporters.DriverPersonalInfos", t => t.DriverPersonalInfoId, cascadeDelete: true)
                .ForeignKey("BasicData.FareTypes", t => t.FareTypeId, cascadeDelete: true)
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
                "Transporters.PreferredAddresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CountryId = c.Long(nullable: false),
                        Locality = c.String(maxLength: 50),
                        SubLocality = c.String(maxLength: 50),
                        Location = c.Geography(),
                        DriverPersonalInfoId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("Transporters.DriverPersonalInfos", t => t.DriverPersonalInfoId)
                .Index(t => t.CountryId)
                .Index(t => t.DriverPersonalInfoId);
            
            AddColumn("Transporters.DriverPersonalInfos", "Locality", c => c.String());
            AddColumn("Transporters.DriverPersonalInfos", "SubLocality", c => c.String());
            DropColumn("Transporters.DriverPersonalInfos", "Address");
        }
        
        public override void Down()
        {
            AddColumn("Transporters.DriverPersonalInfos", "Address", c => c.String());
            DropForeignKey("Transporters.PreferredAddresses", "DriverPersonalInfoId", "Transporters.DriverPersonalInfos");
            DropForeignKey("Transporters.PreferredAddresses", "CountryId", "BasicData.Countries");
            DropForeignKey("BasicData.LocalizedFareTypes", "FareTypeId", "BasicData.FareTypes");
            DropForeignKey("Transporters.DriverFareTypes", "FareTypeId", "BasicData.FareTypes");
            DropForeignKey("Transporters.DriverFareTypes", "DriverPersonalInfoId", "Transporters.DriverPersonalInfos");
            DropIndex("Transporters.PreferredAddresses", new[] { "DriverPersonalInfoId" });
            DropIndex("Transporters.PreferredAddresses", new[] { "CountryId" });
            DropIndex("BasicData.LocalizedFareTypes", new[] { "FareTypeId" });
            DropIndex("Transporters.DriverFareTypes", new[] { "DriverPersonalInfoId" });
            DropIndex("Transporters.DriverFareTypes", new[] { "FareTypeId" });
            DropColumn("Transporters.DriverPersonalInfos", "SubLocality");
            DropColumn("Transporters.DriverPersonalInfos", "Locality");
            DropTable("Transporters.PreferredAddresses");
            DropTable("BasicData.LocalizedFareTypes");
            DropTable("BasicData.FareTypes");
            DropTable("Transporters.DriverFareTypes");
        }
    }
}
