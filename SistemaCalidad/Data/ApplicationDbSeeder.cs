#region Using

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SistemaCalidad.Models;
using SistemaCalidad.Utils;
using SistemaCalidad.Configuration;

#endregion

namespace SistemaCalidad.Data
{
    /// <summary>
    /// Helper class that ensures that the data store used by the application contains the demo user.
    /// </summary>
    public class ApplicationDbSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public ApplicationDbSeeder(IConfiguration configuration,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // We take a dependency on the manager as we want to create a valid user
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }


        

        private async Task CreateAdminRole()
        {
            var email = _configuration.GetSection("Admin1:Email").Value;
            var user =await _userManager.FindByNameAsync(email);
            var superAdmin = _configuration.GetSection("Roles:0").Value;
            if (!await _userManager.IsInRoleAsync(user, superAdmin))
            {
                await _userManager.AddToRoleAsync(user, superAdmin);
            }
        }

        private async Task CreateAdminUsers()
        {
            try
            {
                var email = _configuration.GetSection("Admin1:Email").Value;
                var name = _configuration.GetSection("Admin1:Name").Value;
                var lastName = _configuration.GetSection("Admin1:LastName").Value;
                var address = _configuration.GetSection("Admin1:Address").Value;
                var phoneNumber = _configuration.GetSection("Admin1:PhoneNumber").Value;
                var password = _configuration.GetSection("Admin1:Password").Value;


                //var rolesArray = JsonConvert.DeserializeObject<RegisterViewModel>(admins.ToString());

                var user = await _userManager.FindByNameAsync(email);

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        Name=name,
                        LastName=lastName,
                        Address=address,
                        PhoneNumber=phoneNumber,
                        UserName =email,
                        Email =email,
                    };
                  await _userManager.CreateAsync(user, password);
                }
            }
            catch (Exception ex)
            {
               var a= ex.Message;
                throw;
            }
        }

        private async Task CreateUserRoles()
        {

           var roles = _configuration.GetSection("Roles");

            var rolesArray = roles.AsEnumerable();
            
            foreach (var role in rolesArray)
            {
                if (role.Value!=null)
                {
                    if (!await _roleManager.RoleExistsAsync(role.Value))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role.Value));
                    }
                }
               
            }
          
        }
        /// <summary>
        /// Performs the data store seeding of the demo user if it does not exist yet.
        /// </summary>
        /// <returns>A <c>bool</c> indicating whether the seeding has occurred.</returns>
        public async Task EnsureSeed()
        {
            await CreateUserRoles();
            await CreateAdminUsers();
            await CreateAdminRole();
        }
    }
}
