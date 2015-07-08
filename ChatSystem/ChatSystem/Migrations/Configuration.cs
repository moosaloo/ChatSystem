namespace ChatSystem.Migrations
{
    using ChatSystem.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ChatSystem.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ChatSystem.Models.ApplicationDbContext";
        }

        /// <summary>
        /// این متد وقتی دیتابیس ساخته میشود اجرا میشود
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ChatSystem.Models.ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "maryam@gmail.com",
                Email = "maryam@gmail.com",
                EmailConfirmed = true,
            };

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!manager.Users.Any(a => a.UserName == user.UserName))
            {
                manager.Create(user, "maryam1371@gmail.com");
                var adminUser = manager.FindByName("maryam@gmail.com");
                manager.AddToRoles(adminUser.Id, new string[] { "Admin" });
            }


            using (var db = new ApplicationDbContext())
            {
                if (!db.Rooms.Any())
                {
                    db.Rooms.AddRange(new List<Room>{
                        new Room{ Name="گروه آی تی 91"},
                        new Room{ Name="گروه برنامه نویسان"},
                         new Room{ Name="پسرانه"},
                          new Room{ Name="دخترانه"}

                    });
                    db.SaveChanges();
                }
            }

        }
    }
}
