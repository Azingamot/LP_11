using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using System.Text;

namespace LP_11.Utils
{
	public class Encryptor
	{
		public static T Encrypt<T>(string text)
		{
			try
			{
				string key = Key.value;
				byte[] clear = Encoding.Unicode.GetBytes(text);
				using (Aes encryptor = Aes.Create())
				{
					Rfc2898DeriveBytes pdb = new(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }, 1000, HashAlgorithmName.SHA1);
					encryptor.Key = pdb.GetBytes(32);
					encryptor.IV = pdb.GetBytes(16);

					using (MemoryStream ms = new MemoryStream())
					{
						using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
						{
							cs.Write(clear, 0, clear.Length);
							cs.Close();
						}
						text = Convert.ToBase64String(ms.ToArray());
						clear = ms.ToArray();
					}
				}
				
				if (typeof(T) == typeof(byte[]))
				{
					return (T)(Object)clear;
				}
				else if(typeof(T) == typeof(string))
				{
					return (T)(Object)text;
				}
				else
				{
					return (T)(Object)0;
				}
			}
			catch
			{
				return (T)(Object)0;
			}
		}

		public static string Decrypt(string text)
		{
			try
			{
				string key = Key.value;
				text = text.Replace(" ", "+");
				byte[] textBytes = Convert.FromBase64String(text);
				using (Aes encryptor = Aes.Create())
				{
					Rfc2898DeriveBytes pdb = new(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }, 1000, HashAlgorithmName.SHA1);
					encryptor.Key = pdb.GetBytes(32);
					encryptor.IV = pdb.GetBytes(16);
					using (MemoryStream ms = new MemoryStream())
					{
						using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
						{
							cs.Write(textBytes, 0, textBytes.Length);
							cs.Close();
						}
						text = Encoding.Unicode.GetString(ms.ToArray());
					}
				}
				return text;
			}
			catch
			{
				return "";
			}
		}
	}
}
