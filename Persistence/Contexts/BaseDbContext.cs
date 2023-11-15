using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Car> Car { get; set; }
    public DbSet<Fuel> Fuel { get; set; }
    public DbSet<Model> Model { get; set; }
    public DbSet<Transmission> Transmission { get; set; }


    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
        //Database.EnsureCreated(); // veritabanının oluşturulduğundan emin oluyoruz. //migration'a geçiş yaptığımız için açıklama satırına alıyorum
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // örneğin brand için ilgili konfigürasyonları bulmasını söylüyoruz. Persistence/EntityConfigurations
    }
}
