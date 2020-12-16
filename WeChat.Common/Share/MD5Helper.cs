using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WeChat.Common.Share
{
    public class MD5Helper
    {
        public static string MD5Encrypt32(string password)
        {
            string cl = password;
            string val = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                val = val + s[i].ToString("X");
            }
            return val;
        }
    }
}
