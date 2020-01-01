using CM.Data.Migrations;
using CM.Model.Models;
using CM.Model.Models.Account;
using CM.Model.Models.Invoice;
using CM.Model.Models.Medicine;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CM.Data.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            //Database.SetInitializer<ApplicationDbContext>(null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Person> People { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<PurchasedItem> PurchasedItems { get; set; }
    }
}
