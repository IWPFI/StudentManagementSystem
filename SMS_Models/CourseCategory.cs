using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    public class CourseCategory
    {
        /// <summary>
        /// 教师名称
        /// </summary>
        public string user_id { get; set; }

        public string real_name { get; set; }
    }

    /// <summary>
    /// 课程编号|教师ID
    /// </summary>
    public class CourseIDORTeacherID
    {
        /// <summary>
        /// 
        /// </summary>
        public string course_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string teacher_id { get; set; }
    }

    public class CourseTeacher
    {
        public string course_id { get; set; }

        public string real_name { get; set; }
    }
}
