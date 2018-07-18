﻿namespace TicketingSystem.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
    using TicketingSystem.Data;
    using TicketingSystem.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<TicketingSystemDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                
                Task.Run(async () =>
                {
                    var adminName = WebConstants.AdministratorRole;
                
                    var roles = new[]
                    {
                        adminName,
                        WebConstants.ClientRole,
                        WebConstants.SuportRole
                    };
                
                    foreach (var role in roles)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(role);
                
                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = role
                            });
                        }
                    }
                
                    var adminEmail = "admin@mysite.com";
                
                    var adminUser = await userManager.FindByEmailAsync(adminEmail);
                
                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            Email = adminEmail,
                            UserName = adminName,
                            Name = adminName,
                            IsApproved = true
                        };
                
                        await userManager.CreateAsync(adminUser, "undertaker");
                
                        await userManager.AddToRoleAsync(adminUser, adminName);
                    }
                })
                .Wait();
            }

            return app;
        }
    }
}
