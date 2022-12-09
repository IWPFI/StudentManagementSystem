using System.Windows;
using System.Windows.Input;
using StudentManagementSystem.ViewModel;

namespace StudentManagementSystem.View
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登录窗口拖动
        /// </summary>
        private void WinMove_LeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            #region Notes
            /*
            MouseButtonState：指定鼠标按钮的可能状态 （备注:MouseButtonState 枚举可指定与鼠标按钮的状态相关联的常量）
            Pressed：该按钮处于按下状态；
            Released：该按钮处于释放状态；

            MouseEventArgs.LeftButton：获取鼠标左键的当前状态
           */
            #endregion
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
