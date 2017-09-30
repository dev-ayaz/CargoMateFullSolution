namespace CargoMate.Web.Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityUpdates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "User.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "User.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Role_Id = c.String(maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("User.Roles", t => t.Role_Id)
                .ForeignKey("User.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("User.Users", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.Role_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "User.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ImageUrl = c.String(),
                        IsActive = c.Boolean(),
                        CreationDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "User.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("User.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "User.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("User.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.ApplicationUserIdentityUserRoles",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        IdentityUserRole_UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUserRole_RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.IdentityUserRole_UserId, t.IdentityUserRole_RoleId })
                .ForeignKey("User.Users", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("User.UserRoles", t => new { t.IdentityUserRole_UserId, t.IdentityUserRole_RoleId }, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => new { t.IdentityUserRole_UserId, t.IdentityUserRole_RoleId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("User.UserRoles", "IdentityUser_Id", "User.Users");
            DropForeignKey("User.UserLogins", "IdentityUser_Id", "User.Users");
            DropForeignKey("User.UserClaims", "IdentityUser_Id", "User.Users");
            DropForeignKey("dbo.ApplicationUserIdentityUserRoles", new[] { "IdentityUserRole_UserId", "IdentityUserRole_RoleId" }, "User.UserRoles");
            DropForeignKey("dbo.ApplicationUserIdentityUserRoles", "ApplicationUser_Id", "User.Users");
            DropForeignKey("User.UserRoles", "RoleId", "User.Roles");
            DropForeignKey("User.UserRoles", "Role_Id", "User.Roles");
            DropIndex("dbo.ApplicationUserIdentityUserRoles", new[] { "IdentityUserRole_UserId", "IdentityUserRole_RoleId" });
            DropIndex("dbo.ApplicationUserIdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("User.UserLogins", new[] { "IdentityUser_Id" });
            DropIndex("User.UserClaims", new[] { "IdentityUser_Id" });
            DropIndex("User.Users", "UserNameIndex");
            DropIndex("User.UserRoles", new[] { "IdentityUser_Id" });
            DropIndex("User.UserRoles", new[] { "Role_Id" });
            DropIndex("User.UserRoles", new[] { "RoleId" });
            DropIndex("User.Roles", "RoleNameIndex");
            DropTable("dbo.ApplicationUserIdentityUserRoles");
            DropTable("User.UserLogins");
            DropTable("User.UserClaims");
            DropTable("User.Users");
            DropTable("User.UserRoles");
            DropTable("User.Roles");
        }
    }
}
