using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TicketingSystem.Web.Common.Constants;
using TicketingSystem.Data;

using TicketingSystem.Services;

namespace TicketingSystem.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<TicketingSystemDbContext>().Database.Migrate();

                IUserService userManager = serviceScope.ServiceProvider.GetService<IUserService>();
                IRoleService roleManager = serviceScope.ServiceProvider.GetService<IRoleService>();
                
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
