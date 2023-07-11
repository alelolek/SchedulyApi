﻿
using System.Security.Cryptography;
using CrossCutting.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace API.Servicio
{
	public class HashService
	{
		public ResultHash Hash(string textoPlano)
		{
			var sal = new byte[16];
			using(var random = RandomNumberGenerator.Create())
			{
				random.GetBytes(sal);
			}
			return Hash(textoPlano, sal);
		}


		public ResultHash Hash(string textoPlano, byte[] sal)
		{
			var llaveDerivada = KeyDerivation.Pbkdf2(password: textoPlano,
				salt: sal, prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 32);

			var hash = Convert.ToBase64String(llaveDerivada);

			return new ResultHash()
			{
				Hash = hash,
				Sal = sal
			};
		}
	}
}
