using DinkToPdf.Contracts;
using DinkToPdf;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using logic.systems.school.managment.Seeds;
using logic.systems.school.managment.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;

    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

})
    .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.Configure<PasswordHasherOptions>(options =>
//    options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
//);

builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IstudantService, StudantService>();
builder.Services.AddScoped<IOrgUnit, OrgUnitService>();
builder.Services.AddScoped<ISempleEntityService, SempleEntityService>();
builder.Services.AddScoped<ITuitionService, TuitionService>();
builder.Services.AddScoped<IDashBoard, DashboardService>();
builder.Services.AddScoped<IApp, AppService>();
builder.Services.AddScoped<Idocument, DocumentService>();
builder.Services.AddScoped<IEnrollment, EnrollmentService>();

builder.Services.AddScoped<ISalesService, SalesService>();

builder.Services.AddScoped<IGradeService, GradeService>();
// Register DinkToPdf converter
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

var app = builder.Build();
// cookie de autenticação para nunca expirar
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    Secure = CookieSecurePolicy.Always,
    HttpOnly = HttpOnlyPolicy.Always
});


// Configurar middleware para redirecionar para a tela de login em caso de erro 400
app.Use(async (context, next) =>
{
    await next();

    var statusCode = context.Response.StatusCode;
    if (statusCode == 400 || statusCode == 403 )
    {
        context.Response.Redirect("Identity/Account/Login");
    }
});


app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 403)
    {
        // O status de acesso negado (403) foi retornado, redirecione conforme necessário
        context.Response.Redirect("/Identity/Account/Login");
    }

});


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



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Here is the migration executed
    dbContext.Database.Migrate();
}

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = new string[] { "Administrator".ToUpper(), "employee".ToUpper() };
    foreach (string role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
using (var scope = app.Services.CreateScope())
{
    var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();


    if (await UserManager.FindByEmailAsync("admin@pandaalegria.com") is null)
    {

        var users = new List<IdentityUser>()
        {
               new IdentityUser()
            {
                Email =  "admin@pandaalegria.com",
                NormalizedEmail = "admin@pandaalegria.com",
                UserName =  "admin@pandaalegria.com",
                NormalizedUserName = "admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            }  ,   new IdentityUser()
            {
                Email =  "fausio.matsinhe@logicsystems.co.mz",
                NormalizedEmail = "fausio.matsinhe@logicsystems.co.mz",
                UserName =  "fausio.matsinhe@logicsystems.co.mz",
                NormalizedUserName = "master",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            } ,    new IdentityUser()
            {
                Email =  "ronilson.cuco@logicsystems.co.mz",
                NormalizedEmail = "ronilson.cuco@logicsystems.co.mz",
                UserName =  "ronilson.cuco@logicsystems.co.mz",
                NormalizedUserName = "master",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            } ,   new IdentityUser() {
                Email =  "bernardete.paulino@pandaalegria.com",
                NormalizedEmail = "bernardete.paulino@pandaalegria.com",
                UserName =  "bernardete.paulino@pandaalegria.com",
                NormalizedUserName = "Bernardete Paulino",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            }
        };


        foreach (var item in users)
        {
            var email = item.Email;

            if (email == "admin@pandaalegria.com")
            {
                var pass = "admin1234";
                await UserManager.CreateAsync(item, pass);
                await UserManager.AddToRoleAsync(item, "ADMINISTRATOR");
            }

            if (email == "fausio.matsinhe@logicsystems.co.mz")
            {
                var pass = "Madara1122";
                await UserManager.CreateAsync(item, pass);
                await UserManager.AddToRoleAsync(item, "ADMINISTRATOR");
            }

            if (email == "ronilson.cuco@logicsystems.co.mz")
            {
                var pass = "Madara1122";
                await UserManager.CreateAsync(item, pass);
                await UserManager.AddToRoleAsync(item, "ADMINISTRATOR");
            }

            if (email == "bernardete.paulino@pandaalegria.com")
            {
                var pass = "Bernardete1122";
                await UserManager.CreateAsync(item, pass);
                await UserManager.AddToRoleAsync(item, "ADMINISTRATOR");
            }

        }
    }
}


await SeedOrgUnit.Run();
await SeedSimpleEntity.Run();
await SeedProducts.Run();

app.Run();

