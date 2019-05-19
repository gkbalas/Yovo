namespace Yovo.Migrations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Yovo.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Yovo.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Yovo.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Roles.AddOrUpdate(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = "Admin" });
            context.Roles.AddOrUpdate(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = "Owner" });
            context.Roles.AddOrUpdate(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = "Renter" });
            base.Seed(context);
        }
    }
}
