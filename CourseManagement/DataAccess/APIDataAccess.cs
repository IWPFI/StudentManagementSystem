using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudentManagementSystem.Model;
using System.Data;

namespace StudentManagementSystem.DataAccess
{
    public class APIDataAccess
    {
        private static APIDataAccess instance;

        private APIDataAccess() { }
        public static APIDataAccess GetInstance()
        {
            return instance ?? (instance = new APIDataAccess());
        }

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

        /// <summary>
        /// 获取课程播放信息
        /// </summary>
        /// <returns></returns>
        public List<CourseSeriesModel> GetCourseSeries()
        {
            List<CourseSeriesModel> data = new List<CourseSeriesModel>();
            try
            {
                JArray CourseSeriesJArray = GetArrayInfo("sms_play_record?select=course_id,platform_id,play_count,is_growing,growing_rate");//课程播放信息
                JArray CourseNameArray = GetArrayInfo("sms_course?select=course_name,course_id");
                JArray PlatformsArray = GetArrayInfo("sms_platforms?select=platform_id,platform_name");

                string courseId = "";
                CourseSeriesModel cModel = null;

                foreach (var item in CourseSeriesJArray)
                {
                    string tempId = item.Value<string>("course_id");
                    //课程名称
                    var courseName = CourseNameArray.ToList().First(x => ((JObject)x).Value<string>("course_id") == item.Value<string>("course_id"));

                    if (courseId != tempId)
                    {
                        courseId = tempId;
                        cModel = new CourseSeriesModel();
                        data.Add(cModel);

                        cModel.CourseName = courseName.Value<string>("course_name");
                        cModel.SeriesColection = new LiveCharts.SeriesCollection();
                        cModel.SeriesList = new System.Collections.ObjectModel.ObservableCollection<SeriesModel>();
                    }
                    if (cModel != null)
                    {
                        //平台名称
                        string platformName = (PlatformsArray.First(x => x.Value<string>("platform_id") == item.Value<string>("platform_id"))).Value<string>("platform_name");

                        cModel.SeriesColection.Add(new PieSeries
                        {
                            Title = platformName,
                            Values = new ChartValues<ObservableValue> { new ObservableValue((double)item.Value<long>("play_count")) },
                            DataContext = false
                        });

                        cModel.SeriesList.Add(new SeriesModel
                        {
                            SeriesName = platformName,
                            CurrentValue = item.Value<long>("play_count"),
                            IsGrowing = item.Value<Int64>("is_growing") == 1,
                            ChangeRate = item.Value<int>("growing_rate")
                        });
                    }
                }
            }
            catch { }
            return data;
        }
        /// <summary>
        /// 学生信息列表
        /// </summary>
        /// <returns>学生Id、姓名,表ID</returns>
        public Task<List<StudentInformation>> GetStudentList()
        {
            List<StudentInformation> result = new List<StudentInformation>();
            try
            {
                JArray StudentJArray = GetArrayInfo("sms_students?select=*");
                foreach (var item in StudentJArray)
                {
                    result.Add(new StudentInformation
                    {
                        Id = (int)item.Value<Int64>("id"),
                        StudentName = item.Value<String>("name"),
                        StudentID = item.Value<string>("number"),
                        StudentGrade = item.Value<string>("grade")
                    });
                }
                return Task.FromResult(result);
            }
            catch { return null; }
        }

        /// <summary>
        /// 学生详细资料
        /// </summary>
        /// <returns>全部</returns>
        public Task<StudentInformation?> StudentDetails(string str)
        {
            try
            {
                JArray StudentJArray = GetArrayInfo((String.Format("sms_students?select=*&number=eq.{0}", str)));
                if (StudentJArray == null || StudentJArray.Count <= 0)
                    return null;
                JArray nations = GetArrayInfo("nations");
                JArray visage = GetArrayInfo("visage");

                StudentInformation model = new StudentInformation();
                model.Id = (int)StudentJArray[0].Value<Int64>("id");
                model.StudentID = StudentJArray[0].Value<string>("number");
                model.StudentName = StudentJArray[0].Value<string>("name");
                model.StudentSex = (int)StudentJArray[0].Value<Int64>("sex");
                model.StudentPhone = StudentJArray[0].Value<string>("phone");
                model.StudentGrade = StudentJArray[0].Value<string>("grade");
                model.StudentSite = StudentJArray[0].Value<string>("site");
                model.StudentBirthday = StudentJArray[0].Value<DateTime>("birthday");
                model.NationsName = nations.First(x => x["id"].ToString() == StudentJArray[0]["nation_id"].ToString()).Value<string>("nations");
                model.PoliticsStatus = visage.First(x => x["id"].ToString() == StudentJArray[0]["politics_status_id"].ToString()).Value<string>("visage");
                return Task.FromResult(model);
            }
            catch { return null; }
        }

        /// <summary>
        /// 添加学生数据
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<string> AddStudentDataAsync(StudentInfoModels student)
        {
            var result = string.Empty;
            StudentInfoModels studentlist = new StudentInfoModels()
            {
                number = student.number,
                name = student.name,
                phone = student.phone,
                birthday = student.birthday,
                grade = student.grade,
                nation_id = student.nation_id,
                politics_status_id = student.politics_status_id,
                sex = student.sex,
                site = student.site,
                gmt_create = DateTime.Now.ToString("d")
            };
            //result = HttpPostHelp("sms_students", ObjectToJsonNonempty(student));
            result = await HttpPost("sms_students", ObjectToJsonNonempty(student));
            return result;
        }
    }
}
