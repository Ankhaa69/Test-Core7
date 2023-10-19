using ItemManagment.Helpers;
using ItemManagment.Models.DbContexts;
using ItemManagment.Services;
using ItemManagment.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

var rsaParameters = new RSAParameters();
using (var rsa = SecurityUtils.LoadPublicKey(Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration["Jwt:PublicKeyPath"])))
{
    rsaParameters = rsa.ExportParameters(false);
}

builder.Services.AddAuthentication(options =>
       {
           options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       })
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new RsaSecurityKey(rsaParameters),
               ValidateIssuer = true,
               ValidIssuer = "https://localhost:7090",
               ValidateAudience = false,
               ValidateLifetime = true,
               ClockSkew = TimeSpan.Zero
           };
       });

builder.Services.AddControllers();

builder.Services.AddDbContext<ItemDbContext>(dbContextOptionsBuilder =>
                dbContextOptionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IItemGroupService, ItemGroupService>();
builder.Services.AddScoped<IMeasureService, MeasureService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
  {
     options.AssumeDefaultVersionWhenUnspecified = true;
     options.DefaultApiVersion = new ApiVersion(1, 0);
     options.ReportApiVersions = true;
     options.ApiVersionReader = ApiVersionReader.Combine(
            new HeaderApiVersionReader("X-Version"),
               new QueryStringApiVersionReader("api-version"),
                  new MediaTypeApiVersionReader("ver")
                  );
  }
);
//builder.Services.AddVersionedApiExplorer(options =>
//{
//    options.GroupNameFormat = "'v'VVV";
//    options.SubstituteApiVersionInUrl = true;
//});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ItemManagment",
        Version = "v1",
        Description = "Бараа бүртгэл, Бүлэг,Хэмжих нэгж,Багцалсан бараа ,Нийлүүлэгч зэргийн өгөгдөлтэй харилцах EndPoint",
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT токен оруулна уу",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                Array.Empty<string>()
            }
        });
});

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Debug)
    .CreateLogger();
builder.Host.UseSerilog();


var app = builder.Build();

DatabaseMigrationService.MigrationInitialisation(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ItemManagment V1");
    });
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
