using AuctionManagementSystem.Data;
using AuctionManagementSystem.Services.Interfaces;
using AuctionManagementSystem.Services.Implementations;
using AuctionManagementSystem.Services; 
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ------------------- Add services ---------------------
builder.Services.AddControllers();
// Add services
builder.Services.AddControllers()
  .AddJsonOptions(options =>
  {
      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
  });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure EF Core with SQL Server - pick connection string from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//  Register AuctionService for dependency injection
builder.Services.AddScoped<IAuctionService, AuctionService>();

// Register your services in the container (Dependency Injection)
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// ------------------------------------------------------

var app = builder.Build();

// ------------------- Middleware -----------------------

// Force HTTPS
app.UseHttpsRedirection();

// Always enable Swagger (not only Development)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction API V1");
    c.RoutePrefix = "swagger"; // available at https://localhost:7077/swagger
});

app.UseAuthorization();

app.MapControllers();

app.Run();
