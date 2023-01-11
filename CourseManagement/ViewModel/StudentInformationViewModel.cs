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
    /// <summary>
    /// 学生信息
    /// </summary>
    public class StudentInformationViewModel : ViewModelBase
    {
        /// <summary>
        /// 学生列表
        /// </summary>
        private ObservableCollection<StudentInformation> _studentList;
        public ObservableCollection<StudentInformation> StudentList
        {
            get => _studentList;
            set
            {
                _studentList = value;
                DoNotify();
            }
        }

        private ObservableCollection<StudentInformation> _studentSeekList;
        /// <summary>
        /// 搜索列表
        /// </summary>
        public ObservableCollection<StudentInformation> StudentSeekList
        {
            get => _studentSeekList;
            set { _studentSeekList = value; DoNotify(); }
        }

        /// <summary>
        /// 民族
        /// </summary>
        public ObservableCollection<string> NationModelList { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// 政治面貌
        /// </summary>
        public ObservableCollection<string> PoliticalOutlookList { get; set; } = new ObservableCollection<string>();

        private StudentInfoModels _studentInfo = new StudentInfoModels();
        /// <summary>
        /// 学生信息
        /// </summary>
        public StudentInfoModels StudentInfo
        {
            get { return _studentInfo; }
            set { _studentInfo = value; DoNotify(); }
        }

        public StudentInformationViewModel()
        {
            GetStudentsInfo();
            NationModelList = new ObservableCollection<string>(LocalDataAccess.GetInstance().GeiNation());
            PoliticalOutlookList = new ObservableCollection<string>(LocalDataAccess.GetInstance().GetPoliticalOutlook());

        }

        /// <summary>
        /// Get Students Info
        /// </summary>
        /// <remarks>获取学生信息</remarks>
        public async void GetStudentsInfo()
        {
            if (StudentList != null)
            {
                StudentList.Clear();
            }
            //StudentList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().GetStudents());
            StudentList = new ObservableCollection<StudentInformation> (await APIDataAccess.GetInstance().GetStudentList());
        }

        /// <summary>
        /// Click the student ID to open the details
        /// </summary>
        /// <remarks></remarks>
        public CommandBase OpenStudentId { get; set; } = new CommandBase()
        {
            DoCanExecute = new Func<object, bool>((o => true)),
            DoExecute = new Action<object>((o) =>
            {
                StudentCardWindow studentCard = new StudentCardWindow();
                studentCard.SelectedStudents(o.ToString());
                studentCard.ShowDialog();//打开卡片窗口
            })
        };

        /// <summary>
        /// Add Button Click Command
        /// </summary>
        /// <remarks></remarks>
        public ICommand CmdAddStudentButtonClick => new RelayCommand(() =>
        {
            AddStudentWindow addData = new AddStudentWindow();
            addData.ShowDialog();
        });

        /// <summary>
        /// Search Button Click Command
        /// </summary>
        /// <remarks></remarks>
        public ICommand CmdSearchStudentButtonClick => new RelayCommand(() =>
        {
            SearchStudentWindow searchStudent = new SearchStudentWindow();
            searchStudent.ShowDialog();
        });

        /// <summary>
        /// Refresh interface button Click command
        /// </summary>
        /// <remarks>刷新界面按钮点击命令</remarks>
        public ICommand CmdFlushStudentButtonClick => new RelayCommand(() =>
        {
            GetStudentsInfo();
        });

        private CommandBase _seekCommand;
        /// <summary>
        /// Search Student Information Command
        /// </summary>
        /// <remarks></remarks>
        public CommandBase SeekCommand
        {
            get
            {
                if (_seekCommand == null)
                {
                    _seekCommand = new CommandBase();
                    _seekCommand.DoExecute = new Action<object>(obj =>
                    {
                        //Null检查 | 异常的时候，就会出现：System.ArgumentNullException: 'Value cannot be null. (Parameter 'str')'
                        //ArgumentNullException.ThrowIfNull(obj);
                        StudentSeekList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().SearchStudents(obj.ToString()));
                    });
                    _seekCommand.DoCanExecute = new Func<object, bool>((o) =>
                    {
                        return true;
                    });
                }
                return _seekCommand;
            }
        }

        /// <summary>
        /// Add Student Button Click Command
        /// </summary>
        /// <remarks></remarks>
        public ICommand AddStudentBtnClickCommand => new RelayCommand<object>((obj) =>
        {
            if (obj == null) return;

            if (string.IsNullOrEmpty(StudentInfo.name) || !DoValidate.CheckName(StudentInfo.name))
            {
                MessageWindow.ShowWindow("输入姓名有误，请重新输入！", "警告");
                return;
            }
            if (!string.IsNullOrEmpty(StudentInfo.phone) && !DoValidate.CheckCellPhone(StudentInfo.phone))
            {
                MessageWindow.ShowWindow("输入手机号有误，请重新输入！", "警告");
                return;
            }

            if (StudentInfo.birthday == null) StudentInfo.birthday = DateTime.Now.ToString();
            if (StudentInfo.nation == null) StudentInfo.nation_id = 1;
            else
            {
                StudentInfo.nation_id = NationModelList.ToList().FindIndex(item => item.Equals(StudentInfo.nation)) + 1;
            }
            if (StudentInfo.politics == null) StudentInfo.politics_status_id = 1;
            else
            {
                StudentInfo.politics_status_id = PoliticalOutlookList.ToList().FindIndex(item => item.Equals(StudentInfo.politics)) + 1;
            }

            var msg = APIDataAccess.GetInstance().AddStudentDataAsync(StudentInfo);
            if(msg.Result== "Created")
            {

            MessageWindow.ShowWindow("创建成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            GetStudentsInfo();
            //(obj as Window).Close();
        });
    }
}
