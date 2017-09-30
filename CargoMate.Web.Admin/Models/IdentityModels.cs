using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CargoMate.Web.Admin.Shared;
using System;
using System.Collections.Generic;

namespace CargoMate.Web.Admin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }
 
        public DateTime? CreationDate { get; set; }

        public ICollection<IdentityUserRole> UserRoles { get; set; }


    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name, string description)
            : base(name)
        {
            this.Description = description;
        }

        public virtual string Description { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole
    {
        public ApplicationUserRole()
            : base()
        { }

        public ApplicationRole Role { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=CargoMateDbContext", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ApplicationDbContext>(null);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users", "User");
            modelBuilder.Entity<ApplicationUser>().HasMany(p => p.UserRoles).WithMany();
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "User");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles", "User");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "User");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "User");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "User");

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public bool Seed(ApplicationDbContext context)
        {
            bool success = false;

            using (ApplicationRoleManager roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context)))
            {
                IdentityRole objAdminRole = new IdentityRole("Administrator");
                roleManager.Create(objAdminRole);
            }


            // Create my debug (testing) objects here

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            ApplicationUser user = new ApplicationUser();
            PasswordHasher passwordHasher = new PasswordHasher();

            user.UserName = WebConfigKeys.AdminEmail;
            user.Email = WebConfigKeys.AdminEmail;

            var result = userManager.Create(user, "Admin@123");

            var objAdminUser = userManager.FindByEmail(user.Email);

            success = userManager.AddToRole(objAdminUser.Id, "Administrator").Succeeded;

            return success;
        }



        /// <summary>
        /// Context Initializer
        /// </summary>
        public class DropCreateAlwaysInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

    }
}