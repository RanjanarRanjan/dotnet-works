// using DotNetEnv;
// using EmployeeLayeredApp.Data;
// using EmployeeLayeredApp.Repositories;
// using EmployeeLayeredApp.Services;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;

// Env.Load();

// var builder = WebApplication.CreateBuilder(args);

// // JWT values from .env
// var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
// var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
// var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
// builder.Services.AddScoped<EmployeeLogicService>();
// builder.Services.AddScoped<EmployeeWrapperService>();

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
// .AddJwtBearer(options =>
// {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = issuer,
//         ValidAudience = audience,
//         IssuerSigningKey = new SymmetricSecurityKey(
//             Encoding.UTF8.GetBytes(jwtKey!)
//         )
//     };

//     // Read JWT from Cookie
//     options.Events = new JwtBearerEvents
//     {
//         OnMessageReceived = context =>
//         {
//             context.Token = context.Request.Cookies["jwt"];
//             return Task.CompletedTask;
//         }
//     };
// });

// builder.Services.AddAuthorization();

// builder.Services.AddControllers();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// app.UseSwagger();
// app.UseSwaggerUI();

// app.UseHttpsRedirection();
// app.UseAuthentication();
// app.UseAuthorization();
// app.MapControllers();

// app.Run();


using DotNetEnv;
using EmployeeLayeredApp.Data;
using EmployeeLayeredApp.Repositories;
using EmployeeLayeredApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

Env.Load(); // Load .env variables

var builder = WebApplication.CreateBuilder(args);

// -------------------- DB --------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------- REPOSITORIES --------------------
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// -------------------- SERVICES --------------------
builder.Services.AddScoped<EmployeeLogicService>();
builder.Services.AddScoped<EmployeeWrapperService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtService>();

// -------------------- AUTHENTICATION --------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["jwt"];
            return Task.CompletedTask;
        }
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT_ISSUER"],
        ValidAudience = builder.Configuration["JWT_AUDIENCE"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT_SECRET"]!)
        )
    };
});

builder.Services.AddAuthorization();

// -------------------- MVC & SWAGGER --------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// -------------------- MIDDLEWARE --------------------
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();



// using EmployeeLayeredApp.Data;
// using EmployeeLayeredApp.Repositories;
// using EmployeeLayeredApp.Services;
// using Microsoft.EntityFrameworkCore;

// var builder = WebApplication.CreateBuilder(args);

// // Add DbContext
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// // Add Repository & Services
// builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
// builder.Services.AddScoped<EmployeeLogicService>();
// builder.Services.AddScoped<EmployeeWrapperService>();

// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();
// app.UseAuthorization();
// app.MapControllers();
// app.Run();
