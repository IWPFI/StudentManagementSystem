namespace SMS_Models
{
    public class LoginModels
    {
        /// <summary>
        /// 账号名称
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string real_name { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public int is_validation { get; set; }
        public int is_can_login { get; set; }
        /// <summary>
        /// 是否是老师
        /// </summary>
        public int is_teacher { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
    }
}