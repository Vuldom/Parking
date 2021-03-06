﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace Educo.Parking.Data.Tests.Fakes
{
    internal class FakeIdentityContextFactory
    {
        internal UserManager<ApplicationUser> UserManager { get; private set; }
        internal RoleManager<ApplicationRole> RoleManager { get; set; }
        internal SignInManager<ApplicationUser> SignInManager { get; private set; }

        internal FakeIdentityContextFactory()
        {

            IServiceCollection services = new ServiceCollection();
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            services.AddTransient<IConfiguration>((sp) => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build());
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

            HttpRequest request = new DefaultHttpContext().Request;
            HttpContextAccessor contextAccessor = new HttpContextAccessor { HttpContext = request.HttpContext };
            services.AddSingleton<IHttpContextAccessor>(contextAccessor);

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            contextAccessor.HttpContext.RequestServices = serviceProvider;
            ParkingDBContext context = serviceProvider.GetRequiredService<ParkingDBContext>();

            UserStore<ApplicationUser,ApplicationRole, ParkingDBContext, string> userStore = new UserStore<ApplicationUser, ApplicationRole, ParkingDBContext, string>(context);
            IOptions<IdentityOptions> serviceIOptions = serviceProvider.GetRequiredService<IOptions<IdentityOptions>>();
            UserManager<ApplicationUser> serviceUserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            ILogger<UserManager<ApplicationUser>> serviceILoggerUserManager = serviceProvider.GetRequiredService<ILogger<UserManager<ApplicationUser>>>();

            RoleStore<ApplicationRole, ParkingDBContext, string> roleStore = new RoleStore<ApplicationRole, ParkingDBContext, string>(context);
            RoleManager<ApplicationRole> serviceRoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            ILogger<RoleManager<ApplicationRole>> serviceILoggerRoleManager = serviceProvider.GetRequiredService<ILogger<RoleManager<ApplicationRole>>>();

            IHttpContextAccessor serviceIHttpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            SignInManager<ApplicationUser> serviceSignInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
            ILogger<SignInManager<ApplicationUser>> serviceILoggerSignInManager = serviceProvider.GetRequiredService<ILogger<SignInManager<ApplicationUser>>>();
            IAuthenticationSchemeProvider serviceIAuthenticationSchemeProvider = serviceProvider.GetRequiredService<IAuthenticationSchemeProvider>();

            UserManager = new UserManager<ApplicationUser>(
                userStore,
                serviceIOptions,
                serviceUserManager.PasswordHasher,
                serviceUserManager.UserValidators,
                serviceUserManager.PasswordValidators,
                serviceUserManager.KeyNormalizer,
                serviceUserManager.ErrorDescriber,
                serviceProvider,
                serviceILoggerUserManager
                );

            RoleManager = new RoleManager<ApplicationRole>(
                roleStore,
                serviceRoleManager.RoleValidators,
                serviceRoleManager.KeyNormalizer,
                serviceRoleManager.ErrorDescriber,
                serviceILoggerRoleManager
                );

            SignInManager = new SignInManager<ApplicationUser>(
                UserManager,
                serviceIHttpContextAccessor,
                serviceSignInManager.ClaimsFactory,
                serviceIOptions,
                serviceILoggerSignInManager,
                serviceIAuthenticationSchemeProvider
                );
        }
    }
}
