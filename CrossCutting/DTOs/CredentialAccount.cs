
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.DTOs
{
	public class CredentialAccount
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
