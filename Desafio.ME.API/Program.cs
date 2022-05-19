using Desafio.ME.Database.Context;
using Desafio.ME.Database.Interfaces;
using Desafio.ME.Database.Repositorios;
using Desafio.ME.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MEContext>(c => c.UseInMemoryDatabase(databaseName: "ME"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Pedidos", Description = "Api de Pedidos", Version = "v1" });
});

builder.Services.AddTransient<IPedidoRepositorio, PedidoRepositorio>();
builder.Services.AddTransient<CriarPedidoHandler>();
builder.Services.AddTransient<ExcluirPedidoHandler>();
builder.Services.AddTransient<ObterPedidoHandler>();
builder.Services.AddTransient<AlterarPedidoHandler>();
builder.Services.AddTransient<AlterarStatusHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Pedidos v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
