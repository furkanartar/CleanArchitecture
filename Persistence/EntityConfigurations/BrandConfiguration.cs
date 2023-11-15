using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands").HasKey(b => b.Id); // veritabanında hangi tabloya denk gelecek?

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(indexExpression: b => b.Name, name: "UK_Brands_Name").IsUnique(); // UniqueKey_Fuels_Name

        builder.HasMany(b => b.Models); // Bire çok ilişki. Markanın birden çok Modeli olabilir

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue); // bu noktada Brands tablosu için global filtre uyguluyoruz. Brands tablosu için yazılacak tüm sorgulara DeletedDate verisi yoksa getir şartını ekliyoruz.
    }
}
