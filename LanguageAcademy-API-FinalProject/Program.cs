using LanguageAcademy_API_FinalProject.Helpers;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Data;
using Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = ConnectionStringResolver.Resolve(
    builder.Configuration,
    LoggerFactory.Create(logging => logging.AddConsole()).CreateLogger("Database"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddServiceLayer();
builder.Services.AddRepositoryLayer();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
    await CourseSeeder.SeedAsync(db);
    await HeroSeeder.SeedAsync(db);
    await AboutSeeder.SeedAsync(db);
    await WhyChooseSeeder.SeedAsync(db);
    await HowWeWorkSeeder.SeedAsync(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
