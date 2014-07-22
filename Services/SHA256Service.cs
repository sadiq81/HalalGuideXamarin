using System;
using System.Security.Cryptography;
using System.Text;

namespace HalalGuide.Core
{
	public class SHA256Service : S3Storage.Service.ISHA256Service, SimpleDBPersistence.Service.ISHA256Service
	{
		public static HashAlgorithm CanonicalRequestHashAlgorithm = HashAlgorithm.Create ("SHA-256");

		public string CreateHash (string message, string secret)
		{
			return Convert.ToBase64String (CreateHash (Encoding.UTF8.GetBytes (secret), Encoding.UTF8.GetBytes (message)));
		}

		public byte[] CreateHash (byte[] key, byte[] buffer)
		{
			var kha = KeyedHashAlgorithm.Create ("HMACSHA256");
			kha.Key = key;
			var hash_1 = kha.ComputeHash (buffer);
			return hash_1;

		}

		public byte[] CreateHash (byte[] buffer)
		{
			var hash_1 = CanonicalRequestHashAlgorithm.ComputeHash (buffer);
			return hash_1;
		}
	}
}

