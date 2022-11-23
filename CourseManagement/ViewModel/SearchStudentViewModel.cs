using StudentManagementSystem.Common;
using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentManagementSystem.ViewModel
{
    public class SearchStudentViewModel : ViewModelBase
    {
        public SearchStudentViewModel()
        {

        }

        private ObservableCollection<StudentInformation> studentList;

        public ObservableCollection<StudentInformation> StudentList
        {
            get => studentList; set
            {
                studentList = value;
                DoNotify();
            }
        }

        private CommandBase _seekCommand;
        /// <summary>
        /// 搜索命令
        /// </summary>
        public CommandBase SeekCommand
        {
            get
            {
                if (_seekCommand == null)
                {
                    _seekCommand = new CommandBase();
                    _seekCommand.DoExecute = new Action<object>(obj =>
                    {
                        StudentList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().SearchStudents(obj.ToString()));
                    });
                    _seekCommand.DoCanExecute = new Func<object, bool>((o) =>
                    {
                        return true;
                    });
                }
                return _seekCommand;
            }
        }
    }
}
