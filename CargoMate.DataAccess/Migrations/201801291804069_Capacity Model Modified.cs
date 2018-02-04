namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CapacityModelModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("BasicData.VehicleCapacities", "Weight", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BasicData.VehicleCapacities", "MaximumQuantity", c => c.Long());
            DropColumn("BasicData.VehicleCapacities", "Capacity");
            DropColumn("BasicData.VehicleCapacities", "PalletNumber");
        }
        
        public override void Down()
        {
            AddColumn("BasicData.VehicleCapacities", "PalletNumber", c => c.Long());
            AddColumn("BasicData.VehicleCapacities", "Capacity", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("BasicData.VehicleCapacities", "MaximumQuantity");
            DropColumn("BasicData.VehicleCapacities", "Weight");
        }
    }
}
