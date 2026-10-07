using Microsoft.EntityFrameworkCore;
using MukeshPortfolio.API.Data;

var builder = WebApplication.CreateBuilder(args);

// =====================================================================
// SERVICE REGISTRATION (Dependency Injection Container)
// =====================================================================

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // Circular reference handling (User ↔ Projects ↔ User)
        options.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddOpenApi();

// =====================================================================
// DATABASE CONTEXT REGISTRATION
// =====================================================================
// AppDbContext ko DI container mein register karta hai.
// Scoped lifetime: per HTTP request ek instance.
// =====================================================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// =====================================================================
// CORS
// =====================================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// =====================================================================
// MIDDLEWARE PIPELINE
// =====================================================================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Mukesh Portfolio API v1");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();