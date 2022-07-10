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
using StudentManagementSystem.Common;

namespace StudentManagementSystem.View
{
    /// <summary>
    /// AddDataView.xaml 的交互逻辑
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        public AddStudentWindow()
        {
            InitializeComponent();
        }

        private void SexExpanderLostStylusCapture(object sender, StylusEventArgs e)
        {
            sexRadio.IsExpanded = false;
        }

        private void GetNation()
        {
            throw new NotImplementedException();
        }

        private void Border_LostFocus(object sender, RoutedEventArgs e)
        {
            sexRadio.IsExpanded = false;
        }
    }
}
