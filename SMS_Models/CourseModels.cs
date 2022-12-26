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

    /// <summary>
    /// 课程
    /// </summary>
    public class Courses
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? created_at { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? updated_at { get; set; }
        /// <summary>
        /// 课程id
        /// </summary>
        public string? course_id { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string? course_name { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string? description { get; set; }
        /// <summary>
        /// 是否发布：<br/>0：未发布<br/>1：已发布
        /// </summary>
        public int? is_publish { get; set; }
        /// <summary>
        /// 课程封面
        /// </summary>
        public string? course_cover { get; set; }
        /// <summary>
        /// 课程链接
        /// </summary>
        public string? course_url { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? is_delete { get; set; }
        /// <summary>
        /// 课程分类id
        /// </summary>
        public int? course_type_id { get; set; }
    }
}
