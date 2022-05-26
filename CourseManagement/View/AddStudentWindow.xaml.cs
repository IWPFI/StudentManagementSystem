using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace StudentManagementSystem.View
{
    /// <summary>
    /// AddDataView.xaml 的交互逻辑
    /// </summary>
    public partial class AddStudentWindow : Window, INotifyPropertyChanged
    {
        private readonly string[] GetAdd = new string[9];
        private string setRadioButton;
        DataTransmission data;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddStudentWindow()
        {
            InitializeComponent();
            this.DataContext = data = new DataTransmission();
        }

        /// <summary>
        /// Closes the button click.
        /// </summary>
        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 获取学生数据
        /// </summary>
        private void GetStudentData()
        {
            //try
            //{
            if (!string.IsNullOrEmpty(addId.Text))
            {
                if (!string.IsNullOrEmpty(addName.Text))
                {
                    GetAdd[0] = addId.Text;
                    GetAdd[1] = addName.Text;
                    if (string.IsNullOrEmpty(setRadioButton))
                    {
                        GetAdd[2] = "0";
                    }
                    else
                    {
                        GetAdd[2] = setRadioButton;
                    }

                    if (addBirthday.Text == "")
                    {
                        GetAdd[3] = DateTime.Now.ToString("yyyy-MM-dd");//获取系统当前时间，使用yyyyMMdd 格式作为字符串展示
                    }
                    else
                    {
                        GetAdd[3] = addBirthday.Text.Split(new char[] { ' ' })[0]; //Split(new char[] { ' ' })[0]:截取让DateTime的值为"2011/12/9",即去掉空格以及后面的字符
                    }

                    GetAdd[4] = addNumber.Text;
                    GetAdd[5] = addClass.Text;
                    GetAdd[6] = addAddress.Text;
                    if (string.IsNullOrEmpty(nationComboBox.Text))
                    {
                        GetAdd[7] = "1";
                    }
                    else
                    {
                        GetAdd[7] = (nationComboBox.SelectedIndex + 1).ToString();
                    }
                    if (string.IsNullOrEmpty(politicalOutlookComboBox.Text))
                    {
                        GetAdd[8] = "1";
                    }
                    else
                    {
                        GetAdd[8] = (politicalOutlookComboBox.SelectedIndex + 1).ToString();
                    }
                    AddStudentInformation();
                }
                else
                {
                    MessageWindow.ShowWindow("姓名字段不能为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //throw new Exception("姓名字段不能为空！");
                }
            }
            else
            {
                MessageWindow.ShowWindow("学号字段不能为空！", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                //throw new Exception("学号不能为空");
            }
            //}
            //catch (Exception exp)
            //{
            //    MessageWindow.ShowWindow(exp.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        public void AddStudentInformation()
        {
            LocalDataAccess.GetInstance().AddStudents(GetAdd);
            Close();
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        private void AddStudentButtonClick(object sender, RoutedEventArgs e)
        {
            GetStudentData();
        }

        /// <summary>
        /// 窗口拖动
        /// </summary>
        private void WindowsMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            data.RadioButtonText = (string)(sender as RadioButton).Content;
            switch ((sender as RadioButton).Content)
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
                default:
                    {
                        setRadioButton = "0";
                        break;
                    }
            }
        }

        private void SexExpanderLostStylusCapture(object sender, StylusEventArgs e)
        {
            sexRadio.IsExpanded = false;
        }

        private void Border_LostFocus(object sender, RoutedEventArgs e)
        {
            sexRadio.IsExpanded = false;
        }
    }
}
