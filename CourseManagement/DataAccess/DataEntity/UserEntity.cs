using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.DataAccess.DataEntity
{
    public class UserEntity
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }//密码
        public string Avatar { get; set; }//是否有效
        public int Gender { get; set; }//性别
    }
}
