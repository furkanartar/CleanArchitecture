using MediatR;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<CreatedBrandResponse>
{

    public string Name { get; set; }

    /*
     * Burada olay şu: API'a request gelir handler'a yönlendiririz ve aşağıdaki handler çalışır ve CreateBrandCommad'a request gelir
     * Response'umuz CreatedBrandResponse'idir.
     * 
     * Aşağıdaki class request ve response'u handle etmemizi sağlar.
     * 
     * Handler class'ını aynı class içerisine yazma sebebim CreateBrandCommand class'ıyla kardeş olmasından ötürü.
     * Ayrı ayrı hiçbir anlamları yok. Tek başlarına bir işe yaramazlar. Harici class oluşturmak yerine buraya yazıyorum o yüzden.
     */

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
    {
        public Task<CreatedBrandResponse>? Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            CreatedBrandResponse createdBrandResponse = new CreatedBrandResponse();
            createdBrandResponse.Name = request.Name;
            createdBrandResponse.Id = new Guid();
            return null;
        }
    }
}