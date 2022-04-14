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

        /// <summary>
        /// Get the data.
        /// </summary>
        public void InterfaceData()
        {
            this.DataContext = new StudentInformationViewModel();
        }


        /// <summary>
        /// 添加按钮
        /// </summary>
        private void AddStudentButtonClick(object sender, RoutedEventArgs e)
        {
            AddDataView addData = new AddDataView();
            addData.ShowDialog();
        }

        /// <summary>
        /// 刷新界面按钮
        /// </summary>
        private void FlushStudentButtonClick(object sender, RoutedEventArgs e)
        {
            InterfaceData();
        }

        /// <summary>
        /// 搜索按钮
        /// </summary>
        private void SearchStudentButtonClick(object sender, RoutedEventArgs e)
        {
            SearchView search = new SearchView();
            search.ShowDialog();
        }
    }
}
