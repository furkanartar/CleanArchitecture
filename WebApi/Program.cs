﻿using Application;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(); // Application katmanının IOC süreçleri burada işlenecek. Bu sayede Program.cs temiz kalacak.
builder.Services.AddPersistenceServices(builder.Configuration); // Persistence katmanının IOC süreçleri burada işlenecek.
builder.Services.AddHttpContextAccessor(); // HttpContextAccessor'ı IOC container'a ekliyoruz. Böylece her yerde kullanabiliriz.

//builder.Services.AddDistributedMemoryCache(); // InMemory Cache
builder.Services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:6379"); // Redis Cache (Docker üzerinde)

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

if (app.Environment.IsProduction()) // bu middleware'in yalnızca production'da çalışmasını istediğimiz için if içerisine ekledik. Çünkü development ortamında detaylarını görmek istiyoruz.
    app.ConfigureCustomExceptionMiddleware();// exception middleware'i sisteme dahil ediyoruz, validation da bu middleware üzerinden çalışmakta

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
