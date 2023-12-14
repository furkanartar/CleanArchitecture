using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetById;

public class GetByIdBrandQuery : IRequest<GetByIdBrandResponse>, ICachableRequest
{
    public Guid Id { get; set; }

    public string? CacheKey => $"GetByIdBrandQuery({Id})";

    public string? CacheGroupKey => "GetBrands";

    public bool BypassCache { get; } //varsayılan değeri kullanıyoruz

    public TimeSpan? SlidingExpiration { get; } //varsayılan değeri kullanıyoruz


    public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, GetByIdBrandResponse> // bu request handler kim için çalışıyor? GetByIdBrandQuery -- response'umuz ne? GetByIdBrandResponse
    {
        public readonly IBrandRepository _brandRepository;
        public readonly IMapper _mapper;

        public GetByIdBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdBrandResponse> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(predicate: b => b.Id == request.Id, withDeleted: true, cancellationToken: cancellationToken);

            GetByIdBrandResponse response = _mapper.Map<GetByIdBrandResponse>(brand);

            return response;
        }
    }
}
