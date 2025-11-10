using Microsoft.EntityFrameworkCore;
using Prescription.Data;
using Prescription.Repositories;
using Prescription.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5141); // listen on all network interfaces
});


// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// CORS: allow everything (so phone can access)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Dependency injection
builder.Services.AddScoped<PrescriptionRepository>();
builder.Services.AddScoped<PrescriptionService>();
builder.Services.AddScoped<MedicineRepository>();
builder.Services.AddScoped<MedicineService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// Swagger (dev only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use HTTP only (LAN-friendly)
app.UseAuthorization();

app.MapControllers();

app.Run();
