using StudentManagementSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Model
{
    public class LoginModel : NotifyBase
    {
        private string _userName="admin";
        private string _password="123456";
        private string _validationCode="1234";
        private string _randomField="1234";

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                this.DoNotify();
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.DoNotify();
            }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidationCode
        {
            get { return _validationCode; }
            set
            {
                _validationCode = value;
                this.DoNotify();
            }
        }

        /// <summary>
        /// 验证码随机数
        /// </summary>
        public string RandomField
        {
            get { return _randomField; }
            set
            {
                _randomField = value;
                this.DoNotify();
            }
        }

    }
}
