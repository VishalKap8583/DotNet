
using Enterprise.Repositories.Implementation;
using Enterprise.Repositories.Interface;
using Enterprise.Services.Implementation;
using Enterprise.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IOfficeService, OfficeService>();
builder.Services.AddScoped<IOfficeRepo, OfficeRepo>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
// app.MapRazorPages();
app.MapControllers();

app.Run();

