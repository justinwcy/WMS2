using Application.Constants;
using Application.Interface;
using Application.Interface.Identity;

using Infrastructure.Data;
using Infrastructure.Extensions.Identity;
using Infrastructure.FileStorage;
using Infrastructure.Repository;

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

            services.AddDbContextFactory<WmsDbContext>(
                option => option.UseSqlServer(config.GetConnectionString("Default")),
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

            services.AddAuthorizationBuilder()
                .AddPolicy(StaffPolicy.AdminPolicy, authPolicyBuilder =>
                {
                    authPolicyBuilder.RequireAuthenticatedUser();
                    authPolicyBuilder.RequireRole(StaffRole.Admin);
                })
                .AddPolicy(StaffPolicy.ManagerPolicy, authPolicyBuilder =>
                {
                    authPolicyBuilder.RequireAuthenticatedUser();
                    authPolicyBuilder.RequireRole(StaffRole.Manager);
                })
                .AddPolicy(StaffPolicy.SupervisorPolicy, authPolicyBuilder =>
                {
                    authPolicyBuilder.RequireAuthenticatedUser();
                    authPolicyBuilder.RequireRole(StaffRole.Supervisor);
                })
                .AddPolicy(StaffPolicy.StockKeeperPolicy, authPolicyBuilder =>
                {
                    authPolicyBuilder.RequireAuthenticatedUser();
                    authPolicyBuilder.RequireRole(StaffRole.StockKeeper);
                })
                .AddPolicy(StaffPolicy.ReceiverPolicy, authPolicyBuilder =>
                {
                    authPolicyBuilder.RequireAuthenticatedUser();
                    authPolicyBuilder.RequireRole(StaffRole.Receiver);
                })
                .AddPolicy(StaffPolicy.PurchaserPolicy, authPolicyBuilder =>
                {
                    authPolicyBuilder.RequireAuthenticatedUser();
                    authPolicyBuilder.RequireRole(StaffRole.Purchaser);
                })
                .AddPolicy(StaffPolicy.PickerPolicy, authPolicyBuilder =>
                {
                    authPolicyBuilder.RequireAuthenticatedUser();
                    authPolicyBuilder.RequireRole(StaffRole.Picker);
                })
                .AddPolicy(StaffPolicy.PackerPolicy, authPolicyBuilder =>
                {
                    authPolicyBuilder.RequireAuthenticatedUser();
                    authPolicyBuilder.RequireRole(StaffRole.Packer);
                })
                .AddPolicy(StaffPolicy.VendorPolicy, authPolicyBuilder =>
                {
                    authPolicyBuilder.RequireAuthenticatedUser();
                    authPolicyBuilder.RequireRole(StaffRole.Vendor);
                })
                .AddPolicy(StaffPolicy.QcInspectorPolicy, authPolicyBuilder =>
                {
                    authPolicyBuilder.RequireAuthenticatedUser();
                    authPolicyBuilder.RequireRole(StaffRole.QcInspector);
                })
                .AddPolicy(StaffPolicy.MasterControlPolicy, authPolicyBuilder =>
                {
                    authPolicyBuilder.RequireAuthenticatedUser();
                    authPolicyBuilder.RequireRole(StaffRole.MasterControl);
                });

            services.AddCascadingAuthenticationState();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<AccountService>();
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CreateCompanyHandler).Assembly));
            services.AddScoped<IWmsDbContextFactory<WmsDbContext>, DbContextFactory<WmsDbContext>>();
            
            return services;
        }
    }
}
