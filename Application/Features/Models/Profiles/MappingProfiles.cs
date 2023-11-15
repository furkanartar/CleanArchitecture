using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Model, GetListModelListItemDto>()
            .ForMember(destinationMember: m => m.Brand, memberOptions: opt => opt.MapFrom(m => m.Brand.Name)) //model içerisindeki Brand'ın değerini git Brand tablosundaki Name kolonundan al diyorum
            .ForMember(destinationMember: m => m.Fuel, memberOptions: opt => opt.MapFrom(m => m.Fuel.Name))
            .ForMember(destinationMember: m => m.Transmission, memberOptions: opt => opt.MapFrom(m => m.Transmission.Name))
            .ReverseMap();
        CreateMap<Model, GetListByDynamicModelListItemDto>()
           .ForMember(destinationMember: c => c.Brand, memberOptions: opt => opt.MapFrom(c => c.Brand.Name))
           .ForMember(destinationMember: c => c.Fuel, memberOptions: opt => opt.MapFrom(c => c.Fuel.Name))
           .ForMember(destinationMember: c => c.Transmission, memberOptions: opt => opt.MapFrom(c => c.Transmission.Name))
           .ReverseMap();
        CreateMap<Paginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();
        CreateMap<Paginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();
    }
}
