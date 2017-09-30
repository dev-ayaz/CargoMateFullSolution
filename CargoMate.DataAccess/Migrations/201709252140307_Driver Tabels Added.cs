namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverTabelsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Transporters.DriverPersonalInfos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DriverId = c.String(nullable: false, maxLength: 50),
                        Name = c.String(maxLength: 250),
                        LegalName = c.String(maxLength: 250),
                        DateOfBirth = c.DateTime(storeType: "date"),
                        PhoneNumber = c.String(maxLength: 20),
                        EmailAddress = c.String(maxLength: 50),
                        ImageUrl = c.String(),
                        CountryId = c.Long(),
                        Gender = c.Boolean(),
                        IsPhoneNumberVerified = c.Boolean(),
                        Rating = c.Decimal(precision: 18, scale: 2),
                        TotalTrips = c.Long(),
                        FixedRate = c.Boolean(),
                        IdVerified = c.Boolean(),
                        MembershipDate = c.DateTime(),
                        IsValidated = c.Boolean(),
                        Location = c.Geography(),
                        Address = c.String(),
                        DriverStatusId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BasicData.Countries", t => t.CountryId)
                .ForeignKey("BasicData.DriverStatuses", t => t.DriverStatusId)
                .Index(t => t.CountryId)
                .Index(t => t.DriverStatusId);
            
            CreateTable(
                "Transporters.DriverLegalDocuments",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        LicenseNumber = c.String(maxLength: 50),
                        LicenseExpiryDate = c.DateTime(storeType: "date"),
                        LicenseImage = c.String(),
                        ResidenceNumber = c.String(maxLength: 50),
                        ResidenceExpiryDate = c.DateTime(storeType: "date"),
                        ResidenceImage = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Transporters.DriverPersonalInfos", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Transporters.DriverPersonalInfos", "DriverStatusId", "BasicData.DriverStatuses");
            DropForeignKey("Transporters.DriverLegalDocuments", "Id", "Transporters.DriverPersonalInfos");
            DropForeignKey("Transporters.DriverPersonalInfos", "CountryId", "BasicData.Countries");
            DropIndex("Transporters.DriverLegalDocuments", new[] { "Id" });
            DropIndex("Transporters.DriverPersonalInfos", new[] { "DriverStatusId" });
            DropIndex("Transporters.DriverPersonalInfos", new[] { "CountryId" });
            DropTable("Transporters.DriverLegalDocuments");
            DropTable("Transporters.DriverPersonalInfos");
        }
    }
}
