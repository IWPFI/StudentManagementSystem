using StudentManagementSystem.Common;
using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using StudentManagementSystem.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentManagementSystem.ViewModel
{
    public class StudentInformationViewModel : ViewModelBase
    {
        public CommandBase OpenStudentId { get; set; }//点击Id时执行

        public ObservableCollection<StudentInformation> StudentList { get; set; }

        public StudentInformationViewModel()
        {
            GetSearchStudents();

            OpenStudentId = new CommandBase();
            OpenStudentId.DoCanExecute = new Func<object, bool>((o) => true);
            OpenStudentId.DoExecute = new Action<object>((o) =>
            {
                StudentCardWindow studentCard = new StudentCardWindow();
                studentCard.SelectedStudents(o.ToString());
                studentCard.ShowDialog();//打开卡片窗口
            });
        }

        /// <summary>
        /// 获取学生列表
        /// </summary>
        public void GetSearchStudents()
        {
            if (StudentList != null)
            {
                StudentList.Clear();
            }
            StudentList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().GetStudents());
        }
    }
}
