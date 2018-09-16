using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    public class SecurityHelper
    {
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SHA1(string input)
        {
            SHA1 shaHash = System.Security.Cryptography.SHA1.Create();
            byte[] data = shaHash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString().ToUpper();
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string md5(string str, Encoding encoding = null)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            if (encoding == null)
                bytValue = System.Text.Encoding.UTF8.GetBytes(str);
            else
                bytValue = encoding.GetBytes(str);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            str = sTemp.ToLower();

            return str;
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string Base64(string str, Encoding encodeType)
        {
            return  Convert.ToBase64String(encodeType.GetBytes(str));
        }
    }
}
