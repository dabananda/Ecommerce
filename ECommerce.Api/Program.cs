using ECommerce.Api.Data;
using ECommerce.Api.Middleware;
using ECommerce.Api.Repositories.Implementations;
using ECommerce.Api.Repositories.Interfaces;
using ECommerce.Api.Services.Implementations;
using ECommerce.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Database context configuration
builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDbConnection")));

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(Program));

// Product service and repository registrations
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Custom middlewares
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
