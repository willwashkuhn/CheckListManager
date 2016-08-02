namespace CheckListManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CheckListManager.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CheckListManager.Models.CheckListManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CheckListManager.Models.CheckListManagerContext context)
        {
            context.CheckLists.AddOrUpdate(cl => cl.CheckListId,
                new CheckList
                {
                    Name = "My First List",
                    UserId = 0
                },
                new CheckList
                {
                    Name = "Someone's First List",
                    UserId = 1
                },
                new CheckList
                {
                    Name = "My Second List",
                    UserId = 2
                }
                );

            context.CheckListItems.AddOrUpdate(i => new { i.CheckListId, i.Order },
                new CheckListItem
                {
                    Name = "My First Task",
                    CheckListId = 0,
                    Order = 0,
                    Active = true                
                },
                new CheckListItem
                {
                    Name = "My Second Task",
                    CheckListId = 0,
                    Order = 1,
                    Active = true
                }
                );

            context.Users.AddOrUpdate(i => i.UserId,
                new User
                {
                    Name = "Will Washkuhn"
                },
                new User
                {
                    Name = "Someone Else"
                }
                );
        }
    }
}
