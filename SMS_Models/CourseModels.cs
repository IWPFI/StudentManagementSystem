using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    /// <summary>
    /// 课程模型
    /// </summary>
    public class CourseModels
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 左侧课程封面
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 教师列表
        /// </summary>
        public List<string> Teachers { get; set; }

        /// <summary>
        /// 骨架屏
        /// </summary>
        public bool IsShowSkeleton { get; set; }
    }
}
