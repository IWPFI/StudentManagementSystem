using StudentManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentManagementSystem.View
{
    /// <summary>
    /// FirstPageView.xaml 的交互逻辑
    /// </summary>
    public partial class FirstPageView : UserControl
    {
        public FirstPageView()
        {
            InitializeComponent();

            this.DataContext = new FirstPageViewModel();
            string a="The student management system is to help teachers and students learn better. " +
                "The system is composed of course selection management system, " +
                "textbook management system and classmate address book management system to realize the network course selection, " +
                "textbook management and student information management of higher vocational colleges.";
            Introduction.Text = a;
        }
    }
}
