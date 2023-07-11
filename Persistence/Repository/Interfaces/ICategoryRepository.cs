
using CrossCutting.DTOs;
using CrossCutting.DTOs.Standar;

namespace Persistence.Repository.Interfaces
{
	public interface ICategoryRepository
	{
		public Task<List<CategoryDto>> GetAll();
		public  Task<List<CategoryDto>> GetById(int id);
		public  Task<ResponseDto> Create(CategoryCreationDto categoryCreationDto);
		public  Task<ResponseDto> Update(CategoryCreationDto categoryCreationDto, int id);
		public  Task<ResponseDto> Delete(int id);
	}
}
