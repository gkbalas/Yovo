using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Yovo.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
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
        public virtual ICollection<House> Houses { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // leyteris con string
        //const string connectionString = @"Data Source=DESKTOP-GN1GREE\SQLEXPRESS; Initial Catalog=Yovo; Integrated Security=True; User Id=strah; Password=12345;";
        // thodoris con string
        //const string connectionString = @"Data Source=ACER\SQLEXPRESS;Initial Catalog=Yovo;Integrated Security=True ;User Id=sa;Password=sasasa;";
        // giorgos con string
        const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Yovo;Integrated Security=True;";

        public ApplicationDbContext()
            : base(connectionString, throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Yovo.Models.House> Houses { get; set; }

        public System.Data.Entity.DbSet<Yovo.Models.Address> Addresses { get; set; }

        public System.Data.Entity.DbSet<Yovo.Models.Bedroom> Bedrooms { get; set; }

        public System.Data.Entity.DbSet<Yovo.Models.Feature> Features { get; set; }

        public System.Data.Entity.DbSet<Yovo.Models.HouseImg> HouseImgs { get; set; }

        public System.Data.Entity.DbSet<Yovo.Models.HouseReview> HouseReviews { get; set; }

        public System.Data.Entity.DbSet<Yovo.Models.Reservation> Reservations { get; set; }
    }
}