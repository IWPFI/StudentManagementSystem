using StudentManagementSystem.Common;
using StudentManagementSystem.Model;
using StudentManagementSystem.View;
using System;
using System.Reflection;
using System.Windows;

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
    public class MainViewModel : ViewModelBase
    {

        public LoginModels UserInfo { get; set; } = new LoginModels();

        private string _searchText;
        private FrameworkElement _mainContent;

        /// <summary>
        /// 主窗口搜索框绑定
        /// </summary>
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; this.DoNotify(); }
        }

        public FrameworkElement MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; this.DoNotify(); }
        }

        private CommandBase _navChangedCommand;
        /// <summary>
        /// Navigation switching command
        /// </summary>
        /// <remarks>
        /// 导航切换命令
        /// </remarks>
        public CommandBase NavChangedCommand
        {
            get
            {
                _navChangedCommand = new CommandBase();
                _navChangedCommand.DoCanExecute = new Func<object, bool>((obj) => true);
                _navChangedCommand.DoExecute = new Action<object>(DoNavChanged);
                return _navChangedCommand;
            }
        }

        public MainViewModel()
        {
            UserInfo = GlobalValues.UserInfo;
            DoNavChanged("FirstPageView");
        }

        private void DoNavChanged(object obj)/*₈*/
        {
            #region Notes
            /* Type 类：表示类型声明：类类型、接口类型、数组类型、值类型、枚举类型、类型参数、泛型类型定义，以及开放或封闭构造的泛型类型。
               https://docs.microsoft.com/zh-cn/dotnet/api/system.type?redirectedfrom=MSDN&view=net-6.0 */

            /* ConstructorInfo 类：发现类构造函数的属性，并提供对构造函数元数据的访问权限。
               https://docs.microsoft.com/zh-cn/dotnet/api/system.reflection.constructorinfo?redirectedfrom=MSDN&view=net-6.0 */
            #endregion
            Type type = Type.GetType("StudentManagementSystem.View." + obj.ToString());//通过反射的方式实现窗口切换
            ConstructorInfo cti = type.GetConstructor(System.Type.EmptyTypes);
            this.MainContent = (FrameworkElement)cti.Invoke(null);
        }

        /// <summary>
        /// Search Button Click Command
        /// </summary>
        /// <remarks></remarks>
        public ICommand CmdLogOutBtnClick => new RelayCommand<object>((o) =>
        {
            (o as Window).Close();
            LoginView log = new LoginView();
            log.Show();
            new LoginView().Show();
        });
    }
}