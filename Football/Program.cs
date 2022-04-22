using Football.Controllers;
using Football.Core.Constants;
using Football.Extensions;
using Football.Infrastructure.Data;
using Football.Infrastructure.Data.Identity;
using Football.ModelBinders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationDbContext(builder.Configuration);

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FootballDbContext>();

builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration.GetValue<string>("Facebook:AppId");
        options.AppSecret = builder.Configuration.GetValue<string>("Facebook:AppSecret");
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration.GetValue<string>("Google:ClientId");
        options.ClientSecret = builder.Configuration.GetValue<string>("Google:ClientSecret");
    });

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(FormatingConstant.NormalDateFormat));
        options.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
    });

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddMemoryCache();

builder.Services.ApplicationServices();


var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "Area",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "Manager",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapRazorPages();

app.PrepareDatabase();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultAreaRoute();

    endpoints.MapControllerRoute(
     name: "Team Details",
     pattern: "/Teams/Details/{id}/{information}",
     defaults: new
     {
         controller = typeof(TeamsController).GetControllerName(),
         action = nameof(TeamsController.Details)
     });

    endpoints.MapControllerRoute(
    name: "Player Details",
    pattern: "/Players/Details/{id}/{information}",
        defaults: new
        {
            controller = typeof(PlayersController).GetControllerName(),
            //action = nameof(PlayersController.Details)
        });

    endpoints.MapControllerRoute(
    name: "City Details",
    pattern: "/Cities/Details/{id}/{information}",
      defaults: new
      {
          controller = typeof(CitiesController).GetControllerName(),
          //action = nameof(CitiesController.Details)
      });

    endpoints.MapControllerRoute(
    name: "League Details",
    pattern: "/Leagues/Details/{id}/{information}",
      defaults: new
      {
          controller = typeof(LeaguesController).GetControllerName(),
          //action = nameof(LeaguesController.Details)
      });

    endpoints.MapControllerRoute(
    name: "Stadium Details",
    pattern: "/Stadiums/Details/{id}/{information}",
      defaults: new
      {
          controller = typeof(StadiumsController).GetControllerName(),
          //action = nameof(StadiumsController.Details)
      });

    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});


app.Run();
