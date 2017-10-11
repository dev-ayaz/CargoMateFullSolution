namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleTypemakeRelsationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("BasicData.VehicleMakes", "VehicleTypeId", c => c.Long());
            CreateIndex("BasicData.VehicleMakes", "VehicleTypeId");
            AddForeignKey("BasicData.VehicleMakes", "VehicleTypeId", "BasicData.VehicleTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("BasicData.VehicleMakes", "VehicleTypeId", "BasicData.VehicleTypes");
            DropIndex("BasicData.VehicleMakes", new[] { "VehicleTypeId" });
            DropColumn("BasicData.VehicleMakes", "VehicleTypeId");
        }
    }
}
