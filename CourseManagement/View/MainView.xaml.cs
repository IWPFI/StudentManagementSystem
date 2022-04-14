using StudentManagementSystem.Common;
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
        public MainView()
        {
            InitializeComponent();

            MainViewModel model = new MainViewModel();
            this.DataContext = model;

            model.UserInfo.Avatar = GlobalValues.UserInfo.Avatar;/*It needs to be initialized,Initialize in ₆ constructor*/
            model.UserInfo.UserName = GlobalValues.UserInfo.RealName;
            model.UserInfo.Gender = GlobalValues.UserInfo.Gender;

            #region Notes 
            //SystemParameters: 可用来查询系统设置的属性
            //SystemParameters.PrimaryScreenHeight 属性: 获取一个值，该值指示主监视器的屏幕高度（以像素为单位）[屏幕的高度]
            #endregion
            this.MaxHeight = SystemParameters.PrimaryScreenHeight;/*防止最大化时系统任务栏被遮盖*/
        }

        private string parameters;
        public static bool search;

        /// <summary>
        /// 登录窗口拖动
        /// </summary>
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) { this.DragMove(); }
        }

        /// <summary>
        /// 窗口最小化
        /// </summary>
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 窗口最大化
        /// </summary>
        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            #region Notes 
            /* WindowState 枚举: 指定是最小化、最大化还是还原窗口。 由 WindowState 属性使用。
               Maximized 最大化窗口。 
               Minimized 最小化窗口。 
               Normal 还原窗口。 
            */
            #endregion
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            #region Notes
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
                                //LocalDataAccess.GetInstance().Search(Search.Text);
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        #region Notes
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
                            //LocalDataAccess.GetInstance().Search(Search.Text);
                            //search = true;
                            //StudentInformationViewModel stu = new StudentInformationViewModel();
                            //stu.SearchStudents();
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
