using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.DTO;

namespace BusinessLayer.Profiles
{
    public class RoomCategoryProfile : Profile
    {
        public RoomCategoryProfile()
        {
            CreateMap<RoomCategory, RoomCategoryDTO>();

            CreateMap<Room, RoomDTO>()
                .ForMember(dest => dest.RoomImages, opt => opt.MapFrom(src => src.RoomImages))
                .ForMember(dest => dest.RoomFeatures, opt => opt.MapFrom(src => src.RoomFeatures));

            CreateMap<RoomImages, RoomImageDTO>();

            CreateMap<RoomFeatures, RoomFeatureDTO>();
        }
    }
}
