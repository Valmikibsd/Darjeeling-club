using ClubApp;
using ClubApp.Controllers;
using DarjeelingClubApp.Models;
using Microsoft.AspNetCore.StaticFiles;
using Rotativa.AspNetCore;
using SmartGatewayDotnetBackendApiKeyKit;
using System.Timers;
using static System.Net.Mime.MediaTypeNames;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterApplicationServices();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromDays(365);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    //  options.Cookie.Name = ".ToolsAppSession";
});
builder.Services.Configure<whatsappsettings>(builder.Configuration);




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
RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

app.UseRouting();
app.UseSession();
app.UseAuthorization();


app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
    {
        {".apk", "application/vnd.android.package-archive"},
        {".nupkg", "application/zip"}
    })
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Common}/{action=Index}/{id?}");

app.Run();

public static partial class ServiceInitializer
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}