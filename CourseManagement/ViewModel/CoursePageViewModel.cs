using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using System.Collections.ObjectModel;

namespace StudentManagementSystem.ViewModel
{
    public class CoursePageViewModel : ViewModelBase
    {
        /// <summary>
        /// 课程分类
        /// </summary>
        public ObservableCollection<CategoryItemModel> CategoryCourses { get; set; }

        /// <summary>
        /// 技术分类
        /// </summary>
        public ObservableCollection<CategoryItemModel> CategoryTechnology { get; set; }

        /// <summary>
        /// 授课老师
        /// </summary>
        public ObservableCollection<CategoryItemModel> CategoryTeacher { get; set; }

        /// <summary>
        /// 课程信息 | 绑定界面
        /// </summary>
        public ObservableCollection<CourseModels> CourseLists { get; set; } = new ObservableCollection<CourseModels>();

        /// <summary>
        /// 课程信息 | 获取数据[用于筛选]
        /// </summary>
        private List<CourseModels> CourseAll;

        /// <summary>
        /// 点击课程名称时跳转到网页
        /// </summary>
        public CommandBase OpenCourseUrlCommand { get; set; }

        /// <summary>
        /// 教师名称筛选命令
        /// </summary>
        public CommandBase TeacherFilterCommand { get; set; }

        public CoursePageViewModel()
        {
            this.OpenCourseUrlCommand = new CommandBase();
            this.OpenCourseUrlCommand.DoCanExecute = new Func<object, bool>((o) => true);
            this.OpenCourseUrlCommand.DoExecute = new Action<object>((o) => { System.Diagnostics.Process.Start("explorer.exe", o.ToString()); });//调用浏览器打开url

            this.TeacherFilterCommand = new CommandBase();
            this.TeacherFilterCommand.DoCanExecute = new Func<object, bool>((o) => true);
            this.TeacherFilterCommand.DoExecute = new Action<object>(DoTeacherFilter);

            this.InitCategory();
            this.InitCourseList();
        }

        /// <summary>
        /// 点击教师筛选时
        /// </summary>
        /// <param name="o"></param>
        private void DoTeacherFilter(object o)
        {
            string teacher = o.ToString();
            List<CourseModels> temp = CourseAll;//定义临时列表
            if (teacher != "全部")
            {
                temp = CourseAll.Where(c => c.Teachers.Exists(e => e == teacher)).ToList();
            }
            CourseLists.Clear();

            foreach (var item in temp)
                CourseLists.Add(item);
        }

        /// <summary>
        /// 初始化类别
        /// </summary>
        private void InitCategory()
        {
            //课程分类
            this.CategoryCourses = new ObservableCollection<CategoryItemModel>();
            this.CategoryCourses.Add(new CategoryItemModel("全部", true));
            this.CategoryCourses.Add(new CategoryItemModel("公开课"));
            this.CategoryCourses.Add(new CategoryItemModel("VIP课程"));

            //技术分类
            this.CategoryTechnology = new ObservableCollection<CategoryItemModel>();
            this.CategoryTechnology.Add(new CategoryItemModel("全部", true));
            this.CategoryTechnology.Add(new CategoryItemModel("C#"));
            this.CategoryTechnology.Add(new CategoryItemModel("ASP.NET"));
            this.CategoryTechnology.Add(new CategoryItemModel("微服务"));
            this.CategoryTechnology.Add(new CategoryItemModel("Java"));
            this.CategoryTechnology.Add(new CategoryItemModel("Vue"));
            this.CategoryTechnology.Add(new CategoryItemModel("微信小程序"));
            this.CategoryTechnology.Add(new CategoryItemModel("Winform"));
            this.CategoryTechnology.Add(new CategoryItemModel("WPF"));
            this.CategoryTechnology.Add(new CategoryItemModel("上位机"));

            //授课教师
            this.CategoryTeacher = new ObservableCollection<CategoryItemModel>();
            this.CategoryTeacher.Add(new CategoryItemModel("全部", true));
            var TeacherListTemp = GetListInfo<CourseCategory>("sms_member?is_validation=eq.1&select=user_id,real_name");
            if (TeacherListTemp != null && TeacherListTemp.Count > 0)
            {
                foreach (var item in TeacherListTemp)
                    this.CategoryTeacher.Add(new CategoryItemModel(item.real_name));
            }
        }

        /// <summary>
        /// 初始化课程列表
        /// </summary>
        private void InitCourseList()
        {
            //添加骨架屏
            for (int i = 0; i < 10; i++)
            {
                CourseLists.Add(new CourseModels { IsShowSkeleton = true });
            }

            Task.Run(new Action(async () =>
            {
                CourseAll = APIDataAccess.GetCourseModels();//获取数据源
                await Task.Delay(1000);

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    CourseLists.Clear();//移除骨架屏

                    foreach (var item in CourseAll)
                        CourseLists.Add(item);
                }));
            }));
        }
    }
}