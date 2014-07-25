using Microsoft.AspNet.Identity;
using Notes.Api.Infrastructure.EntityFramework;
using tc_dev.Core.Infrastructure.Identity;
using tc_dev.Core.Infrastructure.Identity.Models;

namespace Notes.Api.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<NotesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NotesDbContext context)
        {
            var userManager = IdentityFactory.CreateUserManager(context);
            var roleManager = IdentityFactory.CreateRoleManager(context);

            var adminRole = roleManager.FindByName("AdminRole");
            if (adminRole == null) {
                adminRole = new AppIdentityRole { Name = "AdminRole" };
                roleManager.Create(adminRole);
            }
            var userRole = roleManager.FindByName("UserRole");
            if (userRole == null) {
                userRole = new AppIdentityRole { Name = "UserRole" };
                roleManager.Create(userRole);
            }

            var adminUser = userManager.FindByName("nsmith");
            if (adminUser == null) {
                adminUser = new AppIdentityUser { UserName = "nsmith", Email = "admin@notes.com" };
                userManager.Create(adminUser, "password1");
                userManager.SetLockoutEnabled(adminUser.Id, false);
            }

            var domainUser = userManager.FindByName("domainuser");
            if (domainUser == null) {
                domainUser = new AppIdentityUser { UserName = "domainuser", Email = "domainuser@email.com" };
                userManager.Create(domainUser, "password1");
                userManager.SetLockoutEnabled(domainUser.Id, false);
            }

            userManager.AddToRole(adminUser.Id, "AdminRole");
            userManager.AddToRole(domainUser.Id, "UserRole");
        }
    }
}
