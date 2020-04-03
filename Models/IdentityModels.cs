using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HSNHospitalProject.Models
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
        /*
         * This is the Model for user accounts, so here is where you would add new fields that will be added to the users table
         * ie. the is_admin column will be added and will indicate whether or not a user is an admin
         */

        //is_admin column indicates whether or not a user is an admin
        public bool is_admin { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /* once your Models are made, you can add them here much like the line of code below and they should be added as a table
        * once you add a migration and update the database.
        * **REMEMBER TO ALWAYS PULL AND UPDATE DATABASE BEFORE ADDING A NEW MIGRATION**
        * This will make sure you have implemented the newest migrations in the master before adding yours on top of them.
        * (DON'T FORGET TO ALWAYS PULL BEFORE PUSHING)
        * - Sam B
        */
        public System.Data.Entity.DbSet<HSNHospitalProject.Models.GalleryImages> GalleryImages { get; set; }
        public System.Data.Entity.DbSet<HSNHospitalProject.Models.ActivityRecords> ActivityRecords { get; set; }
        public System.Data.Entity.DbSet<HSNHospitalProject.Models.Department> Departments { get; set; }
        public System.Data.Entity.DbSet<HSNHospitalProject.Models.JobPost> JobPosts { get; set; }

        public System.Data.Entity.DbSet<HSNHospitalProject.Models.Donation> Donations { get; set; }

        public System.Data.Entity.DbSet<HSNHospitalProject.Models.Article> Articles { get; set; }
    }
}