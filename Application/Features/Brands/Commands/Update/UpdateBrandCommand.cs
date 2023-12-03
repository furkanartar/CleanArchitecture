using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Update;

public class UpdateBrandCommand : IRequest<UpdatedBrandResponse>, ICacheRemoverRequest //response nesnemiz UpdatedBrandResponse
{
    //kullanıcıdan update için hangi dataları alacağımızı belirtiyoruz.
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string? CacheKey => "";

    public bool BypassCache => false;

    public string? CacheGroupKey => "GetBrands";

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, UpdatedBrandResponse> // kim için request handler'sın? UpdateBrandCommand -- response'un ne? UpdatedBrandResponse
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        async Task<UpdatedBrandResponse> IRequestHandler<UpdateBrandCommand, UpdatedBrandResponse>.Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            //brand yoksa hata kısmını yazmadık henüz. yoksa update atmayacağız

            Brand? brand = await _brandRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            brand = _mapper.Map(request, brand); // request'i brand'e çeviriyoruz

            await _brandRepository.UpdateAsync(brand);

            UpdatedBrandResponse response = _mapper.Map<UpdatedBrandResponse>(brand); // yukarıdaki map ile bu map aynı mantık kullanım şekli farklı sadece

            return response;
        }
    }
}
