using AutoMapper;
using MB_API.Data.Entities.Identity;
using MB_API.Data.Entities;
using MB_API.Models.Event;


namespace MB_API.Mapper
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<EventEntity, EventModel>();
        }
       
    }
}
