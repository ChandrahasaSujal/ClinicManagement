using CM.Data.Enums;
using CM.Data.Infrastructure;
using CM.Data.Utils;
using CM.Data.ViewModels;
using CM.Model.Models;
using CM.Model.Models.Account;
using CM.Model.Models.Medicine;
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
                        UserName = "Admin@clinic.com",
                        Email = "Admin@clinic.com",
                        EmailConfirmed = true,
                        UserPassword = GlobalUtil.EncodePasswordToBase64("test#123"),
                        IsDeleted = false
                    };
                    var result = userManager.Create(adminUser, "test#123");
                    if (result.Succeeded)
                    {
                        //Assign role to user
                        var user = userManager.FindByName("Admin@clinic.com");
                        if (user == null)
                            throw new Exception("User not found!");

                        var role = roleManager.FindByName("Admin");
                        if (role == null)
                            throw new Exception("Role not found!");

                        if (!userManager.IsInRole(user.Id, role.Name))
                        {
                            userManager.AddToRole(user.Id, role.Name);
                            context.SaveChanges();
                        }
                    }

                    if (!context.Categories.Any())
                    {
                        context.Categories.Add(new Category()
                        {
                            Id = Guid.NewGuid(),
                            CategoryName = "Others",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            CatrgoryDescription = "Others",
                            IsDeleted = false
                        });
                        context.SaveChanges();
                    }

                    if (!context.Manufacturers.Any())
                    {
                        context.Manufacturers.Add(new Manufacturer()
                        {
                            Id = Guid.NewGuid(),
                            ManufacturerName = "Others",
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            ManufacturerDescription = "Others",
                            IsDeleted = false
                        });
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalUtil.LogException(ex);
            }

        }
    }
}
