﻿using System.ComponentModel.DataAnnotations;

namespace CrossCutting.DTOs
{
	public class EditAdminDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
