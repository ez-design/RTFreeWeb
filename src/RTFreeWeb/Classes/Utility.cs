using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RTFreeWeb.Classes
{
    public class Utility
    {
        public class Text
        {
            /// <summary>
            /// DateTimeに変換
            /// </summary>
            /// <param name="src"></param>
            /// <returns></returns>
            static public DateTime StringToDate(string src)
            {
                src = Regex.Replace(src, "[^0-9]", "");
                if (src.Length < 14)
                {
                    return DateTime.MinValue;
                }

                DateTime res;
                int year  = int.TryParse(src.Substring(0, 4) , out year)  ? year  : 0;
                int month = int.TryParse(src.Substring(4, 2) , out month) ? month : 0;
                int day   = int.TryParse(src.Substring(6, 2) , out day)   ? day   : 0;
                int hour  = int.TryParse(src.Substring(8, 2) , out hour)  ? hour  : 0;
                int min   = int.TryParse(src.Substring(10, 2), out min)   ? min   : 0;
                int sec   = int.TryParse(src.Substring(12, 2), out sec)   ? sec   : 0;

                if (year == 0)
                {
                    return DateTime.MinValue;
                }

                res = new DateTime(year, month, day, hour, min, sec);
                return res;

            }

            /// <summary>
            /// パスワード作成
            /// </summary>
            /// <param name="src"></param>
            /// <returns></returns>
            public static string CreatePassword(string src)
            {
                string res = src;
                for(int i=0; i < 20000; i++)
                {
                    res = Sha256(res);
                }
                return res;

            }

            /// <summary>
            /// SHA256
            /// </summary>
            /// <param name="src"></param>
            /// <returns></returns>
            public static string Sha256(string src)
            {
                using (var sha256 = SHA256.Create())
                {
                    byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(src));
                    var hash = BitConverter.ToString(data).Replace("-", "").ToLower();

                    return hash;

                }
            }

            /*public static string Encrypt(string src, string key)
            {
                // AES暗号化サービスプロバイダ
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.BlockSize = 128;
                aes.KeySize = 128;
                aes.IV = Encoding.UTF8.GetBytes(AesIV);
                aes.Key = Encoding.UTF8.GetBytes(AesKey);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // 文字列をバイト型配列に変換
                byte[] src = Encoding.Unicode.GetBytes(text);

                // 暗号化する
                using (ICryptoTransform encrypt = aes.CreateEncryptor())
                {
                    byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                    // バイト型配列からBase64形式の文字列に変換
                    return Convert.ToBase64String(dest);
                }
            }

            public static string Decrypt(string src, string key)
            {

            }*/
        }

    }
}
