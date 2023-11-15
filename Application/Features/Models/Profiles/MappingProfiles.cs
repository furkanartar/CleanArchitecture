using Application.Features.Models.Queries.GetList;
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
        CreateMap<Paginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();
    }
}
