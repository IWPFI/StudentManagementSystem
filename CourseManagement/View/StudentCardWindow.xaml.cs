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

        ContactsView contacts = new ContactsView();

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

        public ObservableCollection<StudentInformation> StudentsDetailsList { get; set; }


        public StudentCardWindow(string str)
        {
            InitializeComponent();
            StudentsDetailsList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().StudentsDetails1(str));//卡片信息
            this.GridDataContext.DataContext = StudentsDetailsList;
            Binding binding = new Binding();
            binding.Source = this;
            binding.Path = new PropertyPath("Age");
            BindingOperations.SetBinding(this.nianling, TextBox.TextProperty, binding);

            Loaded += (s, e) =>
            {
                if (!string.IsNullOrEmpty(shengri.Text))
                {
                    DateTime dt = Convert.ToDateTime(shengri.Text);
                    GetAgeByBirthdate(dt);
                }
            };
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
            if (string.IsNullOrEmpty(xuehao.Text))
            {
                MessageWindow.ShowWindow("数据不存在！", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
            else
            {
                if (!string.IsNullOrEmpty(xingming.Text))
                {
                    //姓名 检查是否为汉字 或字母
                    if (!DoValidate.CheckName(xingming.Text.Trim()))
                    {
                        MessageWindow.ShowWindow("姓名应为汉字或英文!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(dianhau.Text) && !DoValidate.CheckCellPhone(dianhau.Text.Trim()))
                    {
                        MessageWindow.ShowWindow("手机号不合法!");
                        return;
                    }
                    bool? r = false;
                    r = MessageWindow.ShowWindow("保存将会覆盖之前内容哦，是否继续", "更新", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (r != null && r == true)
                    {
                        Content();
                        LocalDataAccess.GetInstance().StudentsAmend(GetVs);
                        MessageWindow.ShowWindow("修改成功");
                        Close();
                    }
                }
                else
                {
                    MessageWindow.ShowWindow("姓名字段不能为空！");
                }
            }
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        private void DelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(xuehao.Text))
            {
                MessageWindow.ShowWindow("数据不存在！", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
            else
            {
                bool? r = false;
                r = MessageWindow.ShowWindow("删除后就不能还原了哦，是否继续", "删除", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (r != null && r == true)
                {
                    DeleteStudenData = (LocalDataAccess.GetInstance().StudentsDelete(xuehao.Text));
                    MessageWindow.ShowWindow("删除成功,请刷新数据库。");
                    contacts.InterfaceData();
                    this.Close();
                }
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
        /// 获取数据
        /// </summary>
        private void Content()
        {
            //学号
            GetVs[0] = xuehao.Text;

            //姓名
            GetVs[1] = xingming.Text;

            //性别
            GetVs[2] = xingbie.Text;

            //生日
            if (shengri.Text != "")
            {
                GetVs[3] = shengri.Text.Split(new char[] { ' ' })[0]; //Split(new char[] { ' ' })[0]:截取让DateTime的值为"2011/12/9",即去掉空格以及后面的字符
            }
            else
            {
                GetVs[3] = DateTime.Now.ToString("yyyy-MM-dd");//获取系统当前时间，使用yyyyMMdd 格式作为字符串展示
            }

            //班级
            GetVs[4] = bangji.Text;

            //手机号
            GetVs[5] = dianhau.Text;

            //地址
            GetVs[6] = dizhi.Text;
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

        private CommandBase _popupCommand;

        public CommandBase PopupCommand
        {
            get
            {
                if (_popupCommand == null)
                {
                    _popupCommand = new CommandBase();
                    _popupCommand.DoExecute = new Action<object>(obj =>
                    {
                        if (obj.ToString() == "1")
                        {
                            popup.IsOpen = false;
                            popup.IsOpen = true;
                        }
                        else
                        {
                            popup1.IsOpen = false;
                            popup1.IsOpen = true;
                        }
                    });
                    _popupCommand.DoCanExecute = new Func<object, bool>((o) => true);
                }
                return _popupCommand;
            }
        }

        private void NationsButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            popup.IsOpen = true;
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
