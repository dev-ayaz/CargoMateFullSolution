namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payloadTypesmodified2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("Vehicles.VehiclePayloadTypes", "PayLoadTypeIds");
        }
        
        public override void Down()
        {
            AddColumn("Vehicles.VehiclePayloadTypes", "PayLoadTypeIds", c => c.Long());
        }
    }
}
