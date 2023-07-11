
using Application.Interfaces;
using AutoMapper;
using CrossCutting.DTOs;
using CrossCutting.DTOs.Standar;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using System;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/v1/events")] //cambiar segun requerimientos
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class EventsController : ControllerBase
	{
		private readonly IEventService eventService;

		public EventsController(IEventService eventService)
		{
			this.eventService = eventService;
		}


		[HttpGet]
		public async Task<ActionResult<List<EventDto>>> Get()
		{
			return await eventService.GetAll();
		}


		[HttpPost]
		public async Task<ActionResult> Post(int categoryId, EventCreationDto eventCreationDto)
		{
			try
			{
				var emailClaim = HttpContext.User.Claims.Where(c => c.Type == "email").FirstOrDefault();
				var email = emailClaim.Value;
				var response = await eventService.Create(categoryId, eventCreationDto, email);
			}
			catch (Exception)
			{
				throw;
			}
			return Ok();
		}


		[HttpPut("{id:int}")]
		public async Task<ActionResult<ResponseDto>> Put(EventCreationDto eventCreationDto, int categoryId, int id)
		{
			try
			{
				var response = await eventService.Update(eventCreationDto, categoryId,id);
			}
			catch (Exception)
			{
				throw;
			}
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		[Authorize(Policy = "EsAdminPolicy")]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				var response = await eventService.Delete(id);
			}
			catch (Exception)
			{
				throw;
			}
			return NoContent();
		}
	}
}
