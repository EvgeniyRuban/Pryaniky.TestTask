using Microsoft.EntityFrameworkCore;
using Pryaniky.TestTask.API.Extentions;
using Pryaniky.TestTask.DAL;
using Pryaniky.TestTask.DAL.Repositories;
using Pryaniky.TestTask.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.ConfigureDbContext(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler(app.Logger);

app.MapControllers();

app.Run();
