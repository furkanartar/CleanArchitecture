namespace Application.Features.Brands.Commands.Update;

public class UpdatedBrandResponse
{
    //update sonucu hangi verileri döneceğimizi belirtiyoruz
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set;}
}
