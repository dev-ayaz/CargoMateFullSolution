namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleTablesModified : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "Transporters.VehiclePayloadTypes", newSchema: "Vehicles");
            MoveTable(name: "Transporters.VehicleTripTypes", newSchema: "Vehicles");
        }
        
        public override void Down()
        {
            MoveTable(name: "Vehicles.VehicleTripTypes", newSchema: "Transporters");
            MoveTable(name: "Vehicles.VehiclePayloadTypes", newSchema: "Transporters");
        }
    }
}
