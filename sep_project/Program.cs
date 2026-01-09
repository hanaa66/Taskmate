using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using sep_project.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"))
);
builder.Services.AddLocalization(option => option.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(option =>
{
    var supported_language = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("ar")
    };
    option.DefaultRequestCulture = new RequestCulture("en");
    option.SupportedCultures = supported_language;
    option.SupportedUICultures = supported_language;
    
}
);
builder.Services.AddIdentity<IdentityUser,IdentityRole>(
    option=>
    {
        option.Password.RequiredLength = 8;
        option.Password.RequireNonAlphanumeric = true;
        option.Password.RequireUppercase = true;
        option.User.RequireUniqueEmail = true;

    }
    ).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/User/AccessDenied";
    option.LoginPath = "/user/Login";
    option.ReturnUrlParameter=CookieAuthenticationDefaults.ReturnUrlParameter;
    option.Cookie.Name = "Cookie";
    option.Cookie.HttpOnly = true;
    option.SlidingExpiration = true;
    option.ExpireTimeSpan= TimeSpan.FromMinutes(28);
    option.Cookie.MaxAge = null;
}
);



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
#region language
var loc = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(loc!.Value);
#endregion


var Loc= app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(Loc!.Value);

app.UseEndpoints(x =>
{
    x.MapControllerRoute(
      name: "admin",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    x.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}" );
}
);




app.Run();
