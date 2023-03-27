using StudentManagementSystem.Common;
using StudentManagementSystem.Model;
using StudentManagementSystem.View;
using System;
using System.Reflection;
using System.Security.Policy;
using System.Windows;
using System.Windows.Navigation;

namespace StudentManagementSystem.ViewModel
{
    #region Notes
    /* FrameworkElement 类：为 Windows Presentation Foundation (WPF) 元素提供 WPF 框架级属性集、事件集和方法集。 此类表示附带的 WPF 框架级实现，它是基于由 UIElement 定义的 WPF 核心级 API 构建的。
       https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.frameworkelement?redirectedfrom=MSDN&view=windowsdesktop-6.0 */

    /* Type类：https://docs.microsoft.com/zh-cn/dotnet/api/system.type?redirectedfrom=MSDN&view=net-6.0

       Type.GetType(String) 方法：获取具有指定名称的 Type，执行区分大小写的搜索。
       https://docs.microsoft.com/zh-cn/dotnet/api/system.type.gettype?redirectedfrom=MSDN&view=net-6.0#overloads 

       Type.GetConstructor 方法：获取当前 Type 的特定构造函数。重载此成员。
       https://docs.microsoft.com/zh-cn/dotnet/api/system.type.getconstructor?redirectedfrom=MSDN&view=net-6.0#overloads */

    /* ConstructorInfo 类：发现类构造函数的属性并提供对构造函数元数据的访问权。
       https://docs.microsoft.com/zh-cn/dotnet/api/system.reflection.constructorinfo?redirectedfrom=MSDN&view=net-6.0 */

    /* It is mainly used for user information in the main interface 
     * (because there are many user information that need to be bound, a new UserModel₅ "class" is created separately),
     * search box input and window interface*/
    #endregion
    /// <summary>
    /// 主窗口
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        #region 属性[Property]
        public LoginModels UserInfo { get; set; } = new LoginModels();

        private string _searchText;
        /// <summary>
        /// 主窗口搜索框绑定
        /// </summary>
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; this.DoNotify(); }
        }

        private FrameworkElement _mainContent;
        public FrameworkElement MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; this.DoNotify(); }
        } 
        #endregion

        public MainViewModel()
        {
            UserInfo = GlobalValues.UserInfo;
            DoNavChanged("FirstPageView");
        }

        #region 命令[Command]
        /// <summary>
        ///退出登录命令
        /// </summary>
        /// <remarks></remarks>
        public ICommand CmdLogOutBtnClick => new RelayCommand<object>((o) =>
        {
            new LoginView().Show();
            (o as Window).Close();
            //Application.Current.Shutdown();

            //NavigationService.Navigate(new Url("LoginView.xaml"), UriKind.Relative);
        });

        /// <summary>
        /// 导航切换命令
        /// </summary>
        public ICommand CmdNavChanged => new RelayCommand<object>((obj) => DoNavChanged(obj));
        #endregion

        private void DoNavChanged(object obj)
        {
            //通过反射的方式实现窗口切换
            Type type = Type.GetType("StudentManagementSystem.View." + obj.ToString());
            ConstructorInfo cti = type.GetConstructor(System.Type.EmptyTypes);
            this.MainContent = (FrameworkElement)cti.Invoke(null);
        }
    }
}