using System;
using System.Security.Cryptography;

namespace BlockChainCourse.Cryptography
{
	public class Hmac
	{
		private const int KeySize = 32;

		public static byte[] GenerateKey()
		{
			using (var randomNumberGenerator = new RNGCryptoServiceProvider())
			{
				var randomNumber = new byte[KeySize];
				randomNumberGenerator.GetBytes(randomNumber);

				return randomNumber;
			}
		}

		public static byte[] ComputeHmacsha256(byte[] toBeHashed, byte[] key)
		{
			using (var hmac = new HMACSHA256(key))
			{
				return hmac.ComputeHash(toBeHashed);
			}
		}
	}
}
