using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OmsCommon
{
    public class Md5Util
    {

        // md5 加密
        public static string EncryptString(string str)
        {
            // utf-8 x2
            MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] bn= md5.ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in bn)
            {
                stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

    }
}
