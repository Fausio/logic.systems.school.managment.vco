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

builder.Services.AddDefaultIdentity<AppUser>(options =>
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
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Administrator".ToUpper()));

    options.AddPolicy("RequireEmployeeRole", policy =>
        policy.RequireRole("employee".ToUpper()));

    // Add more policies as needed for other roles
});


builder.Services.AddScoped<IstudantService, StudantService>();
builder.Services.AddScoped<IOrgUnit, OrgUnitService>();
builder.Services.AddScoped<ISempleEntityService, SempleEntityService>();
builder.Services.AddScoped<ITuitionService, TuitionService>();
builder.Services.AddScoped<IDashBoard, DashboardService>();
builder.Services.AddScoped<IApp, AppService>();
builder.Services.AddScoped<Idocument, DocumentService>();
builder.Services.AddScoped<IEnrollment, EnrollmentService>();

builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IUserSirvice, UserSirvice>();

builder.Services.AddScoped<IGradeService, GradeService>();
// Register DinkToPdf converter
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));




builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    // Define o tempo de validade do token para um per�odo longo ou indefinido
    options.ValidationInterval = TimeSpan.FromDays(365); // Expira em 1 ano
});




var app = builder.Build();
// cookie de autentica��o para nunca expirar
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

    var statusCodeString = statusCode.ToString();
    if (statusCode == 400 || statusCode == 403 || statusCodeString.Contains("40"))
    {
        context.Response.Redirect("Identity/Account/Login");
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
    string[] roles = new string[] { "Administrator".ToUpper(), "professor".ToUpper(), "Estudante".ToUpper() };
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
    var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    if (await UserManager.FindByEmailAsync("admin@logicsystems.co.mz") is null)
    {

        var users = new List<AppUser>()
        {
               new AppUser()
            {
                Email =  "admin@logicsystems.co.mz",
                NormalizedEmail = "admin@logicsystems.co.mz",
                UserName =  "admin@logicsystems.co.mz",
                NormalizedUserName = "admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            }  ,   new AppUser()
            {
                Email =  "fausio.matsinhe@logicsystems.co.mz",
                NormalizedEmail = "fausio.matsinhe@logicsystems.co.mz",
                UserName =  "fausio.matsinhe@logicsystems.co.mz",
                NormalizedUserName = "master",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            } ,    new AppUser()
            {
                Email =  "ronilson.cuco@logicsystems.co.mz",
                NormalizedEmail = "ronilson.cuco@logicsystems.co.mz",
                UserName =  "ronilson.cuco@logicsystems.co.mz",
                NormalizedUserName = "master",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            } ,   new AppUser() {
                Email =  "bernardete.paulino@logicsystems.co.mz",
                NormalizedEmail = "bernardete.paulino@logicsystems.co.mz",
                UserName =  "bernardete.paulino@logicsystems.co.mz",
                NormalizedUserName = "Bernardete Paulino",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            }
        };


        foreach (var item in users)
        {
            var email = item.Email;

            if (email == "admin@logicsystems.co.mz")
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

            if (email == "bernardete.paulino@logicsystems.co.mz")
            {
                var pass = "Bernardete1122";
                await UserManager.CreateAsync(item, pass);
                await UserManager.AddToRoleAsync(item, "ADMINISTRATOR");
            }

        }



    }


    // seed Teacher users
    if (true)
    {
        var listOfTeacher = new List<string>()
        {
            "A.Abacar"      ,
            "A.Cesar"      ,
            "A.Fonseca"     ,
            "A.Maricoa"     ,
            "D.Cumbane"     ,
            "G.Cangunga"    ,
            "H.Jamal"      ,
            "J.Alvaro"      ,
            "L.Lutano"      ,
            "M.Caide"       ,
            "M.Faquira"     ,
            "M.Macuta"      ,
            "P.Lassine"     ,
            "P.Ntumba"      ,
            "R.Patrocenio"  ,
            "R.Sebastiao"   ,
            "S.Juvencio"    ,
            "S.Saide"       ,
            "V.Liquela"
        };

        var listOfTeacherMails = new List<string>();

        listOfTeacher.ForEach(item =>
        {
            listOfTeacherMails.Add(item + "@logicsystems.co.mz");
        });

        var users = new List<AppUser>();

        users = listOfTeacherMails.Select(item => new AppUser()
        {
            Email = item,
            NormalizedEmail = item,
            UserName = item,
            NormalizedUserName = "Professor",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,

        }).ToList();

        foreach (var item in users)
        {
            var pass = "panda1234";
            await UserManager.CreateAsync(item, pass);
            await UserManager.AddToRoleAsync(item, "PROFESSOR");
        }
    }
}


await SeedOrgUnit.Run();
await SeedSimpleEntity.Run();
await SeedProducts.Run();

app.Run();

