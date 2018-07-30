using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TicketingSystem.Common.Constants;
using TicketingSystem.Data;
using TicketingSystem.Data.Models;

namespace TicketingSystem.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<TicketingSystemDbContext>().Database.Migrate();

                UserManager<User> userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                
                Task.Run(async () =>
                {
                    string adminName = WebConstants.AdministratorRole;
                
                    string[] roles = new[]
                    {
                        adminName,
                        WebConstants.ClientRole,
                        WebConstants.SuportRole
                    };
                
                    foreach (var role in roles)
                    {
                        bool roleExists = await roleManager.RoleExistsAsync(role);
                
                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = role
                            });
                        }
                    }
                
                    string adminEmail = "admin@mysite.com";
                
                    User adminUser = await userManager.FindByEmailAsync(adminEmail);
                
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
