using CM.Data.Infrastructure;
using System.Data.Entity.Migrations;

namespace CM.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CM.Data.Infrastructure.ApplicationDbContext";
        }


    }
}
