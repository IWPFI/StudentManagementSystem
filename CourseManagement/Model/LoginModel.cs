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
        //用户名
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                this.DoNotify();//带通知属性
            }
        }
        //密码
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.DoNotify();//带通知属性
            }
        }
        //验证码
        private string _validationCode;
        public string ValidationCode
        {
            get { return _validationCode; }
            set
            {
                _validationCode = value;
                this.DoNotify();//带通知属性
            }
        }

        private string _randomField;//验证码随机数
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
