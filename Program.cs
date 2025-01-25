using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

// Register caching services

// In-memory cache (local)
builder.Services.AddDistributedMemoryCache(); // For in-memory cache

// Redis cache (external)
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost"; // Ensure Redis is running on localhost:6379 or adjust as needed
    options.InstanceName = "SampleInstance";
});

// Register DbContext with MySQL connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 40)) // Adjust version as per your MySQL version
    ));

// CORS setup: Allow specific origins or all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Allow any origin (you can specify specific origins as well)
              .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
              .AllowAnyHeader(); // Allow any headers
    });
});

builder.Services.AddScoped<InventoryService>();
builder.Services.AddScoped<SalesService>();
builder.Services.AddScoped<GmailServiceHelper>();

// Add controllers and Swagger for API documentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable Swagger for API documentation
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS using the policy defined earlier
app.UseCors("AllowAll"); // Ensure this is used before any other middleware that handles requests

// Middleware pipeline configuration
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers to routes
app.MapControllers();

// Run the application
app.Run();





