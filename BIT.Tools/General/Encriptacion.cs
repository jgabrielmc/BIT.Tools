using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;

namespace KMMP.VMO.BP.Logic
{
    public static class Encriptacion
    {
        private static String SecretKey = ConfigurationManager.AppSettings["KeyPasswordKMMP"].ToString();
        private static String SecretIV = ConfigurationManager.AppSettings["IVPasswordKMMP"].ToString();
        private static byte[] byteKey = Encoding.UTF8.GetBytes(SecretKey);
        private static byte[] byteIV = Encoding.UTF8.GetBytes(SecretIV);
        public static string EncryptString(string clearText)
        {
            byte[] plainText = Encoding.UTF8.GetBytes(clearText);
            byte[] CipherBytes;

            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.Mode = CipherMode.CBC;
                //rijndael.Padding = PaddingMode.Zeros;
                ICryptoTransform aesEncryptor = rijndael.CreateEncryptor(byteKey, byteIV);
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aesEncryptor, CryptoStreamMode.Write))
                {
                    cs.Write(plainText, 0, plainText.Length);
                    cs.FlushFinalBlock();
                    CipherBytes = ms.ToArray();
                    ms.Close();
                    cs.Close();
                }
            }
            return Convert.ToBase64String(CipherBytes);
        }

        public static string DecryptString(string cipherText)
        {
            byte[] cipheredData = Convert.FromBase64String(cipherText);
            byte[] plainTextData;
            int decryptedByteCount;

            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.Mode = CipherMode.CBC;
                //rijndael.Padding = PaddingMode.Zeros;
                ICryptoTransform decryptor = rijndael.CreateDecryptor(byteKey, byteIV);
                using (MemoryStream ms = new MemoryStream(cipheredData))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    plainTextData = new byte[cipheredData.Length];
                    decryptedByteCount = cs.Read(plainTextData, 0, plainTextData.Length);
                    ms.Close();
                    cs.Close();
                }
            }
            return Encoding.UTF8.GetString(plainTextData, 0, decryptedByteCount);
        }
    }
}
