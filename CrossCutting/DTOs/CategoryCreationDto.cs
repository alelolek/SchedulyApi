

using System.ComponentModel.DataAnnotations;

namespace CrossCutting.DTOs
{
	public class CategoryCreationDto
	{
		//[Required(ErrorMessage = "El campo {0} es requerido")]
		//[StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
		//[PrimeraLetraMayuscula]
		public string Name { get; set; }
	}
}
