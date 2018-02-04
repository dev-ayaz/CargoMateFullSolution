namespace CargoMate.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsuranceTableUpdate : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Transporters.InsuranceCompanies", "CountryId");
            AddForeignKey("Transporters.InsuranceCompanies", "CountryId", "BasicData.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Transporters.InsuranceCompanies", "CountryId", "BasicData.Countries");
            DropIndex("Transporters.InsuranceCompanies", new[] { "CountryId" });
        }
    }
}
