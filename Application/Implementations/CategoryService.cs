using Application.Interfaces;
using AutoMapper;
using Azure;
using CrossCutting.DTOs;
using CrossCutting.DTOs.Standar;
using Persistence.Entities;
using Persistence.Repository.Interfaces;

namespace Application.Implementations
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository categoryRepository;

		public CategoryService(ICategoryRepository categoryRepository)
        {
			this.categoryRepository = categoryRepository;
		}

		public async Task<List<CategoryDto>> GetAll()
		{
			try
			{
				return await categoryRepository.GetAll();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<CategoryDto>> GetById(int id)
		{
			try
			{
				return await categoryRepository.GetById(id);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<ResponseDto> Create(CategoryCreationDto categoryCreationDto)
		{	
			var response = new ResponseDto();
			try
			{
				 response = await categoryRepository.Create(categoryCreationDto);
			}
			catch (Exception)
			{
				throw;
			}
			return response;
		}

		public async Task<ResponseDto> Update(CategoryCreationDto categoryCreationDto, int id)
		{
			var response = new ResponseDto();
			try
			{
				response = await categoryRepository.Update(categoryCreationDto,id);
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
				response = await categoryRepository.Delete(id);
			}
			catch (Exception)
			{
				throw;
			}
			return response;
		}


	}
}
