using StudentManagementSystem.Common;
using StudentManagementSystem.Model;
using System;
using System.Reflection;
using System.Windows;

namespace StudentManagementSystem.ViewModel
{
    #region 学习笔记
    /* FrameworkElement 类：为 Windows Presentation Foundation (WPF) 元素提供 WPF 框架级属性集、事件集和方法集。 此类表示附带的 WPF 框架级实现，它是基于由 UIElement 定义的 WPF 核心级 API 构建的。
       https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.frameworkelement?redirectedfrom=MSDN&view=windowsdesktop-6.0 */

    /* Type类：https://docs.microsoft.com/zh-cn/dotnet/api/system.type?redirectedfrom=MSDN&view=net-6.0

       Type.GetType(String) 方法：获取具有指定名称的 Type，执行区分大小写的搜索。
       https://docs.microsoft.com/zh-cn/dotnet/api/system.type.gettype?redirectedfrom=MSDN&view=net-6.0#overloads 

       Type.GetConstructor 方法：获取当前 Type 的特定构造函数。重载此成员。
       https://docs.microsoft.com/zh-cn/dotnet/api/system.type.getconstructor?redirectedfrom=MSDN&view=net-6.0#overloads */

    /* ConstructorInfo 类：发现类构造函数的属性并提供对构造函数元数据的访问权。
       https://docs.microsoft.com/zh-cn/dotnet/api/system.reflection.constructorinfo?redirectedfrom=MSDN&view=net-6.0 */
    #endregion
    /*主要用于主界面用户信息（因为用户信息很多需要绑定所以单独新建一个UserModel₅类），搜索框输入，窗口界面*/
    public class MainViewModel : NotifyBase
    {
        public UserModel UserInfo { get; set; }

        private string _searchText;
        public string SearchText//主窗口搜索框绑定
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
        
        public CommandBase NavChangedCommand { get; set; }//₇导航切换命令（需要初始化，在构造函数里初始化）

        public MainViewModel()
        {
            UserInfo = new UserModel();/*₆初始化只要在用到这个东西前初始化都可以，写在哪里都可以*/
            this.NavChangedCommand = new CommandBase();
            this.NavChangedCommand.DoExecute = new Action<object>(DoNavChanged);//₇⇢₈ Action接收一个object参数调用DoNavChanged委托
            this.NavChangedCommand.DoCanExecute = new Func<object, bool>((o) => true);//按钮是否可用（需要委托）
            DoNavChanged("FirstPageView");
        }
        private void DoNavChanged(object obj)/*₈*/
        {
            #region 学习笔记
            /* Type 类：表示类型声明：类类型、接口类型、数组类型、值类型、枚举类型、类型参数、泛型类型定义，以及开放或封闭构造的泛型类型。
               https://docs.microsoft.com/zh-cn/dotnet/api/system.type?redirectedfrom=MSDN&view=net-6.0 */

            /* ConstructorInfo 类：发现类构造函数的属性，并提供对构造函数元数据的访问权限。
               https://docs.microsoft.com/zh-cn/dotnet/api/system.reflection.constructorinfo?redirectedfrom=MSDN&view=net-6.0 */
            #endregion

            Type type = Type.GetType("StudentManagementSystem.View." + obj.ToString());//通过反射的方式实现窗口切换
            ConstructorInfo cti = type.GetConstructor(System.Type.EmptyTypes);
            this.MainContent = (FrameworkElement)cti.Invoke(null);
        }
    }
}