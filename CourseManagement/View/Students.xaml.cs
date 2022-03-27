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

namespace StudentManagementSystem.View
{
    /// <summary>
    /// Students.xaml 的交互逻辑
    /// </summary>
    public partial class Students : Window, INotifyPropertyChanged
    {
        public Students()
        {
            InitializeComponent();

            GridDataContext.DataContext = new StudentInformationViewModel().StudentsDetailsList;

            Binding binding = new Binding();
            binding.Source = this;
            binding.Path = new PropertyPath("Age");
            BindingOperations.SetBinding(this.nianling, TextBox.TextProperty, binding);

            Loaded += (s, e) =>
            {
                DateTime dt = Convert.ToDateTime(shengri.Text);
                GetAgeByBirthdate(dt);
            };
        }

        private string[] GetVs = new string[7];

        public event PropertyChangedEventHandler? PropertyChanged;
        public void DoNotify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));//达到通知效果
        }

        private int age;

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                DoNotify();
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public ObservableCollection<StudentInformation> AmendStudenList { get; set; }
        //修改
        public void XiugaiXX()
        {
            LocalDataAccess.GetInstance().Transmit(GetVs);
            AmendStudenList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().StudentsAmend());
        }

        //删除
        public void Shanchu()
        {
            LocalDataAccess.GetInstance().Transmit(GetVs);
            DeleteStudenList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().StudentsDelete());
        }

        //获取数据
        private void Content()
        {
            if (xingming.Text != "")
            {
                GetVs[0] = xingming.Text;
                GetVs[1] = xuehao.Text;
                if (shengri.Text != "")
                {
                    GetVs[3] = shengri.Text.Split(new char[] { ' ' })[0]; //Split(new char[] { ' ' })[0]:截取让DateTime的值为"2011/12/9",即去掉空格以及后面的字符
                }
                else
                {
                    GetVs[3] = DateTime.Now.ToString("yyyy-MM-dd");//获取系统当前时间，使用yyyyMMdd 格式作为字符串展示
                }
                GetVs[2] = xingbie.Text;
                GetVs[4] = bangji.Text;
                GetVs[5] = dianhau.Text;
                GetVs[6] = dizhi.Text;
            }
            else
            {
                MessageBox.Show("学生姓名不能为空！");
            }
        }

        //修改操作
        private void AlterButton_Click(object sender, RoutedEventArgs e)
        {
            bool? r = false;
            r = MessageWindow.ShowWindow("保存将会覆盖之前内容哦，是否继续", "更新", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (r != null && r == true)
            {
                Content();
                XiugaiXX();
                MessageWindow.ShowWindow("修改成功", "修改成功");
                Close();
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();//窗口拖动
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        public ObservableCollection<StudentInformation> DeleteStudenList { get; set; }

        //删除操作
        private void DelectButton_Click(object sender, RoutedEventArgs e)
        {
            bool? r = false;
            r = MessageWindow.ShowWindow("删除后就不能还原了哦，是否继续", "删除", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (r != null && r == true)
            {
                Content();
                Shanchu();
                MessageWindow.ShowWindow("删除成功,请刷新数据库。");
                this.Close();
            }

        }
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
