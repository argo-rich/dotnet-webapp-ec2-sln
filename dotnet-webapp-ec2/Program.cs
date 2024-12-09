using dotnet_webapp_ec2.Areas.Identity.Data;
using dotnet_webapp_ec2.Data;
using dotnet_webapp_ec2.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Northwind.EntityModels; // To use AddNorthwindContext method.
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// set up the use if Identity for registration, login, logout, forgot passwd, etc
var connectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");
builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlite(connectionString));
builder.Services.AddDefaultIdentity<NorthwindUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityContext>();

// Add services to the container.
builder.Services.AddNorthwindContext($"..{Path.DirectorySeparatorChar}NorthwindDb", "Northwind.db");
builder.Services.AddControllersWithViews();

// add SSL cert
string cert = Environment.GetEnvironmentVariable("SSL_CERT_NWND")!;
string? word = Environment.GetEnvironmentVariable("SSL_CRT_WRD_NWND");
builder.WebHost.ConfigureKestrel(options =>
    options.ConfigureEndpointDefaults(listenOptions =>
        listenOptions.UseHttps(httpsOptions =>
        {
            httpsOptions.ServerCertificate = new X509Certificate2(cert, word);
        })
    )
);

// set up email sender
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
