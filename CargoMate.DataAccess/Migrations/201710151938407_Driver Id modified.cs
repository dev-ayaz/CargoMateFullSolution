namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverIdmodified : DbMigration
    {
        public override void Up()
        {
            DropIndex("Vehicles.Vehicles", new[] { "DriverPersonalInfo_Id" });
            DropColumn("Vehicles.Vehicles", "DriverPersonalInfoId");
            RenameColumn(table: "Vehicles.Vehicles", name: "DriverPersonalInfo_Id", newName: "DriverPersonalInfoId");
            AlterColumn("Vehicles.Vehicles", "DriverPersonalInfoId", c => c.String(maxLength: 50));
            CreateIndex("Vehicles.Vehicles", "DriverPersonalInfoId");
        }
        
        public override void Down()
        {
            DropIndex("Vehicles.Vehicles", new[] { "DriverPersonalInfoId" });
            AlterColumn("Vehicles.Vehicles", "DriverPersonalInfoId", c => c.Long());
            RenameColumn(table: "Vehicles.Vehicles", name: "DriverPersonalInfoId", newName: "DriverPersonalInfo_Id");
            AddColumn("Vehicles.Vehicles", "DriverPersonalInfoId", c => c.Long());
            CreateIndex("Vehicles.Vehicles", "DriverPersonalInfo_Id");
        }
    }
}
