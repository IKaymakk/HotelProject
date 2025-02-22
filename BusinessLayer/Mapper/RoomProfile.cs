using AutoMapper;
using EntityLayer.DTO;
using EntityLayer.Entities;

namespace BusinessLayer.Mapper
{
 
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDTO>()
                .ForMember(dest => dest.RoomImages, opt =>
                   opt.MapFrom(src => src.RoomImages.Select(r => new RoomImageDTO
                   {
                       FileData = Convert.ToBase64String(r.FileData),
                       ImageId = r.ImageId
                   }).ToList()))
                .ForMember(dest => dest.RoomFeatures, opt => opt.MapFrom(src => src.RoomFeatures.Select(f =>new RoomFeatureDTO
                {
                    FeatureId=f.FeatureId,
                    Features=f.Features
                }
                )));
        }
    }

}
