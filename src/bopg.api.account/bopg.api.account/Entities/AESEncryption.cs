using System.Security.Cryptography;
using System.Text;
using System;
using System.IO;

namespace bopg.api.account.Entities
{
    public class Encryption
    {
        private static Encryption obj;
        private static Aes objAES;
        private byte[] aesKey;
        private byte[] aesIV;

        public static Encryption GetInstance() { { return obj ?? (obj = new Encryption()); } }

        private Encryption()
        {
            objAES = Aes.Create();
            //aesKey = Convert.FromBase64String(Program.Configuration.GetSection("Encryption:Key").Value);
            //aesIV = Convert.FromBase64String(Program.Configuration.GetSection("Encryption:IV").Value);
        }

        public string Encrypt(string plainText, string key)
        {
            string retVal = string.Empty;
            aesKey = Convert.FromBase64String(key);
            aesIV = Convert.FromBase64String(key);
            ICryptoTransform encryptor = objAES.CreateEncryptor(aesKey, aesIV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    var encrypted = msEncrypt.ToArray();

                    retVal = Convert.ToBase64String(encrypted);
                }
            }

            return retVal;
        }

        public string Decrypt(string encryptedText, string key)
        {
            string retVal = string.Empty;
            byte[] cipherText = Convert.FromBase64String(encryptedText);
            aesKey = Convert.FromBase64String(key);
            aesIV = Convert.FromBase64String(key);
            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = objAES.CreateDecryptor(aesKey, aesIV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        retVal = srDecrypt.ReadToEnd();
                    }
                }
            }

            return retVal;
        }
    }
}