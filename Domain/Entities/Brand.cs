using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Brand : Entity<Guid>
{
    public string Name { get; set; }

    public virtual ICollection<Model> Models { get; set; } // Bir markanın birden çok modeli olur. Bire çok ilişki modeli

    public Brand()
    {
        Models = new HashSet<Model>(); // Model tekrarına düşmemek için HashSet kullanıyorum
    }

    public Brand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
