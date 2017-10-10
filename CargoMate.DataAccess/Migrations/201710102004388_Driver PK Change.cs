namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverPKChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Transporters.DriverFareTypes", "DriverPersonalInfoId", "Transporters.DriverPersonalInfos");
            DropForeignKey("Transporters.DriverFareTypes", "FareTypeId", "BasicData.FareTypes");
            DropForeignKey("BasicData.LocalizedFareTypes", "FareTypeId", "BasicData.FareTypes");
            DropForeignKey("Transporters.PreferredAddresses", "DriverPersonalInfoId", "Transporters.DriverPersonalInfos");
            DropForeignKey("Transporters.DriverLegalDocuments", "Id", "Transporters.DriverPersonalInfos");
            DropIndex("Transporters.DriverFareTypes", new[] { "FareTypeId" });
            DropIndex("Transporters.DriverFareTypes", new[] { "DriverPersonalInfoId" });
            DropIndex("BasicData.LocalizedFareTypes", new[] { "FareTypeId" });
            DropIndex("Transporters.DriverLegalDocuments", new[] { "Id" });
            DropIndex("Transporters.PreferredAddresses", new[] { "DriverPersonalInfoId" });
            RenameColumn(table: "Transporters.DriverLegalDocuments", name: "Id", newName: "DId");
            DropPrimaryKey("Transporters.DriverPersonalInfos");
            DropPrimaryKey("Transporters.DriverLegalDocuments");
            AddColumn("Transporters.DriverPersonalInfos", "DId", c => c.String(nullable: false, maxLength: 50));
            AddColumn("Transporters.PreferredAddresses", "DriverPersonalInfo_DId", c => c.String(maxLength: 50));
            AlterColumn("Transporters.DriverLegalDocuments", "DId", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("Transporters.PreferredAddresses", "DriverPersonalInfoId", c => c.String());
            AddPrimaryKey("Transporters.DriverPersonalInfos", "DId");
            AddPrimaryKey("Transporters.DriverLegalDocuments", "DId");
            CreateIndex("Transporters.DriverLegalDocuments", "DId");
            CreateIndex("Transporters.PreferredAddresses", "DriverPersonalInfo_DId");
            AddForeignKey("Transporters.PreferredAddresses", "DriverPersonalInfo_DId", "Transporters.DriverPersonalInfos", "DId");
            AddForeignKey("Transporters.DriverLegalDocuments", "DId", "Transporters.DriverPersonalInfos", "DId");
            DropColumn("Transporters.DriverPersonalInfos", "Id");
            DropColumn("Transporters.DriverPersonalInfos", "DriverId");
            DropTable("Transporters.DriverFareTypes");
            DropTable("BasicData.FareTypes");
            DropTable("BasicData.LocalizedFareTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "BasicData.LocalizedFareTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        FareTypeId = c.Long(),
                        CultureCode = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BasicData.FareTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Transporters.DriverFareTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FareTypeId = c.Long(nullable: false),
                        DriverPersonalInfoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("Transporters.DriverPersonalInfos", "DriverId", c => c.String(nullable: false, maxLength: 50));
            AddColumn("Transporters.DriverPersonalInfos", "Id", c => c.Long(nullable: false, identity: true));
            DropForeignKey("Transporters.DriverLegalDocuments", "DId", "Transporters.DriverPersonalInfos");
            DropForeignKey("Transporters.PreferredAddresses", "DriverPersonalInfo_DId", "Transporters.DriverPersonalInfos");
            DropIndex("Transporters.PreferredAddresses", new[] { "DriverPersonalInfo_DId" });
            DropIndex("Transporters.DriverLegalDocuments", new[] { "DId" });
            DropPrimaryKey("Transporters.DriverLegalDocuments");
            DropPrimaryKey("Transporters.DriverPersonalInfos");
            AlterColumn("Transporters.PreferredAddresses", "DriverPersonalInfoId", c => c.Long());
            AlterColumn("Transporters.DriverLegalDocuments", "DId", c => c.Long(nullable: false));
            DropColumn("Transporters.PreferredAddresses", "DriverPersonalInfo_DId");
            DropColumn("Transporters.DriverPersonalInfos", "DId");
            AddPrimaryKey("Transporters.DriverLegalDocuments", "Id");
            AddPrimaryKey("Transporters.DriverPersonalInfos", "Id");
            RenameColumn(table: "Transporters.DriverLegalDocuments", name: "DId", newName: "Id");
            CreateIndex("Transporters.PreferredAddresses", "DriverPersonalInfoId");
            CreateIndex("Transporters.DriverLegalDocuments", "Id");
            CreateIndex("BasicData.LocalizedFareTypes", "FareTypeId");
            CreateIndex("Transporters.DriverFareTypes", "DriverPersonalInfoId");
            CreateIndex("Transporters.DriverFareTypes", "FareTypeId");
            AddForeignKey("Transporters.DriverLegalDocuments", "Id", "Transporters.DriverPersonalInfos", "Id");
            AddForeignKey("Transporters.PreferredAddresses", "DriverPersonalInfoId", "Transporters.DriverPersonalInfos", "Id");
            AddForeignKey("BasicData.LocalizedFareTypes", "FareTypeId", "BasicData.FareTypes", "Id");
            AddForeignKey("Transporters.DriverFareTypes", "FareTypeId", "BasicData.FareTypes", "Id", cascadeDelete: true);
            AddForeignKey("Transporters.DriverFareTypes", "DriverPersonalInfoId", "Transporters.DriverPersonalInfos", "Id", cascadeDelete: true);
        }
    }
}
