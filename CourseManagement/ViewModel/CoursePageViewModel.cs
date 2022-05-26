using StudentManagementSystem.Common;
using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StudentManagementSystem.ViewModel
{
    public class CoursePageViewModel
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

        public ObservableCollection<CourseModel> CourseList { get; set; } = new ObservableCollection<CourseModel>();

        /// <summary>
        /// 对授课老师进行筛选，点击相应的老师对应相应的课程
        /// </summary>
        private List<CourseModel> courseAll;

        /// <summary>
        /// 点击课程名称时跳转到网页
        /// </summary>
        public CommandBase OpenCourseUrlCommand { get; set; }

        public CommandBase TeacherFilterCommand { get; set; }

        public CoursePageViewModel()
        {
            this.OpenCourseUrlCommand = new CommandBase();
            this.OpenCourseUrlCommand.DoCanExecute = new Func<object, bool>((o) => true);
            this.OpenCourseUrlCommand.DoExecute = new Action<object>((o) => { System.Diagnostics.Process.Start("explorer.exe", o.ToString()); });//调用浏览器打开url

            this.TeacherFilterCommand = new CommandBase();
            this.TeacherFilterCommand.DoCanExecute = new Func<object, bool>((o) => true);
            this.TeacherFilterCommand.DoExecute = new Action<object>(DoFilter);

            this.InitCategory();

            this.InitCourseList();
        }

        private void DoFilter(object o)
        {
            string teacher = o.ToString();
            List<CourseModel> temp = courseAll;
            if (teacher != "全部")
            {
                temp = courseAll.Where(c => c.Teachers.Exists(e => e == teacher)).ToList();
            }
            CourseList.Clear();//₁₁Empty data

            foreach (var item in temp)
                CourseList.Add(item);/*₁₁You need to clear the previous data before re traversing*/
        }

        /// <summary>
        /// 初始化课程列表
        /// </summary>
        private void InitCategory()
        {
            this.CategoryCourses = new ObservableCollection<CategoryItemModel>();
            this.CategoryCourses.Add(new CategoryItemModel("全部", true));
            this.CategoryCourses.Add(new CategoryItemModel("公开课"));
            this.CategoryCourses.Add(new CategoryItemModel("VIP课程"));

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

            this.CategoryTeacher = new ObservableCollection<CategoryItemModel>();
            this.CategoryTeacher.Add(new CategoryItemModel("全部", true));
            foreach (var item in LocalDataAccess.GetInstance().GetTeachers())
                this.CategoryTeacher.Add(new CategoryItemModel(item));
        }


        private void InitCourseList()
        {
            //CourseList = new ObservableCollection<CourseModel>(LocalDataAccess.GetInstance().GetCourses());
            //CourseList = new ObservableCollection<CourseModel>(courseAll);

            for (int i = 0; i < 10; i++)
            {
                CourseList.Add(new CourseModel { IsShowSkeleton = true });
            }
            Task.Run(new Action(async () =>
            {
                courseAll = LocalDataAccess.GetInstance().GetCourses();
                await Task.Delay(4000);

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    CourseList.Clear();
                    foreach (var item in courseAll)
                        CourseList.Add(item);
                }));
            }));
        }
    }
}