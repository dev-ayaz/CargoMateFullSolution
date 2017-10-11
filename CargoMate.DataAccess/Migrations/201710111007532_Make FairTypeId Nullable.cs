namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeFairTypeIdNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Transporters.DriverFareTypes", "FareTypeId", "BasicData.FareTypes");
            DropIndex("Transporters.DriverFareTypes", new[] { "FareTypeId" });
            AlterColumn("Transporters.DriverFareTypes", "FareTypeId", c => c.Long());
            CreateIndex("Transporters.DriverFareTypes", "FareTypeId");
            AddForeignKey("Transporters.DriverFareTypes", "FareTypeId", "BasicData.FareTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Transporters.DriverFareTypes", "FareTypeId", "BasicData.FareTypes");
            DropIndex("Transporters.DriverFareTypes", new[] { "FareTypeId" });
            AlterColumn("Transporters.DriverFareTypes", "FareTypeId", c => c.Long(nullable: false));
            CreateIndex("Transporters.DriverFareTypes", "FareTypeId");
            AddForeignKey("Transporters.DriverFareTypes", "FareTypeId", "BasicData.FareTypes", "Id", cascadeDelete: true);
        }
    }
}
