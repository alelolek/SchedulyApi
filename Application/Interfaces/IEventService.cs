using System.Security.Claims;
using CrossCutting.DTOs;
using CrossCutting.DTOs.Standar;

namespace Application.Interfaces
{
	public interface IEventService
	{
		public  Task<List<EventDto>> GetAll();
		public Task<ResponseDto> Create(int categoryId, EventCreationDto eventCreationDto,string email);
		public Task<ResponseDto> Update(EventCreationDto eventCreationDto, int categoryId, int id);
		public  Task<ResponseDto> Delete(int id);


	}
}
