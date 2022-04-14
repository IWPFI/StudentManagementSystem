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
    public class SearchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void DoNotify([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SearchViewModel()
        {

        }

        public ObservableCollection<StudentInformation> StudentList
        {
            get => studentList; set
            {
                studentList = value;
                DoNotify();
            }
        }

        private CommandBase _closeCommand;
        /// <summary>
        /// Gets the close command.
        /// </summary>
        /// <remarks>关闭命令</remarks>
        public CommandBase CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new CommandBase();
                    _closeCommand.DoExecute = new Action<object>(obj =>
                    {
                        (obj as System.Windows.Window).DialogResult = false;
                    });
                    _closeCommand.DoCanExecute = new Func<object, bool>((o) =>
                    {
                        return true;
                    });
                }
                return _closeCommand;
            }
        }
        private CommandBase _seekCommand;
        private ObservableCollection<StudentInformation> studentList;

        public CommandBase SeekCommand
        {
            get
            {
                if (_seekCommand == null)
                {
                    _seekCommand = new CommandBase();
                    _seekCommand.DoExecute = new Action<object>(obj =>
                    {
                        //LocalDataAccess.GetInstance().SearchStudents(obj.ToString());
                        StudentList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().SearchStudents(obj.ToString()));
                        //MessageBox.Show(obj.ToString());
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
