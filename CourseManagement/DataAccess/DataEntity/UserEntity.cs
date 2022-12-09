using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.DataAccess.DataEntity
{
    public class UserEntity
    {
        /// <summary>
        /// 账号名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public long Gender { get; set; }
    }
}
