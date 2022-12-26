﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using System.Windows.Controls;

namespace StudentManagementSystem.DataAccess
{
    public class APIDataAccess
    {
        public APIDataAccess() { }

        /// <summary>
        /// 获取课程信息
        /// </summary>
        /// <returns></returns>
        public static List<CourseModels> GetCourseModels()
        {
            List<CourseModels> courses = new List<CourseModels>();
            try
            {
                var CourseTeacherList = GetListInfo<CourseIDORTeacherID>("sms_course_teacher_relation?select=course_id,teacher_id");//课程编号丨教师编号|JArray

                var TeacherList = GetListInfo<CourseCategory>("sms_member?is_teacher=eq.1&select=user_id,real_name");//教师编号丨教师名称|List<CourseCategory>

                var CourseTeacherJArray = GetArrayInfo("sms_course_teacher_relation?select=course_id,teacher_id");//课程编号丨教师编号|JArray


                /* NO.1
                JArray TeacherArray = GetArrayInfo("sms_member?is_validation=eq.1&select=user_id,real_name");//教师编号丨教师名称|List<CourseCategory>
                var CoursesJsonString = HttpGetHelp("sms_course");//课程信息|string
                var CoursesJArray = JArray.Parse(CoursesJsonString);
                List<JToken> CoursesJTokenTestList = new List<JToken>();
                foreach (var ss in CoursesJArray)
                {
                    CoursesJTokenTestList.Add(ss);
                }

                var TeacherJsonString = HttpGetHelp("sms_course_teacher_relation?select=course_id,teacher_id");//教师信息|string
                JArray TeacherJArray = JArray.Parse(TeacherJsonString);
                List<JToken> TeacherJTokenTestList = new List<JToken>();
                foreach (var ss in TeacherJArray)
                {
                    TeacherJTokenTestList.Add(ss);
                }
                */

                var sms_course = GetArrayInfo("sms_course");//课程信息|JArray
                foreach (var item in sms_course)
                {
                    /* NO.1
                     var teacherListTestss = (from category in TeacherArray
                                             join prod in CourseTeacherList on category["user_id"] equals prod.teacher_id
                                             where prod.course_id.ToString() == item["course_id"].ToString()
                                             select category["real_name"]).ToList();
                    */

                    /* NO.2
                    var courseTeacherListVs = CourseTeacherList.FindAll(t => t.course_id.ToString() == item["course_id"].ToString());
                    var teacherListTemp = new List<CourseCategory>();
                    foreach (var cource in courseTeacherListVs)
                    {
                        teacherListTemp = TeacherList.Where(t => t.user_id.ToString() == cource.teacher_id.ToString()).ToList();
                    }
                    */

                    var teacherListTemp = (from category in TeacherList
                                           join prod in CourseTeacherList on category.user_id equals prod.teacher_id
                                           where prod.course_id.ToString() == item["course_id"].ToString()
                                           select category.real_name).ToList();

                    courses.Add(new CourseModels
                    {
                        CourseName = item["course_name"],
                        Cover = item["course_cover"],
                        Url = item["course_url"],
                        Description = item["description"],
                        Teachers = teacherListTemp
                    });
                }
            }
            catch (Exception e)
            {
            }
            return courses;
        }
    }
}
