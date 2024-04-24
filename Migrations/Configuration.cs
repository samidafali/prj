namespace ProjetEpîcerie.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjetEpîcerie.Models.SamiDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ProjetEpîcerie.Models.LoloEpicerieDb";
            AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(ProjetEpîcerie.Models.SamiDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
