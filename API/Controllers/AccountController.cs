using API.Servicio;
using CrossCutting.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
	[ApiController]
	[Route("api/v1/accounts")]
	//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly IConfiguration configuration;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly HashService hashService;
		private readonly IDataProtector dataProtector;
		public AccountController(UserManager<IdentityUser> userManager, 
			IConfiguration configuration, 
			SignInManager<IdentityUser> signInManager,
			IDataProtectionProvider dataProtectionProvider,
			HashService hashService)
		{
			this.userManager = userManager;
			this.configuration = configuration;
			this.signInManager = signInManager;
			this.hashService = hashService;
			dataProtector = dataProtectionProvider.CreateProtector("valor_umico_y_protegido");
		}

		[HttpGet("hash/{textoPlano}")]
		public ActionResult RealizarHash(string textoPlano)
		{
			var resultado1 = hashService.Hash(textoPlano);
			var resultado2 = hashService.Hash(textoPlano);
			return Ok(new
			{
				textoPlano = textoPlano,
				Hash1 = resultado1,
				Hash2 = resultado2
			});
		}

		[HttpGet("encriptar")]
		public ActionResult Encriptar()
		{
			var textoPlano = "Alejandra";
			var textoCifrado = dataProtector.Protect(textoPlano);
			var textoDesencriptado = dataProtector.Unprotect(textoCifrado);

			return Ok(new
			{
				textoPlano = textoPlano,
				textoCifrado = textoCifrado,
				textoDesencriptado = textoDesencriptado
			});
		}

		[HttpGet("encriptarPorTiempo")]
		public ActionResult EncriptarPorTiempo()
		{
			var protectorLimitadoPorTiempo = dataProtector.ToTimeLimitedDataProtector();

			var textoPlano = "Alejandra";
			var textoCifrado = protectorLimitadoPorTiempo.Protect(textoPlano, lifetime: TimeSpan.FromSeconds(5));
			Thread.Sleep(6000);
			var textoDesencriptado = protectorLimitadoPorTiempo.Unprotect(textoCifrado);

			return Ok(new
			{
				textoPlano = textoPlano,
				textoCifrado = textoCifrado,
				textoDesencriptado = textoDesencriptado
			});
		}


		[HttpPost("register")] //api/accounts/register
		[Authorize(Policy = "EsAdminPolicy")]
		public async Task<ActionResult<ResponseAuthentication>> Register(CredentialAccount credentialsAccount)
		{
			var user = new IdentityUser
			{
				UserName = credentialsAccount.Email,
				Email = credentialsAccount.Email
			};
			var resultado = await userManager.CreateAsync(user, credentialsAccount.Password);

			if (resultado.Succeeded)
			{
				return await BuildToken(credentialsAccount);
			}
			else
			{
				return BadRequest(resultado.Errors);
			}
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<ActionResult<ResponseAuthentication>> Login(CredentialAccount credentialsAccount)
		{
			var result = await signInManager.PasswordSignInAsync(credentialsAccount.Email,
				credentialsAccount.Password, isPersistent: false, lockoutOnFailure: false);

			if (result.Succeeded)
			{
				return await BuildToken(credentialsAccount);
			}
			else
			{
				return BadRequest("Login Incorrecto");
			}
		}

		[HttpGet("RenewToken")]
		[Authorize(Policy = "EsAdminPolicy")]
		public async Task<ActionResult<ResponseAuthentication>> Renew()
		{
			var emailClaim = HttpContext.User.Claims.Where(c => c.Type == "email").FirstOrDefault();
			var email = emailClaim.Value;
			var credentialUser = new CredentialAccount()
			{
				Email = email
			};
			return await BuildToken(credentialUser);
		}

		private async Task<ResponseAuthentication> BuildToken(CredentialAccount credentialsAccount)
		{
			var claims = new List<Claim>()
			{
				new Claim("email", credentialsAccount.Email),
				new Claim("otra cosa","cualquier otro valor")
			};
			var user = await userManager.FindByEmailAsync(credentialsAccount.Email);
			var claimDB = await userManager.GetClaimsAsync(user);

			claims.AddRange(claimDB);

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyJwt"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expiration = DateTime.UtcNow.AddYears(1);

			var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);

			return new ResponseAuthentication
			{
				Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
				DateExpiration = expiration
			};
		}

		[HttpPost("HacerAdmin")]
		[Authorize(Policy = "EsAdminPolicy")]
		public async Task<ActionResult> HacerAdmin(EditAdminDto editAdminDto)
		{
			var user = await userManager.FindByEmailAsync(editAdminDto.Email);
			await userManager.AddClaimAsync(user, new Claim("EsAdmin", "1"));
			return NoContent();
		}

		[HttpPost("RemoveAdmin")]
		[Authorize(Policy = "EsAdminPolicy")]
		public async Task<ActionResult> RemoveAdmin(EditAdminDto editAdminDto)
		{
			var user = await userManager.FindByEmailAsync(editAdminDto.Email);
			await userManager.RemoveClaimAsync(user, new Claim("EsAdmin", "1"));
			return NoContent();
		}
	}
}
