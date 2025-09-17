using AuctionManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; // Make sure to add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
  .AddJsonOptions(options =>
  {
      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
  });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core with SQL Server Express
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();