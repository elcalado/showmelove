namespace SmlApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SmlApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SmlApi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SmlApi.Models.ApplicationDbContext context)
        {
            context.Sessions.AddOrUpdate(p => p.SessionId,
               new Session
               {
                   Title = "Azure Active Directory Unplugged",
               },
                new Session
                {
                    Title = "The Future of Azure Compute",
                },
                new Session
                {
                    Title = "Developing for Microsoft Band",
                },
                new Session
                {
                    Title = "Creating Powerful dashboards with PowerBI",
                },
                new Session
                {
                    Title = "Going Cross Platform with Cordova",
                }
                );
        }
    }
}
