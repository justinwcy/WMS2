using System.Reflection;

using Application.Constants;
using Application.DependencyInjection;
using Application.Interface.Identity;

using Infrastructure.DependencyInjection;

using Microsoft.AspNetCore.Identity;

using MudBlazor;
using MudBlazor.Services;

using WebUI.Components;
using WebUI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationService();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddMudPopoverService();
builder.Services.AddMudBlazorSnackbar();
builder.Services.AddMudBlazorDialog();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 2000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseMiddleware<BlazorCookieLoginMiddleware>();
app.UseMiddleware<BlazorCookieLogoutMiddleware>();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


using (var scope = app.Services.CreateScope())
{
    // register all roles
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = typeof(StaffRole)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Select(field => field.GetValue(null).ToString())
        .ToList();

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // create default admin account
    var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
    await accountService.SetUpAsync();
}

app.Run();
