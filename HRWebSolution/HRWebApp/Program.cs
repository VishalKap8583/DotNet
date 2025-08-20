using HRWebApp.Repositories.Implementation;
using HRWebApp.Repositories.Interface;
using HRWebApp.Services.Implementation;
using HRWebApp.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
