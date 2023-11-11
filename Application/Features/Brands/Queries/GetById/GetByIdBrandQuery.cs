using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetById;

public class GetByIdBrandQuery : IRequest<GetByIdBrandResponse>
{
    public Guid Id { get; set; }

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
