using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace STG.Score
{
    static class Manager
    {
        const string filename = "Score.dat";

        const string password = "YASDfs8asd3!!=cC#di7";

        static readonly byte[] salt = new byte[] { 1, 49, 38, 100, 26, 67, 29, 2, 17, 241 };

        public static bool Exists { get { return File.Exists(filename); } }

        public static string ReadScore()
        {
            try
            {
                byte[] cipher = File.ReadAllBytes(filename);

                string text = Decrypt(cipher);

                return text;
            }
            catch (FileNotFoundException e)
            {
                return "[]";
            }
        }

        public static void WriteScore(string name, int score)
        {
            JArray scoreArray = JArray.Parse(ReadScore());

            scoreArray.Append(name, score);

            byte[] cipher = Encrypt(JsonConvert.SerializeObject(scoreArray));

            File.WriteAllBytes(filename, cipher);
        }

        public static IEnumerable<Schema> ParseScore(string jArray)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Schema>>(jArray);
        }

        private static void Append(this JArray jArray, string name, int score)
        {
            DateTime localDate = DateTime.Now;

            var data = new JObject
            {
                { "name", name },

                { "score", score },

                { "date", localDate.ToString() }
            };

            if (jArray.Count > 10)
                jArray.RemoveAt(9);

            jArray.Add(data);
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
