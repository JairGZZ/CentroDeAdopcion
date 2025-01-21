using CentroDeAdopcion_LaEsperanza.DB_Context;
using CentroDeAdopcion_LaEsperanza.Services;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewComponentsAsServices();

var cloudinaryConfig = builder.Configuration.GetSection("Cloudinary");

var cloudinary = new Cloudinary(new Account(
    cloudinaryConfig["CloudName"],
    cloudinaryConfig["ApiKey"],
    cloudinaryConfig["ApiSecret"]));

builder.Services.AddSingleton(cloudinary);
builder.Services.AddScoped<CloudinaryService>();

builder.Services.AddDbContext<CentroDeAdopcionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CentroDeAdopcionContext")));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(middleware =>
    {
        middleware.LoginPath = "/Auth/Login";
        middleware.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });


var app = builder.Build();

app.UseStatusCodePages( context =>
{
    var response = context.HttpContext.Response;

    // Verifica si el código de estado es 404.11 o cualquier otro que quieras manejar
    if (response.StatusCode == 404) // Puedes manejar otros códigos aquí
    {
        response.Redirect("/NotFound/NotFoundPage");
    }
    return Task.CompletedTask;


});

// Configure the HTTP request pipeline.


//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
