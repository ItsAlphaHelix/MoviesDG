using AspNetCoreHero.ToastNotification.Extensions;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieDG.Data.Data.Models;
using MovieDG.Web.Hubs;
using MovieDG.Web.Middlewares;
using MovieDG.Web.Providers;
using MoviesDG.Data;
using MoviesDG.Web.Extensions;
using NToastNotify;
using System.Security.Policy;

var builder = WebApplication.CreateBuilder(args);

var keyVaultUrl = new Uri(builder.Configuration.GetSection("KeyVaultURl").Value!);
var azureCredential = new DefaultAzureCredential();

builder.Configuration.AddAzureKeyVault(keyVaultUrl, azureCredential);

var connectionString = builder.Configuration.GetSection("DefaultConnection").Value;

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovieDGDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<MovieDGDbContext>();

builder.Services.AddRazorPages().AddNToastNotifyToastr(new ToastrOptions
{
    ProgressBar = true,
    TimeOut = 5000
});

builder.Services.AddControllersWithViews(
    options =>
    {
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    }
).AddRazorRuntimeCompilation();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddNotyFService();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.Seed();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapHub<ChatHub>("/chatHub");
});

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseNotyf();
app.MapRazorPages();

app.Run();
