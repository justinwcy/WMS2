using System.Reflection;
using Application.Constants;
using Application.Interface;
using Application.Interface.Identity;
using Infrastructure.Behaviour;
using Infrastructure.Caching;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Infrastructure.Extensions.Identity;
using Infrastructure.Extensions.Identity.Authorization;
using Infrastructure.Repository;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            
            var localStoragePath = Path.Combine(Directory.GetCurrentDirectory(), 
                $@"..\{FileStorageConstants.MainFolder}");
            services.AddSingleton<IFileStorage>(new LocalFileStorage(localStoragePath));

            var connectionString = config.GetConnectionString("Default");

            var serverVersion = new MariaDbServerVersion(new Version(10, 4, 32));

            services.AddDbContextFactory<WmsDbContext>(
                option =>
                {
                    option.UseMySql(connectionString, serverVersion, mysqlOptions =>
                    {
                        mysqlOptions.EnableRetryOnFailure();
                    });
                },
                ServiceLifetime.Transient);
            
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies();
            
            services.AddIdentityCore<WmsStaff>(options=>options.SignIn.RequireConfirmedEmail = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<WmsDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            // Replace the default authorization policy provider with our own
            // custom provider which can return authorization policies for given
            // policy names (instead of using the default policy provider)
            services.AddSingleton<IAuthorizationPolicyProvider, DataAccessPolicyProvider>();

            // As always, handlers must be provided for the requirements of the authorization policies
            services.AddSingleton<IAuthorizationHandler, DataAccessAuthorizationHandler>();

            services.AddAuthorization(options =>
            {
                var accessNames = typeof(AccessName)
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Select(field => field.GetValue(null).ToString())
                    .ToList();
                var dataTableNames = typeof(DataTableName)
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Select(field => field.GetValue(null).ToString())
                    .ToList();

                foreach (var accessName in accessNames)
                {
                    if (accessName == null)
                    {
                        continue;
                    }

                    foreach (var dataTableName in dataTableNames)
                    {
                        if (dataTableName == null)
                        {
                            continue;
                        }
                        options.AddPolicy(
                            DataAccessUtilities.GetPolicyString(accessName, dataTableName), 
                            policy => policy.Requirements.Add(
                                new DataAccessRequirement(accessName, dataTableName)));
                    }
                }
            });

            services.AddCascadingAuthenticationState();
            services.AddScoped<IAccountService, AccountService>();
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(typeof(CreateCompanyHandler).Assembly);
                config.AddOpenBehavior(typeof(QueryCachingPipelineBehaviour<,>));
            });
            services.AddScoped<IWmsDbContextFactory<WmsDbContext>, DbContextFactory<WmsDbContext>>(); 
            services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
            
            // Adding caching capabilities but no cache for now, just in case
            services.AddMemoryCache();
            services.AddSingleton<ICachedService, CacheService>();
            return services;
        }
    }
}
