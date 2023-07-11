
using AutoMapper;
using CrossCutting.DTOs;
using Persistence.Entities;

namespace Persistence.Mapping
{
	public class EventMapperProfile : Profile
	{
        public EventMapperProfile()
        {
			CreateMap<EventCreationDto, Event>();
			CreateMap<Event, EventDto>().ReverseMap();
		}
	}
}
