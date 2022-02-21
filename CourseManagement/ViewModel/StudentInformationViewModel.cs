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

        //public ObservableCollection<StudentInformation> SearchStudentsList { get; set; }

        //public ObservableCollection<StudentInformation> AmendStudenList { get; set; }
        //public ObservableCollection<StudentInformation> DeleteStudenList { get; set; }
        //public ObservableCollection<StudentInformation> AddStudenList { get; set; }

        public CommandBase OpenStudentId { get; set; }//点击Id时执行

        public StudentInformationViewModel()
        {
            //StudentList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().GetStudents());

            StudentsDetailsList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().StudentsDetails());//卡片信息


            //if (MainView.search)
            //{
            //    StudentList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().SearchStudents());
            //    MainView.search = false;
            //}
            //else
            //{
            //    StudentList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().GetStudents());
            //}

            if (MainView.search)
            {
                SearchStudents();
            }
            else
            {
                NoSearchStudents();
            }


            OpenStudentId = new CommandBase();
            OpenStudentId.DoCanExecute = new Func<object, bool>((o) => true);
            OpenStudentId.DoExecute = new Action<object>((o) =>
            {
                InitStudentList(o.ToString());
            });
        }

        public void SearchStudents()
        {
            StudentList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().SearchStudents());
            MainView.search = false;
        }
        public void NoSearchStudents()
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
    }
}
