namespace SMS_Models
{
    public class LoginModels
    {
        public string user_name { get; set; }
        public string real_name { get; set; }
        public int is_validation { get; set; }
        public int is_can_login { get; set; }
        public int is_teacher { get; set; }
        public string avatar { get; set; }
        public int gender { get; set; }
        public string email { get; set; }
    }
}