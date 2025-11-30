using ECommerce.Api.Data;
using ECommerce.Api.Interceptors;
using ECommerce.Api.Middleware;
using ECommerce.Api.Repositories.Implementations;
using ECommerce.Api.Repositories.Interfaces;
using ECommerce.Api.Services.Implementations;
using ECommerce.Api.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// fluent validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddOpenApi();

builder.Services.AddScoped<AuditableEntityInterceptor>();

builder.Services.AddDbContext<ECommerceDbContext>((sp, options) =>
{
    var interceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDbConnection")).AddInterceptors(interceptor);
});

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
