namespace Application.Features.Brands.Queries.GetById;

public class GetByIdBrandResponse //Her istek için mutlaka ayrı nesneler oluşturmalıyız. İçerikleri bugün aynı olsa bile yarın değişebilir. Üşenmemeliyiz...
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
