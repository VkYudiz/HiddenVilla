using AutoMapper;
using DataAcesss.Data;
using Models;

namespace BusinessLayer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoomDTO, HotelRoom>();
            CreateMap<HotelRoom, HotelRoomDTO>();
            CreateMap<HotelRoomImage, HotelRoomImageDTO>().ReverseMap();
        }
    }
}
