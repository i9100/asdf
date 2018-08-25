using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace STG.Score
{
    class Manager
    {
        const string filename = "Score.dat";

        const string password = "YASDfs8asd3!!=cC#di7";

        static readonly byte[] salt = new byte[] { 1, 49, 38, 100, 26, 67, 29, 2, 17, 241 };

        public static string ReadHighScore()
        {
            byte[] cipher = File.ReadAllBytes(filename);

            string text = Decrypt(cipher);

            return text;
        }

        public static void WriteHighScore(int score)
        {
            byte[] cipher = Encrypt(score.ToString());

            File.WriteAllBytes(filename, cipher);
        }

        private static byte[] Encrypt(string text)
        {
            byte[] encrypted;

            using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt))
            using (Rijndael rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = deriveBytes.GetBytes(rijAlg.KeySize / 8);
                rijAlg.IV = deriveBytes.GetBytes(rijAlg.BlockSize / 8);

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        private static string Decrypt(byte[] cipher)
        {
            string text;

            using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt))
            using (Rijndael rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = deriveBytes.GetBytes(rijAlg.KeySize / 8);
                rijAlg.IV = deriveBytes.GetBytes(rijAlg.BlockSize / 8);

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipher))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            text = srDecrypt.ReadToEnd();
                        }
                    }
                }

                return text;
            }
        }
    }
}
