using StudentManagementSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Model
{
    public class UserModel : NotifyBase/*NotifyBase改变时通知界面*//*₅用来存放用户信息*/
    {
        private string _avatar;

        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; this.DoNotify(); }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; this.DoNotify(); }
        }

        private int _gender;

        public int Gender
        {
            get { return _gender; }
            set { _gender = value; this.DoNotify(); }
        }
    }
}
