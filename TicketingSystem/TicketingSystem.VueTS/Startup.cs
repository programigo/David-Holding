using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;
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

			services.AddDbContext<TicketingSystemDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<DATA_MODELS.User, IdentityRole>()
				.AddEntityFrameworkStores<TicketingSystemDbContext>()
				.AddDefaultTokenProviders();

			services
				.ConfigureApplicationCookie(options =>
				{
					options.Cookie.HttpOnly = true;
					options.LoginPath = "/account/login";
					options.LogoutPath = "/account/logout";
					options.AccessDeniedPath = "/Account/login";
					options.SlidingExpiration = true;
					options.ReturnUrlParameter = "returnUrl";
					options.Events = new CookieAuthenticationEvents
					{
						OnRedirectToLogin = ctx =>
						{
							if (ctx.Request.Path.StartsWithSegments("/api"))
							{
								ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
							}
							else
							{
								ctx.Response.Redirect(ctx.RedirectUri);
							}

							return Task.FromResult(0);
						},
						OnRedirectToAccessDenied = ctx =>
						{
							if (ctx.Request.Path.StartsWithSegments("/api"))
							{
								ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden;
							}
							else
							{
								ctx.Response.Redirect(ctx.RedirectUri);
							}

							return Task.FromResult(0);
						},
					};
				});

			services.AddScoped<ISignInService, SignInService>();
			services.AddScoped<ITicketService, TicketService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IMessageService, MessageService>();
			services.AddScoped<IAdminUserService, AdminUserService>();
			services.AddScoped<IAdminProjectService, AdminProjectService>();

			services.AddAutoMapper();

			services.AddDomainServices();

			services.AddMvc(options =>
			{
				options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
			});

		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
				{
					HotModuleReplacement = true
				});
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
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");

				routes.MapSpaFallbackRoute(
					name: "spa-fallback",
					defaults: new { controller = "Home", action = "Index" });
			});

			app.UseDatabaseMigration();
		}
	}
}
