using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blog.Utilites
{

    //In order to use an instnce of this class (as it's defined right now..)
    //I would need the following code somewhere in my application 
    //var myDataService = new DataService();
    //var myString = myDataService.DoSomething();
    //myString = "some string"
    //var myOtherString = myDataService.DoSomethingElse();
    //myOtherString = "some other string";

    //Static:  used so we dont have to create an instance of the calss in order to use methods within the class
    //var myString = DataService.DoSomething();
    //myString = "some string"
    //var myOtherString = DataService.DoSomethingElse();
    //myOtherString = "some other string";


    public static class DataUtility
    {                          //Wrapper Method (ManageDataAsync)
        public static async Task ManageDataAsync(IHost host)
        {
            //This technique is used to obtain references to services that get registered in 
            //the ConfigureServices method of the Startup class
            using var svcScope = host.Services.CreateScope();
            var svcProvider = svcScope.ServiceProvider;

            //This dbContextSvc knows how to talk to the DataBase (aka _context)
            //Service 1: An instance of ApplicationDbContext
            //don't actually need this line
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();

            //Service 2: An instance of RoleManager
            var roleManagerSvc = svcProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //Service 3: An instance of UserManager
            var userManagerSvc = svcProvider.GetRequiredService<UserManager<BlogUser>>();


            //Step 1: Add a few Roles into the system (Administrator & Moderator)
            //Call the SeedRolesAsync method and pass it the role service
            //can change to await SeedRolesAsync(roleManagerSvc) if remove line 41 
            await SeedRolesAsync(dbContextSvc, roleManagerSvc);
            //Step 2: Add a few Users (Administrator & Moderator)
            await SeedUsersAsync(dbContextSvc, userManagerSvc);
            //Step 3: Assign a User to the Administrator role and a User to the Moderator role
            await AssignRolesAsync(dbContextSvc, userManagerSvc);
        }
         //can remove ApplicationDbContext dbContext if remove line 41
        private static async Task SeedRolesAsync(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleSvc)
        {
            //write the code to seed a few roles
            //call upon the roleSvc to add a new role
            await roleSvc.CreateAsync(new IdentityRole("Administrator"));
            await roleSvc.CreateAsync(new IdentityRole("Moderator"));
        }

        private static async Task SeedUsersAsync(ApplicationDbContext dbContext, UserManager<BlogUser> userManagerSvc)
        {
            //step 1: create yourself as a user
            var adminUser = new BlogUser()
            {
                Email = "mlongworth@alumni.unc.edu",
                UserName = "mlongworth@alumni.unc.edu",
                FirstName = "Michelle",
                LastName = "Longworth",
                EmailConfirmed = true

            };

            await userManagerSvc.CreateAsync(adminUser, "Handsome02*");
            //step 2: create someone else as a user
            var modUser = new BlogUser()
            {
                Email = "mlongworthdeveloper@gmail.com",
                UserName = "mlongworthdeveloper@gmail.com",
                FirstName = "Michelle",
                LastName = "Longworth",
                EmailConfirmed = true

            };

            await userManagerSvc.CreateAsync(modUser, "Handsome02*");

        }

        private static async Task AssignRolesAsync(ApplicationDbContext dbContext, UserManager<BlogUser> userManagerSvc)
        {
            //write the code to assign each user to a specific role
            //step 1: somehow get a reference to the Michelle Longworth Admin user
            var adminUser = await userManagerSvc.FindByEmailAsync("mlongworth@alumni.unc.edu");

            //step 2: assign the adminUser to the Administrator role
            await userManagerSvc.AddToRoleAsync(adminUser, "Administrator");

            //step 3: step 1 again
            var modUser = await userManagerSvc.FindByEmailAsync("mlongworthdeveloper@gmail.com");

            //step 4: step 2 again
            await userManagerSvc.AddToRoleAsync(modUser, "Moderator");
        }
    }
}
