
using CrossCutting.DTOs.Standar;
using CrossCutting.DTOs;
using Persistence.Repository.Interfaces;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.Implementations
{
	public class EventService : IEventService
	{

		private readonly IEventRepository eventRepository;

		public EventService(IEventRepository eventRepository)
		{
			this.eventRepository = eventRepository;
		}

		public async Task<List<EventDto>> GetAll()
		{
			try
			{
				return await eventRepository.GetAll();
			}
			catch (Exception)
			{
				throw;
			}
		}

		//public async Task<List<CategoryDto>> GetById(int id)
		//{
		//	try
		//	{
		//		return await eventRepository.GetById(id);
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

		public async Task<ResponseDto> Create(int categoryId, EventCreationDto eventCreationDto, string email)
		{
			var response = new ResponseDto();
			try
			{
				response = await eventRepository.Create(categoryId, eventCreationDto, email);
			}
			catch (Exception)
			{
				throw;
			}
			return response;
		}

		public async Task<ResponseDto> Update(EventCreationDto eventCreationDto, int categoryId, int id)
		{
			var response = new ResponseDto();
			try
			{
				response = await eventRepository.Update(eventCreationDto, categoryId,id);
			}
			catch (Exception)
			{
				throw;
			}
			return response;
		}

		public async Task<ResponseDto> Delete(int id)
		{
			var response = new ResponseDto();
			try
			{
				response = await eventRepository.Delete(id);
			}
			catch (Exception)
			{
				throw;
			}
			return response;
		}
	}
}
