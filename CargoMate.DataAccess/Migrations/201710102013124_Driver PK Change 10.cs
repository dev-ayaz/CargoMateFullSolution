namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverPKChange10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Transporters.DriverFareTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FareTypeId = c.Long(nullable: false),
                        DriverPersonalInfoId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Transporters.DriverPersonalInfos", t => t.DriverPersonalInfoId)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("BasicData.LocalizedFareTypes", "FareTypeId", "BasicData.FareTypes");
            DropForeignKey("Transporters.DriverFareTypes", "FareTypeId", "BasicData.FareTypes");
            DropForeignKey("Transporters.DriverFareTypes", "DriverPersonalInfoId", "Transporters.DriverPersonalInfos");
            DropIndex("BasicData.LocalizedFareTypes", new[] { "FareTypeId" });
            DropIndex("Transporters.DriverFareTypes", new[] { "DriverPersonalInfoId" });
            DropIndex("Transporters.DriverFareTypes", new[] { "FareTypeId" });
            DropTable("BasicData.LocalizedFareTypes");
            DropTable("BasicData.FareTypes");
            DropTable("Transporters.DriverFareTypes");
        }
    }
}
