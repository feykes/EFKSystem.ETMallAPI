using EFKSystem.Application;
using EFKSystem.Application.Validators.Products;
using EFKSystem.Infrastructure;
using EFKSystem.Infrastructure.Enums;
using EFKSystem.Infrastructure.Filters;
using EFKSystem.Infrastructure.Services.Storage.Azure;
using EFKSystem.Infrastructure.Services.Storage.Local;
using EFKSystem.Persistence;
using EFKSystem.WebAPI.Configurations;
using EFKSystem.WebAPI.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>())
//    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true); //son k�s�m uygulamada default olarak var olan validation filter�n� devre d��� b�rak�yor. kendimiz y�netmek i�in yapt�k

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

//SqlColumn sqlColumn = new SqlColumn();
//sqlColumn.ColumnName = "UserName";
//sqlColumn.DataType = System.Data.SqlDbType.NVarChar;
//sqlColumn.PropertyName = "UserName";
//sqlColumn.DataLength = 50;
//sqlColumn.AllowNull = true;
//ColumnOptions columnOpt = new ColumnOptions();
//columnOpt.Store.Remove(StandardColumn.Properties);
//columnOpt.Store.Add(StandardColumn.LogEvent);
//columnOpt.AdditionalColumns = new Collection<SqlColumn> { sqlColumn };

//Logger log = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.File("logs/log.txt")
//    .WriteTo.MSSqlServer(
//    connectionString: builder.Configuration.GetConnectionString("PotgreSql"),
//     sinkOptions: new MSSqlServerSinkOptions
//{
//AutoCreateSqlTable = true,
//TableName = "logs",
//},
//     appConfiguration: null,
//     columnOptions: columnOpt

//    )
//    .Enrich.FromLogContext()
//    .Enrich.With<CustomUserNameColumn>()
//    .MinimumLevel.Information()
//    .CreateLogger();
//builder.Host.UseSerilog(log);

//builder.Services.AddHttpLogging(logging =>
//{
//    logging.LoggingFields = HttpLoggingFields.All;
//    logging.RequestHeaders.Add("sec-ch-ua");
//    logging.ResponseHeaders.Add("MyResponseHeader");
//    logging.MediaTypeOptions.AddText("application/javascript");
//    logging.RequestBodyLogLimit = 4096;
//    logging.ResponseBodyLogLimit = 4096;

//});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //olu�turulacak token de�erini kimlerin kullanaca��n� belirtir
            ValidateIssuer = true, //token de�erini kimin da��tt���n� belirtir
            ValidateLifetime = true, //token�n s�resi
            ValidateIssuerSigningKey = true, //token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden security key vewrisinin do�rulanmas�d�r

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType=ClaimTypes.Name //bu sat�r sayesinde jwt �zerinde name claimine kar��l�k gelen de�eri User.Identity.Name propertysi ile elde edebiliriz
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

//app.UseSerilogRequestLogging(); //yaz�ld��� yerden sonraki middlewareler loglan�r

//app.UseHttpLogging();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.Use(async (context, next) =>
//{
//    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
//    LogContext.PushProperty("user_name", username);
//    await next();
//});

app.MapControllers();

app.Run();
