using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Model
{
    public class CourseModel
    {
        public string CourseName { get; set; }//课程名称
        public string Cover { get; set; }//左侧课程封面
        public string Url { get; set; }
        public string Description { get; set; }//描述

        public List<string> Teachers { get; set; }//老师，这里需要一个列表

        public bool IsShowSkeleton { get; set; }
    }
}
