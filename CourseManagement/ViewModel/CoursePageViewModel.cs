using StudentManagementSystem.Common;
using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace StudentManagementSystem.ViewModel
{
    public class CoursePageViewModel
    {
        public ObservableCollection<CategoryItemModel> CategoryCourses { get; set; }//绑定到界面课程分类
        public ObservableCollection<CategoryItemModel> CategoryTechnology { get; set; }//绑定到界面技术分类
        public ObservableCollection<CategoryItemModel> CategoryTeacher { get; set; }//绑定到界面授课老师

        public ObservableCollection<CourseModel> CourseList { get; set; } = new ObservableCollection<CourseModel>();
        private List<CourseModel> courseAll;//对授课老师进行筛选，点击相应的老师对应相应的课程

        public CommandBase OpenCourseUrlCommand { get; set; }//点击课程名称时跳转到网页₁₀
        public CommandBase TeacherFilterCommand { get; set; }

        public CoursePageViewModel()
        {
            this.OpenCourseUrlCommand = new CommandBase();//₁₀实例化
            this.OpenCourseUrlCommand.DoCanExecute = new Func<object, bool>((o) => true);//₁₀
            this.OpenCourseUrlCommand.DoExecute = new Action<object>((o) => { System.Diagnostics.Process.Start("explorer.exe", o.ToString()); });//₁₀可以打开界面了

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
            CourseList.Clear();//清空₁₁

            foreach (var item in temp)
                CourseList.Add(item);/*₁₁在重新遍历之前需要清空之前的数据*/
        }


        private void InitCategory()//初始化课程列表
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
            CourseList = new ObservableCollection<CourseModel>(LocalDataAccess.GetInstance().GetCourses());
            //CourseList = new ObservableCollection<CourseModel>(courseAll);

            //for (int i = 0; i < 10; i++)
            //{
            //    CourseList.Add(new CourseModel { IsShowSkeleton = true });
            //}
            //Task.Run(new Action(async () =>
            //{
            courseAll = LocalDataAccess.GetInstance().GetCourses();
            //    await Task.Delay(4000);

            //    Application.Current.Dispatcher.Invoke(new Action(() =>
            //    {
            //        CourseList.Clear();
            //        foreach (var item in courseAll)
            //            CourseList.Add(item);
            //    }));
            //}));
        }
    }
}