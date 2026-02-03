var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(); // <-- REQUIRED for Controllers

var app = builder.Build();

// Optional: HTTPS redirection can be enabled if you want
// app.UseHttpsRedirection();

app.UseAuthorization();

// Map controller routes
app.MapControllers();

app.Run();
