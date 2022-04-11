﻿using StudentManagementSystem.Common;
using StudentManagementSystem.DataAccess;
using StudentManagementSystem.ViewModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace StudentManagementSystem.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private string parameters;
        public MainView()
        {
            InitializeComponent();

            MainViewModel model = new MainViewModel/*₆*/();
            this.DataContext = model;
            model.UserInfo.Avatar = GlobalValues.UserInfo.Avatar;/*需要对其进行初始化，在₆构造函数里面进行初始化*/
            model.UserInfo.UserName = GlobalValues.UserInfo.RealName;
            model.UserInfo.Gender = GlobalValues.UserInfo.Gender;

            #region 学习笔记 
            //SystemParameters: 可用来查询系统设置的属性
            //SystemParameters.PrimaryScreenHeight 属性: 获取一个值，该值指示主监视器的屏幕高度（以像素为单位）[屏幕的高度]
            #endregion
            this.MaxHeight = SystemParameters.PrimaryScreenHeight;/*为了防止₄最大化时系统任务栏被遮盖*/
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) { this.DragMove(); }//登录窗口拖动
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;//窗口最小化
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            #region 学习笔记 
            /* WindowState 枚举: 指定是最小化、最大化还是还原窗口。 由 WindowState 属性使用。
               Maximized 最大化窗口。 
               Minimized 最小化窗口。 
               Normal 还原窗口。 
            */
            #endregion
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;//₄窗口最大化(需要判断,点一下默认状态,在点一下放大)
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static bool search;
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            #region 学习笔记
            //Key 枚举: 指定键盘上可能的键值
            #endregion
            if (e.Key == Key.Enter)
            {
                switch (parameters)
                {
                    case "FirstPageView":
                        {
                            MessageBox.Show(parameters);
                            break;
                        }
                    case "TextbookView":
                        {
                            MessageBox.Show(parameters);
                            break;
                        }
                    case "CoursePageView":
                        {
                            MessageBox.Show(parameters);
                            break;
                        }
                    case "ContactsView":
                        {
                            if (Search.Text != "")
                            {
                                LocalDataAccess.GetInstance().Search(Search.Text);
                                search = true;
                            }
                            break;
                        }
                    default:
                        search = false;
                        break;
                }
            }
        }


        #region 学习笔记
        /* ButtonBase.CommandParameter 属性：获取或设置要传递给 Command 属性的参数。*/
        #endregion
        private void FirstPageRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            parameters = ((ButtonBase)sender).CommandParameter.ToString();
        }

        private void TextbookRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            parameters = ((ButtonBase)sender).CommandParameter.ToString();
        }

        private void CoursePageRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            parameters = ((ButtonBase)sender).CommandParameter.ToString();
        }

        private void ContactsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            parameters = ((ButtonBase)sender).CommandParameter.ToString();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            switch (parameters)
            {
                case "FirstPageView":
                    {
                        MessageBox.Show(parameters);
                        break;
                    }
                case "TextbookView":
                    {
                        MessageBox.Show(parameters);
                        break;
                    }
                case "CoursePageView":
                    {
                        MessageBox.Show(parameters);
                        break;
                    }
                case "ContactsView":
                    {
                        if (Search.Text != "")
                        {
                            LocalDataAccess.GetInstance().Search(Search.Text);
                            search = true;
                            StudentInformationViewModel stu = new StudentInformationViewModel();
                            stu.SearchStudents();
                        }
                        break;
                    }
                default:
                    search = false;
                    break;
            }
        }
    }
}
