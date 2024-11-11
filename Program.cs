using Microsoft.EntityFrameworkCore;
using MediatR;
using Products_CQRS.Data;
using Microsoft.Extensions.DependencyInjection;
using Products_CQRS.Commands;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configure the JSON serializer to handle circular references using ReferenceHandler.Preserve
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// MediatR szolgáltatás hozzáadása a Commands egyik handler típusával
// MediatR szolgáltatás hozzáadása a Commands egyik handler típusával
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
