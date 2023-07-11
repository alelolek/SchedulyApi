using Application.Interfaces;
using AutoMapper;
using CrossCutting.DTOs;
using CrossCutting.DTOs.Standar;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using System;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/v1/categories")]
	//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}

		[HttpGet]
		//[AllowAnonymous]
		public async Task<ActionResult<List<CategoryDto>>> Get()
		{
			return await categoryService.GetAll();
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<List<CategoryDto>>> Get(int id)
		{
			return await categoryService.GetById(id);
		}

		[HttpPost]
		public async Task<ActionResult> Post(CategoryCreationDto categoryCreacionDto)
		{
			try
			{
				var response = await categoryService.Create(categoryCreacionDto);
			}
			catch (Exception)
			{
				throw;
			}
			return Ok();
		}

		[HttpPut("{id:int}")]
		//[Authorize(Policy = "EsAdminPolicy")]
		public async Task<ActionResult> Put(CategoryCreationDto categoryCreacionDto, int id)
		{
			try
			{
				var response = await categoryService.Update(categoryCreacionDto,id);
			}
			catch (Exception)
			{
				throw;
			}
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		//[Authorize(Policy = "EsAdminPolicy")]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				var response = await categoryService.Delete(id);
			}
			catch (Exception)
			{
				throw;
			}
			return NoContent();
		}
	}
}
