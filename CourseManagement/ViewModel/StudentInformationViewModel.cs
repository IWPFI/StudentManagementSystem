﻿using StudentManagementSystem.Common;
using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using StudentManagementSystem.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentManagementSystem.ViewModel
{
    public class StudentInformationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void DoNotify([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CommandBase OpenStudentId { get; set; }//点击Id时执行
        private ObservableCollection<StudentInformation> studentList;
        private ObservableCollection<StudentInformation> studentsDetailsList;

        public ObservableCollection<StudentInformation> StudentList
        {
            get => studentList;
            set
            {
                studentList = value;
                DoNotify();
            }
        }

        public ObservableCollection<StudentInformation> StudentsDetailsList
        {
            get => studentsDetailsList; set
            {
                studentsDetailsList = value;
                DoNotify();
            }
        }

        public StudentInformationViewModel()
        {
            GetSearchStudents();
            CardList();

            OpenStudentId = new CommandBase();
            OpenStudentId.DoCanExecute = new Func<object, bool>((o) => true);
            OpenStudentId.DoExecute = new Action<object>((o) =>
            {
                InitStudentList(o.ToString());
            });
        }

        /// <summary>
        /// 获取学生列表
        /// </summary>
        public void GetSearchStudents()
        {
            if (StudentList != null)
            {
                StudentList.Clear();
            }
            StudentList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().GetStudents());
        }

        public string InitStudentList(string a)
        {
            LocalDataAccess.GetInstance().Chuangdi(a);
            Students students = new Students();
            students.ShowDialog();
            return a;
        }

        /// <summary>
        /// 卡片信息
        /// </summary>
        private void CardList()
        {
            StudentsDetailsList = new ObservableCollection<StudentInformation>(LocalDataAccess.GetInstance().StudentsDetails());//卡片信息
        }
    }
}
