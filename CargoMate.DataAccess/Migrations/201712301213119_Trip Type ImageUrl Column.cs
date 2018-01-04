namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TripTypeImageUrlColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("BasicData.TripTypes", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("BasicData.TripTypes", "ImageUrl");
        }
    }
}
