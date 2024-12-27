var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Add MiniProfiler
builder.Services.AddMiniProfiler(o => o.RouteBasePath = "/profiler");

var app = builder.Build();

// Enable MiniProfiler middleware
app.UseMiniProfiler();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.) + MiniProfiler
app.UseSwaggerUIWithMiniProfiler();

app.MapControllers();
app.Run();
