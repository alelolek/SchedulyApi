﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Entities
{
	public class Event
	{
        public int Id { get; set; }

		[Required]
		public string Title { get; set; }
        public string Description { get; set; }
		[Required]
		public DateTime StartDateTime { get; set; }
		[Required]
		public DateTime EndtDateTime { get; set; }
        public string Location { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public string UserId { get; set; }
		public IdentityUser User { get; set; }

	}
}
