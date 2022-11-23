using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//对用户密码进行加密
namespace StudentManagementSystem.Common
{
    /// <summary>
    /// 密码加密处理
    /// </summary>
    class MD5Provider
    {
        public static string GetMD5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] pws = md5.ComputeHash(Encoding.UTF8.GetBytes(str));/*哈希处理*/
            string pwd = "";
            foreach (var item in pws)/*遍历每个字节*/
            {
                pwd += item.ToString("X2");
            }
            return pwd;
        }
    }
}
