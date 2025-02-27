using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.DTO;

namespace BusinessLayer.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomCategory, RoomCategoryDTO>();

            CreateMap<Room, RoomDTO>()
                .ForMember(dest => dest.RoomImages, opt =>
                    opt.MapFrom(src => src.RoomImages.Select(r => new RoomImageDTO
                    {
                        FileData = r.FileData != null ? Convert.ToBase64String(r.FileData) : string.Empty,
                        ImageId = r.ImageId
                    }).ToList()))

             .ForMember(dest => dest.RoomFeatures, opt => opt.MapFrom(src => src.RoomFeatures));

            CreateMap<RoomImages, RoomImageDTO>();

            CreateMap<RoomFeatures, RoomFeatureDTO>();
        }

    }

}
