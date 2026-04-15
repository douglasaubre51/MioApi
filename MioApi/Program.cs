var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable("MIO_DB_STRING"));
    options.EnableSensitiveDataLogging();
    options.EnableServiceProviderCaching();
    options.EnableDetailedErrors();
}
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Repositories:
builder.Services.AddScoped<ProjectRepository>();
builder.Services.AddScoped<TaskRepository>();


var app = builder.Build();


app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
