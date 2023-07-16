using System.ComponentModel.DataAnnotations;
using CrossCutting.Validations;

namespace CrossCutting.DTOs
{
	public class EventCreationDto
	{
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
		[PrimeraLetraMayuscula]
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime EndtDateTime { get; set; }
		public string Location { get; set; } // se va 

		//[Url]
		//public string UrlStream { get; set; }
		//public tipo Image { get; set; }

	}
}
