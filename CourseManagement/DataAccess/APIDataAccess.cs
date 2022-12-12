using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.DataAccess
{
    public class APIDataAccess
    {
        public APIDataAccess() { }
        public static List<CourseModels> GetCourseModels()
        {
            try
            {
                var data = GetArrayInfo("sms_course_teacher_relation?select=course_id,teacher_id");//课程以及对应教师编号
                var data1 = GetArrayInfo("sms_member?is_validation=eq.1&select=user_id,real_name");//课程以及对应教师编号
                var vs = GetListInfo<CourseCategory>("sms_member?is_validation=eq.1&select=user_id,real_name");



                List<int> listA = new List<int> { 1, 2, 3, 5, 7, 9 };
                List<int> listB = new List<int> { 13, 4, 17, 29, 2 };

                List<int> Result = listA.Union(listB).ToList<int>();          //剔除重复项
                List<int> Result1 = listA.Concat(listB).ToList<int>(); 

                var s = GetArrayInfo("sms_course");
                string Text = s[0]["course_name"];
                var vslist = new List<CourseModels>();
                foreach (var item in s)
                {
                    vslist.Add(new CourseModels { CourseName = item["course_name"], Cover = item["course_cover"], Url = item["course_url"], Description = item["description"] });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return new List<CourseModels>();
        }
    }
}
