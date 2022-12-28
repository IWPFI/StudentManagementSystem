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
            this.DataContext = new StudentInformationViewModel();
        }
    }
}
