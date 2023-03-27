using StudentManagementSystem.Common;
using StudentManagementSystem.ViewModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace StudentManagementSystem.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            #region Notes 
            //SystemParameters: 可用来查询系统设置的属性
            //SystemParameters.PrimaryScreenHeight 属性: 获取一个值，该值指示主监视器的屏幕高度（以像素为单位）[屏幕的高度]
            #endregion
            this.MaxHeight = SystemParameters.PrimaryScreenHeight;/*防止最大化时系统任务栏被遮盖*/
        }

        private string parameters;
        public static bool search;

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
