using StudentManagementSystem.Common;
using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using StudentManagementSystem.View;
using System;
using System.Collections.ObjectModel;

namespace StudentManagementSystem.ViewModel
{
    public class StudentInformationViewModel
    {
        public ObservableCollection<StudentInformation> StudentList { get; set; }

        public ObservableCollection<StudentInformation> StudentsDetailsList { get; set; }

        public CommandBase OpenStudentId { get; set; }//点击Id时执行

        public StudentInformationViewModel()
        {
            GetSearchStudents();
            StudentsList();

            OpenStudentId = new CommandBase();
            OpenStudentId.DoCanExecute = new Func<object, bool>((o) => true);
            OpenStudentId.DoExecute = new Action<object>((o) =>
            {
                InitStudentList(o.ToString());
            });
        }

        //搜索学生信息
        public void SearchStudents()
        {
            StudentList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().SearchStudents());
            MainView.search = false;
        }

        //获取学生列表
        public void GetSearchStudents()
        {
            StudentList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().GetStudents());
        }

        public string InitStudentList(string a)
        {
            LocalDataAccess.GetInstance().Chuangdi(a);
            Students students = new Students();
            students.ShowDialog();
            return a;
        }

        private void StudentsList()
        {
            StudentsDetailsList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().StudentsDetails());//卡片信息
        }
    }
}
