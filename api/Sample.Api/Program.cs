using Microsoft.EntityFrameworkCore;
using Sample.Api.Data;
using Sample.Api.Models;
using Sample.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Services ---
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<INotesService, NotesService>();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Sample.Api", Version = "v1" });

    // Include XML comments
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});

// CORS – allow the Vite dev server
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendDev", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// --- Middleware ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample.Api v1"));
}

app.UseHttpsRedirection();
app.UseCors("FrontendDev");
app.UseAuthorization();
app.MapControllers();

// --- Database initialisation + seeding ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Notes.Any())
    {
        db.Notes.AddRange(
            new Note { Id = Guid.NewGuid(), Title = "Welcome to Notes!", Content = "This is your first note. Feel free to edit or delete it.", CreatedAt = DateTime.UtcNow.AddMinutes(-5) },
            new Note { Id = Guid.NewGuid(), Title = "Getting started", Content = "Use the form below to create new notes. Click a note to edit it.", CreatedAt = DateTime.UtcNow.AddMinutes(-4) },
            new Note { Id = Guid.NewGuid(), Title = "Tech stack", Content = "This app is built with ASP.NET Core 10, EF Core, React 19, TypeScript, Vite, Tailwind CSS and TanStack React Query.", CreatedAt = DateTime.UtcNow.AddMinutes(-3) },
            new Note { Id = Guid.NewGuid(), Title = "API docs", Content = "The backend exposes a Swagger UI at /swagger when running in Development mode.", CreatedAt = DateTime.UtcNow.AddMinutes(-2) },
            new Note { Id = Guid.NewGuid(), Title = "Sample note five", Content = "Delete me when you're ready to start fresh!", CreatedAt = DateTime.UtcNow.AddMinutes(-1) }
        );
        db.SaveChanges();
    }
}

app.Run();

