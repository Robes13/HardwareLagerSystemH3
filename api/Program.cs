using api.Data;
using MySql.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Configure DbContext to use MySQL instead of SQL Server
#pragma warning disable CS8604
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
#pragma warning restore CS8604
builder.Services.AddScoped<IHardwareCategory, HardwareCategoryRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
