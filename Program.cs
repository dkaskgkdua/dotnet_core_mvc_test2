using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using test2.Data;
using test2.Areas.Identity.Data;
using test2.Models;
using System.Net;
using test2.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.Listen(IPAddress.Any, 80);
    serverOptions.Listen(IPAddress.Any, 443, listenOptions =>
    {
        listenOptions.UseHttps();
        //listenOptions.UseHttps("testCert.pfx", "testPassword");
        //listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
    });
});

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = "3245";
    googleOptions.ClientSecret = "234";
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true) // 이메일인증만 로그인가능
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEmailSender, NaverEmailSender>();

var app = builder.Build();
//ST역할을 위해 추가
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData seedData = new SeedData();
    seedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
