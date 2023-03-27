using StudentManagementSystem.Common;
using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using StudentManagementSystem.View;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;

namespace StudentManagementSystem.ViewModel
{
    /// <summary>
    /// 通讯录管理
    /// </summary>
    public class StudentInformationViewModel : ViewModelBase
    {
        #region 属性[Property]
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

        private List<StudentInformation> _studentSeekList;
        /// <summary>
        /// 搜索列表
        /// </summary>
        public List<StudentInformation> StudentSeekList
        {
            get => _studentSeekList;
            set { _studentSeekList = value; DoNotify(); }
        }

        /// <summary>
        /// 民族
        /// </summary>
        public List<string> NationModelList { get; set; } = new List<string>();

        /// <summary>
        /// 政治面貌
        /// </summary>
        public List<string> PoliticalOutlookList { get; set; } = new List<string>();

        private StudentInfoModels _studentInfo = new StudentInfoModels();
        /// <summary>
        /// 学生信息
        /// </summary>
        public StudentInfoModels StudentInfo
        {
            get { return _studentInfo; }
            set { _studentInfo = value; DoNotify(); }
        }
        #endregion

        public StudentInformationViewModel()
        {
            GetStudentsInfo();
            NationModelList = APIDataAccess.GetInstance().GetNationsList();
            PoliticalOutlookList = APIDataAccess.GetInstance().GetVisageList();

        }

        #region 命令[Command]
        /// <summary>
        /// 打开详细信息卡片窗口【ContactsView】
        /// </summary>
        /// <remarks></remarks>
        public CommandBase CmdOpenStudentInfoCard { get; set; } = new CommandBase()
        {
            DoCanExecute = new Func<object, bool>((o => true)),
            DoExecute = new Action<object>(async (o) =>
            {
                StudentCardWindow studentCard = new StudentCardWindow();
                await studentCard.SelectedStudents(o.ToString());
                studentCard.ShowDialog();//打开卡片窗口
            })
        };

        /// <summary>
        /// 打开添加学生窗口【ContactsView】
        /// </summary>
        /// <remarks></remarks>
        public ICommand CmdOpenAddStudentWindow => new RelayCommand(() =>
        {
            AddStudentWindow addData = new AddStudentWindow();
            addData.ShowDialog();
        });

        /// <summary>
        /// 打开搜索窗口【ContactsView】
        /// </summary>
        /// <remarks></remarks>
        public ICommand CmdOpenSearchStudentWindow => new RelayCommand(() =>
        {
            SearchStudentWindow searchStudent = new SearchStudentWindow();
            searchStudent.ShowDialog();
        });

        /// <summary>
        /// 刷新界面按钮点击命令【ContactsView】
        /// </summary>
        public ICommand CmdFlushStudentDate => new RelayCommand(() =>
        {
            GetStudentsInfo();
        });

        /// <summary>
        /// 搜索命令【SearchStudentWindow】
        /// </summary>
        public ICommand CmdSeek => new RelayCommand<object>((obj) =>
        {
            string temp = obj.ToString();
            if (string.IsNullOrEmpty(temp))
            {
                StudentSeekList = StudentList.ToList(); return;
            }
            IEnumerable<StudentInformation> queryHighScores = from score in StudentList
                                                              where score.StudentID.Contains(temp) || score.StudentName.Contains(temp)
                                                              select score;
            StudentSeekList = queryHighScores.ToList();
        });

        /// <summary>
        /// 添加学生命令【AddStudentWindow】
        /// </summary>
        /// <remarks></remarks>
        public ICommand CmdAddStudentDate => new RelayCommand<object>((obj) =>
        {
            if (obj == null) return;

            if (string.IsNullOrEmpty(StudentInfo.name) || !DoValidate.CheckName(StudentInfo.name))
            {
                MessageWindow.ShowWindow("Wrong name, please re-enter ！", "Warning");
                return;
            }
            if (!string.IsNullOrEmpty(StudentInfo.phone) && !DoValidate.CheckCellPhone(StudentInfo.phone))
            {
                MessageWindow.ShowWindow("Error in entering mobile phone number, please re-enter ！", "Warning");
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

            if (StudentList.ToList().Exists(x => x.StudentID == StudentInfo.number))
            {
                MessageWindow.ShowWindow("Current student already exists !");
                return;
            }

            var msg = APIDataAccess.GetInstance().AddStudentDataAsync(StudentInfo);
            if (msg.Result == "Created")
            {
                MessageWindow.ShowWindow("Created successfully！", "Tips", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            GetStudentsInfo();
            (obj as Window).Close();
        }); 
        #endregion

        /// <summary>
        /// 获取学生信息
        /// </summary>
        private async void GetStudentsInfo()
        {
            if (StudentList != null)
            {
                StudentList.Clear();
            }
            StudentList = new ObservableCollection<StudentInformation>(await APIDataAccess.GetInstance().GetStudentList());
        }
    }
}
