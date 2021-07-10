﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IISTestApplication
{
    public class IdentitySeedData
    {
        public static void CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            CreateAdminAccountAsync(serviceProvider, configuration).Wait();
        }

        public static async Task CreateAdminAccountAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var username = configuration["Data:AdminUser:Name"] ?? "admin";
            var email = configuration["Data:AdminUser:Email"] ?? "admin@example.com";
            var password = configuration["Data:AdminUser:Password"] ?? "Admin+1995";
            var role = configuration["Data:AdminUser:Role"] ?? "Admins";
            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                var user = new IdentityUser
                {
                    UserName = username,
                    Email = email
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
