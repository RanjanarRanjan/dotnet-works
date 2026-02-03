using LibraryManagementSystem.Data;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;
using System.Text;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load .env
Env.Load();

// Serilog
builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.Console()
      .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
});

// Add DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// JWT Service
builder.Services.AddSingleton<JwtService>();

//Add Book
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IBookIssueRepository, BookIssueRepository>();

// Authentication with cookies support
var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT_KEY"] ?? DotNetEnv.Env.GetString("JWT_KEY")!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("jwt-token"))
                context.Token = context.Request.Cookies["jwt-token"];
            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Name
    };
});

// Add controllers and swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build app
var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

// EF Core migration
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
// using LibraryManagementSystem.Data;
// using LibraryManagementSystem.Repositories;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;
// using Serilog;
// using System.Security.Claims;
// using System.Text;
// using DotNetEnv;

// var builder = WebApplication.CreateBuilder(args);
// Env.Load();

// // Serilog
// builder.Host.UseSerilog((ctx, lc) =>
//     lc.WriteTo.Console().WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day));

// // DB Context
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// // Repositories
// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<IBookRepository, BookRepository>();

// // JWT
// var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT_KEY"] ?? DotNetEnv.Env.GetString("JWT_KEY")!);
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.Events = new JwtBearerEvents
//         {
//             OnMessageReceived = context =>
//             {
//                 if (context.Request.Cookies.ContainsKey("jwt-token"))
//                     context.Token = context.Request.Cookies["jwt-token"];
//                 return Task.CompletedTask;
//             }
//         };
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = false,
//             ValidateAudience = false,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(key),
//             RoleClaimType = ClaimTypes.Role,
//             NameClaimType = ClaimTypes.Name
//         };
//     });

// // Controllers & Swagger
// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// app.UseSerilogRequestLogging();
// app.UseAuthentication();
// app.UseAuthorization();
// app.MapControllers();
// app.UseSwagger();
// app.UseSwaggerUI();

// // Migrate Users table only (Books via SP)
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     db.Database.Migrate();
// }

// app.Run();

