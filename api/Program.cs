using api.Data;
using MySql.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Controllers;
using api.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env
DotEnv.Load();

// Retrieve the connection string from the environment variables
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Database connection string is not set.");
}

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseMySQL(connectionString));


builder.Services.AddScoped<IRole, RoleRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IHardware, HardwareRepository>();
builder.Services.AddScoped<ITypes, TypeRepository>();
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddScoped<IHardwareCategory, HardwareCategoryRepository>();
builder.Services.AddScoped<IHardwareStatus, HardwareStatusRepository>();
builder.Services.AddScoped<IEmail, EmailRepository>();
builder.Services.AddScoped<IUserHardware, UserHardwareRepository>();
builder.Services.AddScoped<INotification, NotificationRepository>();
builder.Services.AddScoped<EmailCodeGenerator>();
builder.Services.AddScoped<EmailCodeSender>();
builder.Services.AddScoped<JwtTokenService>();
// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddControllers().AddNewtonsoftJson(Options =>
{
    Options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
