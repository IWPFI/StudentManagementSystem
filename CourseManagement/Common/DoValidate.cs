using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace StudentManagementSystem.Common
{
    public class DoValidate
    {
        /// <summary>
        /// 检查QQ
        /// </summary>
        /// <param name="Str">qq字符串</param>
        /// <returns>合法返回 true</returns>
        public static bool CheckQQ(string Str)//QQ
        {
            Regex QQReg = new Regex(@"^\d{9,10}?$");
            return QQReg.IsMatch(Str);
        }

        /// <summary>
        /// 检查手机号
        /// </summary>
        /// <param name="Str">手机号</param>
        /// <returns>合法返回 true</returns>
        public static bool CheckCellPhone(string Str)//手机
        {
            Regex CellPhoneReg = new Regex(@"^1[358][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$");
            return CellPhoneReg.IsMatch(Str);
        }

        /// <summary>
        /// 检查E-Mail是否合法
        /// </summary>
        /// <param name="Str">要检查的E-mail字符串</param>
        /// <returns>合法返回true</returns>
        public static bool CheckEMail(string Str)//E-mail
        {
            Regex emailReg = new Regex(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$");
            return emailReg.IsMatch(Str);

        }

        /// <summary>
        /// 检查姓名是否合法
        /// </summary>
        /// <param name="nameStr">要检查的内容</param>
        /// <returns></returns>
        public static bool CheckName(string nameStr)//检查姓名是否合法
        {
            Regex nameReg = new Regex(@"^[\u4e00-\u9fa5]{0,}$");//为汉字
            Regex nameReg2 = new Regex(@"^\w+$");//字母
            if (nameReg.IsMatch(nameStr) || nameReg2.IsMatch(nameStr))//为汉字或字母
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
