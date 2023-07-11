
using AutoMapper;
using CrossCutting.DTOs.Standar;
using CrossCutting.DTOs;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Repository.Implementations
{
	public class EventRepository : IEventRepository
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;
		private readonly UserManager<IdentityUser> userManager;

		public EventRepository(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
			this.context = context;
			this.mapper = mapper;
			this.userManager = userManager;
		}


		public async Task<List<EventDto>> GetAll()
		{
			try
			{
				var events = await context.Events.ToListAsync();
				return mapper.Map<List<EventDto>>(events);
			}
			catch (Exception)
			{
				throw;
			}
		}

		//public async Task<List<EventDto>> GetById(int categoryId)
		//{
		//	try
		//	{
		//		var existeCategory = await context.Categories.AnyAsync(x => x.Id == categoryId);
		//		var eventos = await context.Events.Where(x => x.CategoryId == categoryId).ToListAsync();
		//		return mapper.Map<List<EventoDto>>(eventos);
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

		public async Task<ResponseDto> Create(int categoryId,EventCreationDto eventCreationDto,string email)
		{
			var response = new ResponseDto();
			try
			{
				var user = await userManager.FindByEmailAsync(email);
				var userId = user.Id;

				var existeCategory = await context.Categories.AnyAsync(x => x.Id == categoryId);
				var evento = mapper.Map<Event>(eventCreationDto);
				evento.UserId = userId;
				evento.CategoryId = categoryId;
				context.Add(evento);
				await context.SaveChangesAsync();
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
				var existeCategory = await context.Categories.AnyAsync(x => x.Id == categoryId);
				var existe = await context.Events.AnyAsync(x => x.Id == id);

				var evento = mapper.Map<Event>(eventCreationDto);
				evento.Id = id;
				evento.CategoryId = categoryId;
				context.Update(evento);
				await context.SaveChangesAsync();
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
				var existe = await context.Events.AnyAsync(x => x.Id == id);
				context.Remove(new Event() { Id = id });
				await context.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
			return response;
		}
	}
}

