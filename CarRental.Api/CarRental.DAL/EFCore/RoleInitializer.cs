using System.Threading.Tasks;
using CarRental.DAL.Entities;
using CarRental.DAL.Enums;
using Microsoft.AspNetCore.Identity;

namespace CarRental.DAL.EFCore
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@gmail.com";

            var password = "qwerty";

            var adminRoleName = Role.Admin.ToString().ToLower();

            var userRoleName = Role.User.ToString().ToLower();

            var managerRoleName = Role.Manager.ToString().ToLower();

            var roles = new[] {adminRoleName, userRoleName, managerRoleName};

            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new User { Email = adminEmail, UserName = adminEmail, Name = "Tatsiana", Surname = "Kazliakouskaya"};

                var result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
