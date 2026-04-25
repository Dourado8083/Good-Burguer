using GoodBurger.Api.Data;
using GoodBurger.Api.Interfaces;
using GoodBurger.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
//Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//Conexão com meu mysql do computador
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddControllers();
//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWeb", policy =>
        policy.WithOrigins("http://localhost:5110")
              .AllowAnyMethod()
              .AllowAnyHeader()
              );

});

// Registro das Services
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IOrderService, OrderServices>();
var app = builder.Build();
app.MapOpenApi();
app.MapControllers();
app.UseCors("AllowWeb");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GoodBurger API v1");
    });

}

app.Run();

