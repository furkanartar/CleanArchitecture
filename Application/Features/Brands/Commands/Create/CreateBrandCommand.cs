using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<CreatedBrandResponse>, ITransactionalRequest //response nesnemiz CreatedBrandResponse, yalnızca ITransactionRequest diyerek süreci transaction'a dahil ediyoruz bu kadar...
{
    //kullanıcıdan update için hangi dataları alacağımızı belirtiyoruz.

    public string Name { get; set; }

    /*
     * Kullanıcıdan istediğimiz verileri CreateBrandCommand class'ı içerisinde belirtiriz.
     * Response yapımız CreatedBrandResponse'idir.
     * 
     * Aşağıdaki class request ve response'u handle etmemizi sağlar.
     * 
     * Handler class'ını aynı class içerisine yazma sebebim CreateBrandCommand class'ıyla kardeş olmasından ötürü.
     * Ayrı ayrı hiçbir anlamları yok. Tek başlarına bir işe yaramazlar. Harici class oluşturmak yerine buraya yazıyorum o yüzden.
     */

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse> //MediatR handler
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<CreatedBrandResponse>? Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            //burada gelen request'i doğrudan döndürmüyoruz çünkü hem veri güvenliği için hem de kolonların hepsini dışarı açmamış olabiliriz veya tüm kolon verilerini almıyor olabiliriz (ki böyle olur normalde).

            await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);

            Brand brand = _mapper.Map<Brand>(request); //kaba işi auto mapper'a bırakıyorum ben detay işi yapıyorum.
            brand.Id = Guid.NewGuid();

            // Transaction test için aşağıdaki kodu aktif ediyoruz
            //Brand brand2 = _mapper.Map<Brand>(request); //kaba işi auto mapper'a bırakıyorum ben detay işi yapıyorum.
            //brand2.Id = Guid.NewGuid();

            await _brandRepository.AddAsync(brand);
            //await _brandRepository.AddAsync(brand2);

            CreatedBrandResponse createdBrandResponse = _mapper.Map<CreatedBrandResponse>(brand);
            return createdBrandResponse;
        }
    }
}