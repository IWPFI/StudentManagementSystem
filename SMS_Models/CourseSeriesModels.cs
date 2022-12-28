using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    public class CourseSeriesModels
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        public string course_id { get; set; }
        /// <summary>
        /// 平台ID
        /// </summary>
        public string platform_id { get; set; }
        /// <summary>
        /// 播放量
        /// </summary>
        public int play_count { get; set; }
        /// <summary>
        /// 是否增长：<br/>1:增长<br/>2:下降
        /// </summary>
        public int is_growing { get; set; }
        /// <summary>
        /// 增长率
        /// </summary>
        public int growing_rate { get; set; }
    }

}
