using AutoMapper;
using PackIt.Application.Locations.DTO;
using PackIt.Persistance.EF.Locations.ReadModels;

namespace PackIt.Persistance.EF.Locations.Mapper
{
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<LocationReadModel, LocationDto>()
                .ForMember(dest => dest.Children,
                    opt => opt.MapFrom(src => src.Sublocations))
               .ForMember(dest => dest.Parent, opt => opt.Ignore())//bo ef obsadza Parent i tworzy cykl w grafie ?
                    .ForMember(dest => dest.ParentId,
                    opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Description, opt =>opt.MapFrom(src =>src.Description));
        }
    }
}
