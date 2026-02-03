using Microsoft.EntityFrameworkCore;
using RepositoryPatternDemo.Data;
using RepositoryPatternDemo.Repositories.Interfaces;
using RepositoryPatternDemo.Repositories.Implementations;
using RepositoryPatternDemo.Services.Interfaces;
using RepositoryPatternDemo.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.Run();
