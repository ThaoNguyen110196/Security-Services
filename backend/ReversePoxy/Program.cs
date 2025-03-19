

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure reverse proxy from appsettings
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxyInternal"))
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxyMarketing"));

// Set up CORS to allow requests from the client
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
             policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseRouting();
app.UseCors(); // Apply CORS middleware
app.UseAuthorization();

// Map controllers and reverse proxy
app.MapControllers();
app.MapReverseProxy();

// Listen on port 6000
app.Run();
