using StudentManagementSystem.Common;
using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using StudentManagementSystem.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StudentManagementSystem.ViewModel
{
    public class AddStudentViewModel : ViewModelBase
    {
        public AddStudentViewModel()
        {
            StudentInfo = new StudentInformation();
            NationModelList = new ObservableCollection<string>(LocalDataAccess.GetInstance().GeiNation());
            PoliticalOutlookList = new ObservableCollection<string>(LocalDataAccess.GetInstance().GetPoliticalOutlook());
        }

        /// <summary>
        /// 民族
        /// </summary>
        public ObservableCollection<string> NationModelList { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public ObservableCollection<string> PoliticalOutlookList { get; set; }

        private StudentInformation _studentInfo;
        /// <summary>
        /// 学生信息
        /// </summary>
        public StudentInformation StudentInfo
        {
            get { return _studentInfo; }
            set { _studentInfo = value; DoNotify(); }
        }

        private CommandBase _addCommand;
        /// <summary>
        /// 添加命令
        /// </summary>
        public CommandBase AddCommand
        {
            get
            {
                _addCommand = new CommandBase();
                _addCommand.DoCanExecute = new Func<object, bool>((obj) => true);
                _addCommand.DoExecute = new Action<object>((obj) =>
                {
                    if (obj == null) return;

                    if (string.IsNullOrEmpty(StudentInfo.StudentName) || !DoValidate.CheckName(StudentInfo.StudentName))
                    {
                        MessageWindow.ShowWindow("输入姓名有误，请重新输入！", "警告");
                        return;
                    }
                    if (!string.IsNullOrEmpty(StudentInfo.StudentPhone) && !DoValidate.CheckCellPhone(StudentInfo.StudentPhone))
                    {
                        MessageWindow.ShowWindow("输入手机号有误，请重新输入！", "警告");
                        return;
                    }

                    if (StudentInfo.StudentBirthday == null) StudentInfo.StudentBirthday = DateTime.Now;
                    if (StudentInfo.NationsName == null) StudentInfo.NationsName = "1";
                    if (StudentInfo.PoliticsStatus == null) StudentInfo.PoliticsStatus = "1";

                    if (LocalDataAccess.GetInstance().AddStudents(StudentInfo) >= 0)
                        MessageWindow.ShowWindow("添加成功，请手动刷新界面！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);

                    (obj as Window).Close();

                });
                return _addCommand;
            }
        }

    }
}
