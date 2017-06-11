using BookingApp.Models;

namespace BookingApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookingApp.Models.BAContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            // if you want to debug seed method, uncomment these lines. 
            // run update-database from Package Manager Console to start debugger
            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                System.Diagnostics.Debugger.Launch();
            }

        }

        protected override void Seed(BookingApp.Models.BAContext context)
        {
            ////System.Diagnostics.Debug.WriteLine("\n__________________________________\nConfiguration.Seed() debug:\n");

            //#region Creating Roles
            //if (!context.Roles.Any(r => r.Name == "Admin"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "Admin" };

            //    manager.Create(role);
            //}

            //if (!context.Roles.Any(r => r.Name == "Manager"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "Manager" };

            //    manager.Create(role);
            //}

            //if (!context.Roles.Any(r => r.Name == "AppUser"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "AppUser" };

            //    manager.Create(role);
            //}
            //#endregion

            //// First, we have to add independent entities, then call context.SaveChanges() and then add dependent...

            //#region Adding Users

            //context.AppUsers.AddOrUpdate(
            //      p => p.FullName,
            //      new AppUser() { FullName = "Admin Adminovic" }
            // );

            //context.AppUsers.AddOrUpdate(
            //     p => p.FullName,
            //     new AppUser() { FullName = "Menadzer Menadzerovic" }
            //);

            //context.AppUsers.AddOrUpdate(
            //    p => p.FullName,
            //    new AppUser() { FullName = "AppUser AppUserovic" }
            //);


            //// neautentikovanog korisnika ne pravimo, 
            //// to je bilo ko, ko pristupi, a da nije u rolama?
            //#endregion

            //#region Associating users with roles
            //var userStore = new UserStore<BAIdentityUser>(context);
            //var userManager = new UserManager<BAIdentityUser>(userStore);


            //if (!context.Users.Any(u => u.UserName == "admin"))
            //{
            //    var _appUser = context.AppUsers.FirstOrDefault(a => a.FullName == "Admin Adminovic");
            //    var user = new BAIdentityUser()
            //    {
            //        Id = "admin",
            //        UserName = "admin",
            //        Email = "admin@yahoo.com",
            //        PasswordHash = BAIdentityUser.HashPassword("admin"),
            //        appUserId = _appUser.Id // navigation
            //    };
            //    userManager.Create(user);
            //    userManager.AddToRole(user.Id, "Admin");
            //}

            //if (!context.Users.Any(u => u.UserName == "manager"))
            //{
            //    var _appUser = context.AppUsers.FirstOrDefault(a => a.FullName == "Menadzer Menadzerovic");
            //    var user = new BAIdentityUser()
            //    {
            //        Id = "manager",
            //        UserName = "manager",
            //        Email = "manager@yahoo.com",
            //        PasswordHash = BAIdentityUser.HashPassword("manager"),
            //        appUserId = _appUser.Id // navigation
            //    };
            //    userManager.Create(user);
            //    userManager.AddToRole(user.Id, "Manager");
            //}

            //if (!context.Users.Any(u => u.UserName == "appu"))
            //{
            //    var _appUser = context.AppUsers.FirstOrDefault(a => a.FullName == "AppUser AppUserovic");
            //    var user = new BAIdentityUser()
            //    {
            //        Id = "appu",
            //        UserName = "appu",
            //        Email = "appu@yahoo.com",
            //        PasswordHash = BAIdentityUser.HashPassword("appu"),
            //        appUserId = _appUser.Id // navigation
            //    };
            //    userManager.Create(user);
            //    userManager.AddToRole(user.Id, "AppUser");
            //}

            //#endregion

            //ContextHelper.SaveChanges(context);


            //// dakle prvo pravimo drzave, pa regione

            ////context.Countries.AddOrUpdate(
            //// c => c.Name,
            ////        new Country() { Name = "Srbija", Code = "SRB" },
            ////    new Country() { Name = "Makedonija", Code = "MCD" },
            ////    new Country() { Name = "Australija", Code = "ASTRL" },
            ////    new Country() { Name = "Norveska", Code = "NRWY" },
            ////    new Country() { Name = "Kina", Code = "CHIN" }
            //// );



            ////var countries = new List<Country>()
            ////{
            ////    new Country(){Name="Srbija",Code="SRB"},
            ////    new Country(){Name="Makedonija",Code="MCD"},
            ////    new Country(){Name="Australija",Code="ASTRL"},
            ////    new Country(){Name="Norveska",Code="NRWY"},
            ////    new Country(){Name="Kina",Code="CHIN"}
            ////};
            ////context.Countries.AddOrUpdate(c => c.Code, countries.ToArray());
            //////ContextHelper.SaveChanges(context);

            ////var regions = new List<Region>()
            ////{
            ////    new Region(){Name="reg0", CountryId=countries[0].Id},
            ////    new Region(){Name="reg1", CountryId=countries[0].Id},
            ////    new Region(){Name="reg2", CountryId=countries[0].Id},

            ////    new Region(){Name="reg3", CountryId=countries[1].Id},

            ////    new Region(){Name="reg4", CountryId=countries[2].Id},
            ////    new Region(){Name="reg5", CountryId=countries[2].Id},

            ////    new Region(){Name="reg6", CountryId=countries[3].Id},
            ////    new Region(){Name="reg7", CountryId=countries[3].Id},
            ////    new Region(){Name="reg8", CountryId=countries[3].Id},

            ////    new Region(){Name="reg9", CountryId=countries[4].Id},
            ////};

            ////context.Regions.AddOrUpdate(r => r.Id, regions.ToArray());

            ////countries[0].Regions.Add(regions[0]);
            ////countries[0].Regions.Add(regions[1]);
            ////countries[0].Regions.Add(regions[2]);

            ////countries[1].Regions.Add(regions[3]);

            ////countries[2].Regions.Add(regions[4]);
            ////countries[2].Regions.Add(regions[5]);

            ////countries[3].Regions.Add(regions[6]);
            ////countries[3].Regions.Add(regions[7]);
            ////countries[3].Regions.Add(regions[8]);

            ////countries[4].Regions.Add(regions[9]);


            ////ContextHelper.SaveChanges(context);

            ////Country c1 = new Country() { Name = "BiH" };
            ////context.Countries.AddOrUpdate(c1);
            ////ContextHelper.SaveChanges(context);

            ////Region r1 = new Region() { Name = "aaa", CountryId = c1.Id };
            ////Region r2 = new Region() { Name = "bbb", CountryId = c1.Id };

            ////// if you try to add to database something like this, you will get EnittyValidationError
            ////// because Name length is constrained to minimun 3 characters
            ////// this is just for learning purpose :) 
            ////// Region r3 = new Region() { Name = "bb", CountryId = c1.Id };


            ////context.Regions.Add(r1);
            ////context.Regions.Add(r2);
            ////ContextHelper.SaveChanges(context);


            ////// ako regione dodamo kao child objekte na objekat koji je vec u bazi, 
            ////// sledeci put kad uradimo SaveChanges, oni ce se dodati u odgovarajucu tabelu u bazi
            ////// dakle to je kao da smo uradili context.Regions.Add(r1)...
            ////// c1.Regions.Add(r1);
            ////// c1.Regions.Add(r2);
            ////// ContextHelper.SaveChanges(context); 


            ////context.Countries.AddOrUpdate(c1);
            ////ContextHelper.SaveChanges(context);

            ////context.Countries.Remove(c1);
            ////ContextHelper.SaveChanges(context);


        }
    }
}
