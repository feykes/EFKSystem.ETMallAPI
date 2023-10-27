using EFKSystem.Application;
using EFKSystem.Application.Validators.Products;
using EFKSystem.Infrastructure;
using EFKSystem.Infrastructure.Enums;
using EFKSystem.Infrastructure.Filters;
using EFKSystem.Infrastructure.Services.Storage.Azure;
using EFKSystem.Infrastructure.Services.Storage.Local;
using EFKSystem.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
