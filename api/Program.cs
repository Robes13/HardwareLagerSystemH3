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

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext to use MySQL instead of SQL Server
#pragma warning disable CS8604
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
#pragma warning restore CS8604

builder.Services.AddScoped<IRole, RoleRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IHardware, HardwareRepository>();
builder.Services.AddScoped<ITypes, TypeRepository>();
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddScoped<IHardwareCategory, HardwareCategoryRepository>();
builder.Services.AddScoped<IHardwareStatus, HardwareStatusRepository>();
builder.Services.AddScoped<IEmail, EmailRepository>();
builder.Services.AddScoped<IUserHardware, UserHardwareRepository>();
builder.Services.AddScoped<EmailCodeGenerator>();
builder.Services.AddScoped<EmailCodeSender>();
builder.Services.AddControllers().AddNewtonsoftJson(Options =>
{
    Options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});builder.Services.AddEndpointsApiExplorer();
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