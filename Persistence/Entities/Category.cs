﻿
namespace Persistence.Entities
{
	public class Category
	{
        public int Id { get; set; }
		public string Name { get; set; }
		public List<Event> Eventos { get; set; }
	}
}
