using AuctionManagementSystem.Data;
using AuctionManagementSystem.Services.Interfaces;
using AuctionManagementSystem.Services.Implementations;
using AuctionManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using AuctionManagementSystem.Hubs;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// ------------------- Add services ---------------------

// Add Controllers and configure JSON options in a single call
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SignalR services
builder.Services.AddSignalR();

// Configure EF Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register your services in the container (Dependency Injection)
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// ------------------------------------------------------

var app = builder.Build();

// ------------------- Middleware -----------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger documentation only in development
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction API V1");
        c.RoutePrefix = "swagger"; // available at /swagger
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers and hubs
app.MapControllers();
app.MapHub<BidHub>("/bidhub"); // <- expose hub at /bidhub endpoint

app.Run();