using BrinquedotecaPUC.Web.IoC;
using BrinquedotecaPUC.Web;
using BrinquedotecaPUC.Web.Infra.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DBConnection");

builder.Services.AddDbContext<BrinquedotecaContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddControllersWithViews();

var key = Encoding.UTF8.GetBytes(Settings.Secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

//AddRazorPages adiciona serviços para o Razor Pages ao aplicativo.
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();

IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
ConfigureConfiguration(configurationBuilder);
IConfiguration configuration = configurationBuilder.Build();

builder.Services.AddServices(configuration);

var app = builder.Build();

static void ConfigureConfiguration(IConfigurationBuilder configuration)
{
    var tipoDepuracao = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var diretorio = Path.Combine(Directory.GetCurrentDirectory(), !string.IsNullOrEmpty(tipoDepuracao) ? $"appsettings.{tipoDepuracao}.json" : "appsettings.json");

    configuration.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(diretorio, optional: true, reloadOnChange: true);
}

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

//MapRazorPages adiciona pontos de extremidade para o Razor Pages ao IEndpointRouteBuilder.
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
