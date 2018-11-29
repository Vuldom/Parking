using Educo.Parking.Business;
using Educo.Parking.Business.Managers;
using Educo.Parking.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Educo.Parking.Shell.Web.Extensions;
using Educo.Parking.Business.Services.EmailService;
using Microsoft.AspNetCore.Identity;

namespace Educo.Parking.Shell.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration
        {
            get;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseDataMigrations();
            }

            app.UseStaticFiles();
            // установка аутентификации пользователя на основе куки
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IdentityProfileManager>();
            services.AddTransient<IdentityAccountManager>();
            services.AddTransient<AccountManager>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<CarManager>();
            services.AddTransient<IParkingManager, ParkingManager>();
            services.AddTransient(sp => new DataContextFactory(Configuration));

            services.AddDbContext<ParkingDBContext>();
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ParkingDBContext>() 
            .AddDefaultTokenProviders();
            services.AddAuthentication().AddGoogle(options => { options.ClientSecret = "oLqHQgJt-6fEMXJ3qwqwYYO_"; options.ClientId = "488184278424-u21f18jdtltjnvsncd1dklvjoqg6oab7.apps.googleusercontent.com"; });
        }
    }
}
