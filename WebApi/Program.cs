using Application;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(); //Application katman�n�n IOC s�re�leri burada i�lenmekte.
builder.Services.AddPersistenceServices(builder.Configuration); //Persistence katman�n�n IOC s�re�leri burada i�lenmekte.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction()) //bu middleware'in yaln�zca production'da �al��mas�n� istedi�imiz i�in if i�erisine ekledik. ��nk� development ortam�nda hatan�n detaylar�n� g�rmek istiyoruz
    app.ConfigureCustomExceptionMiddleware(); //exception middleware'i sisteme dahil ediyoruz, validation da bu middleware �zerinden �al��makta

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
