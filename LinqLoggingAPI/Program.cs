// var builder = WebApplication.CreateBuilder(args);

// // ---------------- SERVICES ----------------
// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // ---------------- MIDDLEWARE ----------------
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// // app.UseHttpsRedirection(); // disabled

// // app.UseAuthorization();

// // ---------------- ENDPOINTS ----------------
// app.MapControllers();

// app.MapGet("/weatherforecast", () =>
// {
//     var summaries = new[]
//     {
//         "Freezing", "Bracing", "Chilly", "Cool", "Mild",
//         "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//     };

//     return Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
// });

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }

using LinqLoggingAPI.Middleware;
using LinqLoggingAPI.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog Configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/app-log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddSingleton<StudentService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Global Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();

