using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using StudentManagementSystem.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StudentManagementSystem.Common;
using StudentManagementSystem.Controls;

namespace StudentManagementSystem.View
{
    /// <summary>
    /// StudentCardWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StudentCardWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string[] GetVs = new string[7];

        public int UpdateStudentData { get; set; }

        public int DeleteStudenData { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; OnPropertyChanged(); }
        }

        private StudentInfoModels _studentInfo;
        //学生信息
        public StudentInfoModels StudentInfo
        {
            get { return _studentInfo; }
            set { _studentInfo = value; OnPropertyChanged(); }
        }

        public StudentCardWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            Binding binding = new Binding();
            binding.Source = this;
            binding.Path = new PropertyPath("Age");
            BindingOperations.SetBinding(this.nianling, TextBox.TextProperty, binding);

            Loaded += (s, e) =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(StudentInfo.birthday) && StudentInfo != null)
                    {
                        GetAgeByBirthdate(Convert.ToDateTime(StudentInfo.birthday));
                    }
                }
                catch { }
            };
            NationModelList = new List<string>(LocalDataAccess.GetInstance().GeiNation());
            PoliticalOutlookList = new List<string>(LocalDataAccess.GetInstance().GetPoliticalOutlook());
        }

        /// <summary>
        /// 民族
        /// </summary>
        public List<string> NationModelList { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public List<string> PoliticalOutlookList { get; set; }

        public async Task SelectedStudents(string str)
        {
            //StudentInfo = LocalDataAccess.GetInstance().StudentsDetails(str);//卡片信息
            try
            {
                StudentInfo = await APIDataAccess.GetInstance().StudentDetails(str);
            }
            catch { }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();//窗口拖动
        }

        /// <summary>
        /// 修改按钮
        /// </summary>
        private void AlterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(StudentInfo.number))
            {
                MessageWindow.ShowWindow("数据不存在！", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
                return;
            }
            if (!string.IsNullOrEmpty(StudentInfo.name))
            {
                //姓名 检查是否为汉字 或字母
                if (!DoValidate.CheckName(StudentInfo.name.Trim()))
                {
                    MessageWindow.ShowWindow("姓名应为汉字或英文!", "提示");
                    return;
                }

                if (!string.IsNullOrEmpty(StudentInfo.phone) && !DoValidate.CheckCellPhone(StudentInfo.phone.Trim()))
                {
                    MessageWindow.ShowWindow("手机号不合法!");
                    return;
                }
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

                bool? r = false;
                r = MessageWindow.ShowWindow("保存将会覆盖之前内容，是否继续 !", "更新", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (r != null && r == true)
                {
                    //LocalDataAccess.GetInstance().StudentsAmend(StudentInfo);
                    Task.Run(() => APIDataAccess.GetInstance().ChangeStudentDate(StudentInfo));
                    MessageWindow.ShowWindow("修改成功");
                }
            }
            else
            {
                MessageWindow.ShowWindow("姓名字段不能为空 ！");
            }
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        private void DelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(StudentInfo.number))
            {
                MessageWindow.ShowWindow("数据不存在！", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
                return;
            }
            bool? r = false;
            r = MessageWindow.ShowWindow("删除后就不能还原了哦，是否继续", "删除", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (r != null && r == true)
            {
                DeleteStudenData = (LocalDataAccess.GetInstance().StudentsDelete(StudentInfo.number));
                MessageWindow.ShowWindow("删除成功,请刷新数据库。");
                this.Close();
            }

        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Gets the age by birthdate.
        /// </summary>
        /// <param name="birthdate">The birthdate.</param>
        /// <returns>An int.</returns>
        /// <remarks>计算年龄</remarks>
        public int GetAgeByBirthdate(DateTime birthdate)
        {
            DateTime now = DateTime.Now;
            Age = now.Year - birthdate.Year;
            if (now.Month < birthdate.Month || (now.Month == birthdate.Month && now.Day < birthdate.Day))
            {
                Age--;
            }
            return Age < 0 ? 0 : Age;
        }

        private void BirthdayDatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            GetAgeByBirthdate((DateTime)((System.Windows.Controls.DatePicker)sender).DisplayDate);
        }

        //更换头像
        //private void ImgBorder_MouseUp(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFile = new OpenFileDialog();
        //    openFile.Filter = "JPEG图片|*.jpg|PNG图片|*png|BMP图片|.bmp";
        //    if (openFile.ShowDialog() == true)
        //    {
        //        string pic = openFile.FileName;
        //        image.Source = new BitmapImage(new Uri(pic));
        //    }
        //}
    }
}
