namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverPKChange1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Transporters.DriverLegalDocuments", "DId", "Transporters.DriverPersonalInfos");
            DropForeignKey("Transporters.PreferredAddresses", "DriverPersonalInfo_DId", "Transporters.DriverPersonalInfos");
            DropIndex("Transporters.PreferredAddresses", new[] { "DriverPersonalInfo_DId" });
            DropColumn("Transporters.PreferredAddresses", "DriverPersonalInfoId");
            RenameColumn(table: "Transporters.DriverLegalDocuments", name: "DId", newName: "Id");
            RenameColumn(table: "Transporters.PreferredAddresses", name: "DriverPersonalInfo_DId", newName: "DriverPersonalInfoId");
            RenameIndex(table: "Transporters.DriverLegalDocuments", name: "IX_DId", newName: "IX_Id");
            DropPrimaryKey("Transporters.DriverPersonalInfos");
            AddColumn("Transporters.DriverPersonalInfos", "Id", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("Transporters.PreferredAddresses", "DriverPersonalInfoId", c => c.String(maxLength: 50));
            AddPrimaryKey("Transporters.DriverPersonalInfos", "Id");
            CreateIndex("Transporters.PreferredAddresses", "DriverPersonalInfoId");
            AddForeignKey("Transporters.DriverLegalDocuments", "Id", "Transporters.DriverPersonalInfos", "Id");
            AddForeignKey("Transporters.PreferredAddresses", "DriverPersonalInfoId", "Transporters.DriverPersonalInfos", "Id");
            DropColumn("Transporters.DriverPersonalInfos", "DId");
        }
        
        public override void Down()
        {
            AddColumn("Transporters.DriverPersonalInfos", "DId", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("Transporters.PreferredAddresses", "DriverPersonalInfoId", "Transporters.DriverPersonalInfos");
            DropForeignKey("Transporters.DriverLegalDocuments", "Id", "Transporters.DriverPersonalInfos");
            DropIndex("Transporters.PreferredAddresses", new[] { "DriverPersonalInfoId" });
            DropPrimaryKey("Transporters.DriverPersonalInfos");
            AlterColumn("Transporters.PreferredAddresses", "DriverPersonalInfoId", c => c.String());
            DropColumn("Transporters.DriverPersonalInfos", "Id");
            AddPrimaryKey("Transporters.DriverPersonalInfos", "DId");
            RenameIndex(table: "Transporters.DriverLegalDocuments", name: "IX_Id", newName: "IX_DId");
            RenameColumn(table: "Transporters.PreferredAddresses", name: "DriverPersonalInfoId", newName: "DriverPersonalInfo_DId");
            RenameColumn(table: "Transporters.DriverLegalDocuments", name: "Id", newName: "DId");
            AddColumn("Transporters.PreferredAddresses", "DriverPersonalInfoId", c => c.String());
            CreateIndex("Transporters.PreferredAddresses", "DriverPersonalInfo_DId");
            AddForeignKey("Transporters.PreferredAddresses", "DriverPersonalInfo_DId", "Transporters.DriverPersonalInfos", "DId");
            AddForeignKey("Transporters.DriverLegalDocuments", "DId", "Transporters.DriverPersonalInfos", "DId");
        }
    }
}
