using Microsoft.EntityFrameworkCore;
using MovieDG.Data.Data.Models;
using MovieDG.Web.Providers;
using MoviesDG.Data;
using MoviesDG.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using AspNetCoreHero.ToastNotification.Extensions;
using MovieDG.Core.Seeding;
using MovieDG.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
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

builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddNotyFService();


var app = builder.Build();

// Configure the HTTP request pipeline.
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
