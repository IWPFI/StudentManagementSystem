﻿using StudentManagementSystem.DataAccess;
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
        public event PropertyChangedEventHandler? PropertyChanged;
        public void DoNotify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        ContactsView contacts = new ContactsView();
        private string[] GetVs = new string[7];
        public int UpdateStudentData { get; set; }
        public int DeleteStudenData { get; set; }

        private int age;
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                DoNotify();
            }
        }

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
                    Content();
                    DeleteStudenData = (LocalDataAccess.GetInstance().StudentsDelete(GetVs));
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

        //获取数据
        private void Content()
        {
            GetVs[0] = xuehao.Text;
            GetVs[1] = xingming.Text;
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

        /// <summary>
        /// Gets the age by birthdate.
        /// </summary>
        /// <param name="birthdate">The birthdate.</param>
        /// <returns>An int.</returns>
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
