using System.ComponentModel.DataAnnotations;
using CrossCutting.Validations;

namespace Scheduly.Tests.PruebasUnitarias
{
    [TestClass]
    public class PrimeraLetraMayusculaAttributeTests
    {
        [TestMethod]
        public void PrimeraLetraMinuscula_DevuelveError()
        {
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            var valor = "hola";
            var valContext = new ValidationContext(new { Nombre = valor });

            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            Assert.AreEqual("La primera letra debe ser mayúscula", resultado.ErrorMessage);
        }

		[TestMethod]
		public void ValorNulo_NoDevuelveError()
		{
			var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
			string valor = null;
			var valContext = new ValidationContext(new { Nombre = valor });

			var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            Assert.IsNull(resultado);
		}

		[TestMethod]
		public void ValorConPrimeraLetraMayuscula_NoDevuelveError()
		{
			var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
			string valor = "Hola";
			var valContext = new ValidationContext(new { Nombre = valor });

			var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

			Assert.IsNull(resultado);
		}
	}
}