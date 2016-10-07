using System;
using System.Security.Cryptography;
using System.IO;

namespace Chapter18
{
	/// <summary>
	/// Summary description for EncryptionUtil.
	/// </summary>
	public class EncryptionUtil
	{
		public static byte[] EncryptString(string stringToEncrypt, SymmetricAlgorithm crypt)
		{
			// Create a cryptographic stream for encryption.
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms,
				crypt.CreateEncryptor(), CryptoStreamMode.Write);

			// Write the string to binary data with the help of a BinaryWriter.
			BinaryWriter w = new BinaryWriter(cs);
			w.Write(stringToEncrypt);
			w.Flush();
			
			// All the data has been written. Now, make sure the last block
			// is properly padded. Failing to do this will cause an error
			// when you attempt to decrypt the data.
			cs.FlushFinalBlock();

			// Now move the encrypted data out of the stream,
			// and into an array of bytes.
			return ms.ToArray();	
		}

		public static string DecryptToString(byte[] dataToDecrypt, SymmetricAlgorithm crypt)
		{
			// Create a cryptographic stream for decryption.
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms,
				crypt.CreateDecryptor(), CryptoStreamMode.Write);

			// Write the binary data to the memory stream.
			cs.Write(dataToDecrypt, 0, dataToDecrypt.Length);
			cs.FlushFinalBlock();

			// Read the unencrypted data from the stream into a string,
			// with the help of the BinaryReader.
			BinaryReader r = new BinaryReader(ms);
			ms.Position = 0;
			string decryptedData = r.ReadString();
			r.Close();

			return decryptedData;	
		}
	}
}
