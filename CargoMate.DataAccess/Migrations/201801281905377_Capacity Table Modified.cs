namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CapacityTableModified : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BasicData.UOM",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Factor = c.Double(nullable: false),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "[BasicData.Localization].LocalizedUOM",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                        Description = c.String(),
                        UOMId = c.Long(),
                        CultureCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.UOM", t => t.UOMId)
                .Index(t => t.UOMId);
            
            AddColumn("BasicData.VehicleCapacities", "Breadth", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BasicData.VehicleCapacities", "Height", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BasicData.VehicleCapacities", "MaxQuantity", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BasicData.VehicleCapacities", "WeightUnitId", c => c.Long());
            AddColumn("BasicData.VehicleCapacities", "LengthUnitId", c => c.Long());
            AddColumn("BasicData.VehicleCapacities", "UOMId", c => c.Long());
            AlterColumn("BasicData.VehicleCapacities", "Capacity", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BasicData.VehicleCapacities", "Length", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("BasicData.VehicleCapacities", "WeightUnitId");
            CreateIndex("BasicData.VehicleCapacities", "LengthUnitId");
            CreateIndex("BasicData.VehicleCapacities", "UOMId");
            AddForeignKey("BasicData.VehicleCapacities", "LengthUnitId", "BasicData.LengthUnits", "Id");
            AddForeignKey("BasicData.VehicleCapacities", "UOMId", "BasicData.UOM", "Id");
            AddForeignKey("BasicData.VehicleCapacities", "WeightUnitId", "BasicData.WeightUnits", "Id");
            DropColumn("BasicData.VehicleCapacities", "CultureCode");
            DropColumn("BasicData.VehicleCapacities", "Source");
        }
        
        public override void Down()
        {
            AddColumn("BasicData.VehicleCapacities", "Source", c => c.String(maxLength: 250));
            AddColumn("BasicData.VehicleCapacities", "CultureCode", c => c.String(maxLength: 10));
            DropForeignKey("BasicData.VehicleCapacities", "WeightUnitId", "BasicData.WeightUnits");
            DropForeignKey("BasicData.VehicleCapacities", "UOMId", "BasicData.UOM");
            DropForeignKey("[BasicData.Localization].LocalizedUOM", "UOMId", "BasicData.UOM");
            DropForeignKey("BasicData.VehicleCapacities", "LengthUnitId", "BasicData.LengthUnits");
            DropIndex("[BasicData.Localization].LocalizedUOM", new[] { "UOMId" });
            DropIndex("BasicData.VehicleCapacities", new[] { "UOMId" });
            DropIndex("BasicData.VehicleCapacities", new[] { "LengthUnitId" });
            DropIndex("BasicData.VehicleCapacities", new[] { "WeightUnitId" });
            AlterColumn("BasicData.VehicleCapacities", "Length", c => c.Long());
            AlterColumn("BasicData.VehicleCapacities", "Capacity", c => c.Long());
            DropColumn("BasicData.VehicleCapacities", "UOMId");
            DropColumn("BasicData.VehicleCapacities", "LengthUnitId");
            DropColumn("BasicData.VehicleCapacities", "WeightUnitId");
            DropColumn("BasicData.VehicleCapacities", "MaxQuantity");
            DropColumn("BasicData.VehicleCapacities", "Height");
            DropColumn("BasicData.VehicleCapacities", "Breadth");
            DropTable("[BasicData.Localization].LocalizedUOM");
            DropTable("BasicData.UOM");
        }
    }
}
