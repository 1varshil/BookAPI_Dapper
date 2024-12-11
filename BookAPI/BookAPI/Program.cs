using BookAPI.DapperContext;
using BookAPI.Interface;
using BookAPI.Repo;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Database");

// Register IDbConnection as a singleton
builder.Services.AddSingleton<IDbConnection>(sp =>
    new SqlConnection(connectionString));

// Register dependencies
builder.Services.AddScoped<DapperDbContext>(); // Use Scoped for DbContext to ensure one instance per request
builder.Services.AddScoped<IDapperService, DapperRepo>(); // Ensure correct dependency injection

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
