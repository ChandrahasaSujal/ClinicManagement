using CM.Data.Enums;
using CM.Data.Infrastructure;
using CM.Data.ViewModels;
using CM.Model.Models;
using CM.Model.Models.Account;
using CM.Tools;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data
{
    public static class Seed
    {
        public static void Initialize(ApplicationDbContext context)
        {
            try
            {
                //Create Roles
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole(Role.Admin.ToString()));
                }

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                if (!userManager.Users.Any())
                {
                    ApplicationUser adminUser = new ApplicationUser
                    {
                        Name = "Chandu",
                        UserName = "CMTest@cm.com",
                        Email = "CMTest@cm.com",
                        EmailConfirmed = true,
                        UserPassword = GlobalUtil.EncodePasswordToBase64("test#123"),
                        IsDeleted = false
                    };
                    var result = userManager.Create(adminUser, "test#123");
                    if (result.Succeeded)
                    {
                        //Assign role to user
                        var user = userManager.FindByName("Chandu");
                        if (user == null)
                            throw new Exception("User not found!");

                        var role = roleManager.FindByName("Chandu");
                        if (role == null)
                            throw new Exception("Role not found!");

                        if (!userManager.IsInRole(user.Id, role.Name))
                        {
                            userManager.AddToRole(user.Id, role.Name);
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
