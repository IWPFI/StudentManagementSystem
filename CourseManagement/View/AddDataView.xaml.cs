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

            //this.addSex.SetBinding(TextBox.TextProperty, new Binding("RadioButtonText")
            //{
            //    Source = data = new DataTransmission(),
            //    Mode = BindingMode.TwoWay,
            //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            //});
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

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
                        Tianjia();
                    }
                    else
                    {
                        MessageBox.Show("学生姓名不能为空！");
                    }
                }
                else
                {
                    MessageBox.Show("学生学号不能为空！");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("学号字段必须为数字！");
            }
        }

        public void Tianjia()
        {
            LocalDataAccess.GetInstance().Tianjia(GetAdd);
        }
        /// <summary>
        /// 添加操作
        /// </summary>
        public ObservableCollection<StudentInformation> AddStudenList { get; set; 
        }

        private void AddStudentButtonClick(object sender, RoutedEventArgs e)
        {
            GetStudentData();
            AddStudenList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().AddStudents());
            Close();
        }

        private void WindowsMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();//窗口拖动
        }

        //private void TextChangeRadioButton_Checked(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("cs");
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Binding binding = new Binding();
            //binding.Source = data;
            //binding.Mode = BindingMode.TwoWay;
            //binding.Path = new PropertyPath("RadioButtonText");
            //binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            //BindingOperations.SetBinding(this.addSex, TextBox.TextProperty, binding);
        }

        private void AddSex_MouseEnter(object sender, MouseEventArgs e)
        {
            addSex.ToolTip = @"性别字段只支持输入整数或不输入
                0：未知
                1：男
                2：女";
        }

        private void AddSex_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)//AddSexTextBox失去焦点时，用来判断用户输入是否为指定数字
        {
            if (!string.IsNullOrEmpty(addSex.Text))
            {
                switch (addSex.Text)
                {
                    case "0":
                    case "1":
                    case "2":
                        break;
                    default:
                        MessageBox.Show("请输入指定数字！");
                        addSex.Clear();
                        break;
                }
            }
        }

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            char[] delimiterChars = { ' ', ':' };

            string text = sender.ToString();
            //MessageBox.Show($"Original text: '{text}'");

            string[] words = text.Split(delimiterChars);
            //MessageBox.Show($"{words.Length} words in text:");

            //MessageBox.Show(words[2]);
            data.RadioButtonText = words[2];
            Judge(words[2]);
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
    }
}
