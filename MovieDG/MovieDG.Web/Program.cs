using Microsoft.EntityFrameworkCore;
using MovieDG.Data.Data.Models;
using MovieDG.Web.Providers;
using MoviesDG.Core.Messaging;
using MoviesDG.Data;
using MoviesDG.Web.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovieDGDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
    .AddEntityFrameworkStores<MovieDGDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IEmailSender>(x => new SendGridEmailSender(builder.Configuration.GetSection("SendGrid:ApiKey").Value));
builder.Services.AddApplicationServices();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
