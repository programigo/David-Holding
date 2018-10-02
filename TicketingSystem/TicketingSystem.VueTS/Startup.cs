using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketingSystem.Data;
using TicketingSystem.Implementations;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Infrastructure.Extensions;
using TicketingSystem.VueTS.Services;
using DATA_MODELS = TicketingSystem.Data.Models;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;

namespace TicketingSystem.VueTS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStr = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<TicketingSystemDbContext>(options =>
                options.UseSqlServer(connectionStr));

            services.AddIdentity<DATA_MODELS.User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<TicketingSystemDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper();

            services.AddDomainServices();

            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddScoped<IAdminProjectService, AdminProjectService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseMigration();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
