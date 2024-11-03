using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Application.Constants;
using Application.DependencyInjection;
using WebUI.Components;
using Infrastructure.DependencyInjection;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationService();

builder.Services.AddMudServices();

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

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// register all roles
using (var scope = app.Services.CreateScope())
{
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
}

app.Run();
