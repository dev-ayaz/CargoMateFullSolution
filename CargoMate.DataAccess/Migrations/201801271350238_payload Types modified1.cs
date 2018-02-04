namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payloadTypesmodified1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Vehicles.VehiclePayloadTypes", "PayLoadTypeIds", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("Vehicles.VehiclePayloadTypes", "PayLoadTypeIds");
        }
    }
}
