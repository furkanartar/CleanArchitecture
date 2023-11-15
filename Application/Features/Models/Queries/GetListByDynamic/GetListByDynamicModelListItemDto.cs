namespace Application.Features.Models.Queries.GetListByDynamic;

public class GetListByDynamicModelListItemDto
{
    public Guid Id { get; set; }
    public string Brand { get; set; } //join  entity'nin adını da başına yazmamız gerekiyor BrandName gibi ki auto mapper map'lemeyi yapabilsin ya da MappingProfiles.cs içinde ekstra bildirmemiz gerek (ben ekstra bildirdim)
    public string Fuel { get; set; } //join
    public string Transmission { get; set; } //join
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; }
}
