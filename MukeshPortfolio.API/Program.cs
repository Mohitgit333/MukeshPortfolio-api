var builder = WebApplication.CreateBuilder(args);

// =====================================================================
// SERVICE REGISTRATION (Dependency Injection Container)
// Yahan hum saari services register karte hain jo app mein use hongi.
// =====================================================================

// Controllers ko register karta hai (ProjectsController, SkillsController, etc.)
builder.Services.AddControllers();

// OpenAPI/Swagger support
builder.Services.AddOpenApi();

// CORS policy — React frontend (localhost:5173) se API access ke liye
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
// Yahan order important hai — yeh sequence mein execute hote hain.
// =====================================================================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Swagger UI — development environment mein
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