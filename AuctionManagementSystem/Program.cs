using AuctionManagementSystem.Data;
using AuctionManagementSystem.Services.Interfaces;
using AuctionManagementSystem.Services.Implementations;
using AuctionManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using AuctionManagementSystem.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ------------------- Add services ---------------------

// Add CORS services to the container (CRITICAL FOR FRONT-END)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            // Explicitly allow the origins your React app uses
            policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Required for SignalR and sometimes for authentication
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // CRITICAL FIX: Ensure the modern serializer ignores cycles 
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Configure Swagger to use JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auction API", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
    });

    // Add a security requirement for the Bearer token
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

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

// Get the JWT key, providing an empty string as a fallback if the key is not found.
var jwtKey = builder.Configuration["Jwt:Key"] ?? string.Empty;

// Configure JWT Authentication
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };
});

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

// Use the CORS policy here, BEFORE authentication and authorization (CRITICAL PLACEMENT)
app.UseCors("AllowAllOrigins");

// ?? INSERTION POINT: Add this line to allow serving files from wwwroot.
// This will resolve the 404 errors for your images (e.g., /images/filename.png).
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

// Map controllers and hubs
app.MapControllers();
app.MapHub<BidHub>("/bidhub");

app.Run();
