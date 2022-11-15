using System.Security.Claims;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.Graph;
using stid_management.Config;
using stid_management.Data;
using stid_management.Models;
using stid_management.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<StidContext>(options =>
    options.UseSqlite("Data source=stid.db"));

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));
builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy
    options.FallbackPolicy = options.DefaultPolicy;
});

var (tenantId, clientId, clientSecret, scopes) = builder
    .Configuration
    .GetSection("AzureADB2C")
    .Get<GraphConfig>()
    .Deconstruct();

var clientSecretCredential = new ClientSecretCredential(
    tenantId, clientId, clientSecret);

builder.Services.AddSingleton(new GraphServiceClient(clientSecretCredential, scopes));
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IAppRegistrationService, AppRegistrationService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapPost("/appRoleAssignment", async ([FromBody] QueryParams queryParams, IUserService userService) =>
    {
        var appRole = await userService.AddAppRoleToUser(queryParams.UserId, queryParams.AppRoleId, queryParams.AppId);
        return Results.Ok(appRole);
    })
    .AllowAnonymous();

app.MapGet("/applications", async (IAppRegistrationService appRegistrationService) =>
    {
        var apps = await appRegistrationService.GetApplicationsAsync();
        return Results.Ok(apps);
    })
    .AllowAnonymous();

app.MapPut("/applications/{objectId}/appRole", 
        async (
            IAppRegistrationService appRegistrationService, 
            string objectId,
            AppRoleCreate appRoleCreate) =>
        {
            var appRole = appRoleCreate.ToAppRole();
            var res = await appRegistrationService.AddAppRoleToApplicationAsync(objectId, appRole);
            return Results.Ok(res);
        })
    .AllowAnonymous();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

