﻿using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<CreatedBrandResponse> //response nesnemiz CreatedBrandResponse
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

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<CreatedBrandResponse>? Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            //burada gelen request'i doğrudan döndürmüyoruz çünkü hem veri güvenliği için hem de kolonların hepsini dışarı açmamış olabiliriz veya tüm kolon verilerini almıyor olabiliriz (ki böyle olur normalde).

            Brand brand = _mapper.Map<Brand>(request); //kaba işi auto mapper'a bırakıyorum ben detay işi yapıyorum.
            brand.Id = Guid.NewGuid();

            await _brandRepository.AddAsync(brand);

            CreatedBrandResponse createdBrandResponse = _mapper.Map<CreatedBrandResponse>(brand);

            return createdBrandResponse;
        }
    }
}