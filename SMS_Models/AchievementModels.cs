using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    public class AchievementModels
    {
        /// <summary>
        /// 数据库id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 创建事件
        /// </summary>
        public string created_at { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string updated_at { get; set; }
        /// <summary>
        /// 科目
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public string score { get; set; }
        /// <summary>
        /// 评价
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// 学生id
        /// </summary>
        public string student_id { get; set; }

    }
}
