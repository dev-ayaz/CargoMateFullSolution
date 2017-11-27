namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveVehicleTypemakenavigations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("BasicData.VehicleMakes", "VehicleTypeId", "BasicData.VehicleTypes");
            DropIndex("BasicData.VehicleMakes", new[] { "VehicleTypeId" });
            AddColumn("Vehicles.Vehicles", "VehicleTypeId", c => c.Long());
            CreateIndex("Vehicles.Vehicles", "VehicleTypeId");
            AddForeignKey("Vehicles.Vehicles", "VehicleTypeId", "BasicData.VehicleTypes", "Id");
            DropColumn("BasicData.VehicleMakes", "VehicleTypeId");
        }
        
        public override void Down()
        {
            AddColumn("BasicData.VehicleMakes", "VehicleTypeId", c => c.Long());
            DropForeignKey("Vehicles.Vehicles", "VehicleTypeId", "BasicData.VehicleTypes");
            DropIndex("Vehicles.Vehicles", new[] { "VehicleTypeId" });
            DropColumn("Vehicles.Vehicles", "VehicleTypeId");
            CreateIndex("BasicData.VehicleMakes", "VehicleTypeId");
            AddForeignKey("BasicData.VehicleMakes", "VehicleTypeId", "BasicData.VehicleTypes", "Id");
        }
    }
}
