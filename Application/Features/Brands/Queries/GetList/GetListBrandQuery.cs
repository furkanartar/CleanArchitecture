using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetList;

public class GetListBrandQuery : IRequest<GetListResponse<GetListBrandListItemDto>>, ICachableRequest
{
    //API'a yapılacak isteği burada oluşturuyoruz, kullanıcıdan neleri istediğimizi burada belirtiyoruz.

    public PageRequest PageRequest { get; set; }

    public string CacheKey => $"GetListBrandQuery({PageRequest.PageIndex},{PageRequest.PageSize})"; //burayı bir metot ile dinamik hale getirebiliriz ama geliştiricinin görmesi adına bu şekilde bırakıyorum

    public bool BypassCache { get; }

    public TimeSpan? SlidingExpiration { get; } //varsayılan değeri kullanıyoruz

    public string? CacheGroupKey => "GetBrands";

    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, GetListResponse<GetListBrandListItemDto>> // GetListBrandQuery için çalışacaksın geriye GetListResponse response'unu döneceksin tipinde GetListBrandListItemDto olacak.
    {
        //GetListBrandQuery -> çalışacağımız kaynak
        //GetListResponse -> içerisinde sayfalama alt yapısının bilgileri bulunmakta
        //GetListBrandListItemDto -> veri kaynağımız

        public readonly IBrandRepository _brandRepository;
        public readonly IMapper _mapper;

        public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBrandListItemDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            Paginate<Brand> brands = await _brandRepository.GetListAsync(
                 index: request.PageRequest.PageIndex,
                 size: request.PageRequest.PageSize,
                 withDeleted: true,
                 cancellationToken: cancellationToken
                 ); // burada bize tüm kolonları dönüyor biz kolonları aşağıda filtreleyeceğiz

            GetListResponse<GetListBrandListItemDto> response = _mapper.Map<GetListResponse<GetListBrandListItemDto>>(brands); // brands'i GetListResponse<GetListBrandListItemDto>'ya çeviriyoruz
            //mapper burada bizim için kolonları filtreliyor
            return response;
        }
    }
}