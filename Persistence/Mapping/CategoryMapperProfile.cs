using System;
using System.Collections.Generic;
using AutoMapper;
using CrossCutting.DTOs;
using Persistence.Entities;

namespace Persistence.Mapping
{
	public class CategoryMapperProfile : Profile
	{
        public CategoryMapperProfile()
        {
			CreateMap<CategoryCreationDto, Category>();
			CreateMap<Category, CategoryDto>();
		}
	}
}
