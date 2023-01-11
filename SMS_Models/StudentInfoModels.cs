using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    /// <summary>
    /// 学生信息模型
    /// </summary>
    public class StudentInfoModels
    {
        /// <summary>
        /// 数据库id
        /// </summary>
        public string? id { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string number { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string? birthday { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public int nation_id { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]//不被序列化
        public string nation { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public int politics_status_id { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string politics { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        public string grade { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string site { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int is_delete { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string gmt_create { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string gmt_modified { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string e_mail { get; set; }
        /// <summary>
        /// qq
        /// </summary>
        public string qq { get; set; }
        /// <summary>
        /// 学校
        /// </summary>
        public string school { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string major { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string identity_card { get; set; }
    }

}
