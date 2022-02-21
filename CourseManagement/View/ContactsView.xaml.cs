using StudentManagementSystem.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace StudentManagementSystem.View
{
    /// <summary>
    /// ContactsView.xaml 的交互逻辑
    /// </summary>
    public partial class ContactsView : UserControl
    {
        public ContactsView()
        {
            InitializeComponent();
            InterfaceData();
        }
        public void InterfaceData()
        {
            this.DataContext = new StudentInformationViewModel();
        }


        //添加功能 

        private void AddStudentButtonClick(object sender, RoutedEventArgs e)
        {
            AddDataView addData = new AddDataView();
            addData.ShowDialog();
        }

        //刷新界面
        private void FlushStudentButtonClick(object sender, RoutedEventArgs e)
        {
            InterfaceData();
        }
    }
}
