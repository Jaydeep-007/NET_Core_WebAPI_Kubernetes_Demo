using Microsoft.EntityFrameworkCore;
using NET_Core_WebAPI_Kubernetes_Demo.Data;
using NET_Core_WebAPI_Kubernetes_Demo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContext<DbContextClass>
(o => o.UseInMemoryDatabase("NET_Core_WebAPI_Kubernetes_Demo"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DbContextClass>();

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
