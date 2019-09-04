using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Palladin.Services.LogicService
{
    internal static class Crypto
    {
        private const int Keysize = 256;
        private const int DerivationIterations = 1000;
        /// <summary>
        /// eSecurity - Serviços e Treinamentos em Segurança Cibernética (Pentest)
        /// </summary>
        private const string passPhrase = "a0ae5eb9a140bd4d2c2a654055c59177";

        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="plainText">String to be encrypted</param>
        /// <param name="password">Password</param>
        public static string Encrypt(string plainText)
        {
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = Encoding.UTF8.GetBytes(passPhrase);
            passwordBytes = SHA512.Create().ComputeHash(passwordBytes);
            var bytesEncrypted = Crypto.Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(bytesEncrypted);
        }

        /// <summary>
        /// Decrypt a string.
        /// </summary>
        /// <param name="encryptedText">String to be decrypted</param>
        /// <param name="password">Password used during encryption</param>
        /// <exception cref="FormatException"></exception>
        public static string Decrypt(string encryptedText)
        {
            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(passPhrase);
            passwordBytes = SHA512.Create().ComputeHash(passwordBytes);
            var bytesDecrypted = Crypto.Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }

        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            var saltBytes = new byte[] { 7,4,3,8,6,2,7,9 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            var saltBytes = new byte[] { 7, 4, 3, 8, 6, 2, 7, 9 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
    }
}
