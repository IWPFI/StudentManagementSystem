using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace StudentManagementSystem.View
{
    /// <summary>
    /// AddDataView.xaml 的交互逻辑
    /// </summary>
    public partial class AddDataView : Window
    {
        private readonly string[] GetAdd = new string[7];
        private string setRadioButton;
        DataTransmission data;


        public AddDataView()
        {
            InitializeComponent();
            this.DataContext = data = new DataTransmission();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 获取学生数据
        /// </summary>
        private void GetStudentData()
        {
            try
            {
                if (!string.IsNullOrEmpty(addId.Text))
                {
                    if (!string.IsNullOrEmpty(addName.Text))
                    {
                        GetAdd[0] = addId.Text;
                        GetAdd[1] = addName.Text;
                        GetAdd[2] = setRadioButton;
                        GetAdd[4] = addNumber.Text;
                        GetAdd[5] = addClass.Text;
                        GetAdd[6] = addAddress.Text;
                        if (addBirthday.Text == "")
                        {
                            GetAdd[3] = DateTime.Now.ToString("yyyy-MM-dd");//获取系统当前时间，使用yyyyMMdd 格式作为字符串展示
                        }
                        else
                        {
                            GetAdd[3] = addBirthday.Text.Split(new char[] { ' ' })[0]; //Split(new char[] { ' ' })[0]:截取让DateTime的值为"2011/12/9",即去掉空格以及后面的字符
                        }
                        AddStudentInformation();
                    }
                    else
                    {
                        throw new Exception("姓名字段不能为空！");
                    }
                }
                else
                {
                    throw new Exception("学号不能为空");
                }
            }
            catch (Exception exp)
            {
                MessageWindow.ShowWindow(exp.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void AddStudentInformation()
        {
            LocalDataAccess.GetInstance().Tianjia(GetAdd);
            AddStudenList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().AddStudents());
            Close();
        }

        /// <summary>
        /// 添加操作
        /// </summary>
        public ObservableCollection<StudentInformation> AddStudenList { get; set; 
        }

        private void AddStudentButtonClick(object sender, RoutedEventArgs e)
        {
            GetStudentData();
        }

        private void WindowsMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();//窗口拖动
        }

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            //char[] delimiterChars = { ' ', ':' };
            //string text = sender.ToString();
            //string[] words = text.Split(delimiterChars);
            data.RadioButtonText = (string)(sender as RadioButton).Content;
            Judge((string)(sender as RadioButton).Content);
        }

        private void Judge(string a)
        {
            switch (a)
            {
                case "男":
                    {
                        setRadioButton = "1";
                        break;
                    }
                case "女":
                    {
                        setRadioButton = "2";
                        break;
                    }
                case "未知":
                    {
                        setRadioButton = "0";
                        break;
                    }
                default:
                    {
                        setRadioButton = "0";
                        break;
                    }
            }
        }

        private void SexExpanderLostFocus(object sender, RoutedEventArgs e)
        {
            sexRadio.IsExpanded = false;
        }

        private void SexExpanderLostStylusCapture(object sender, StylusEventArgs e)
        {
            sexRadio.IsExpanded = false;
        }

        private void GetNation()
        {
            throw new NotImplementedException();
        }
    }
}
