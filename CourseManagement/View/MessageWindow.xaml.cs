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
using System.Windows.Shapes;

namespace StudentManagementSystem.View
{
    /// <summary>
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MessageBoxImageProperty = DependencyProperty.Register(
            "MessageBoxImage", typeof(MessageBoxImage), typeof(MessageWindow), new PropertyMetadata(default(MessageBoxImage)));

        /// <summary>
        /// 
        /// </summary>
        public MessageBoxImage MessageBoxImage
        {
            get => (MessageBoxImage)GetValue(MessageBoxImageProperty);
            set => SetValue(MessageBoxImageProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(MessageWindow), new PropertyMetadata(default(string)));

        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MessageBoxButtonProperty = DependencyProperty.Register(
            "MessageBoxButton", typeof(MessageBoxButton), typeof(MessageWindow), new PropertyMetadata(default(MessageBoxButton)));

        /// <summary>
        /// 
        /// </summary>
        public MessageBoxButton MessageBoxButton
        {
            get => (MessageBoxButton)GetValue(MessageBoxButtonProperty);
            set => SetValue(MessageBoxButtonProperty, value);
        }

        public MessageWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool? ShowWindow(string message)
        {
            return ShowWindow(message, "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static bool? ShowTopmost(string message)
        {
            return ShowWindow(message, "", MessageBoxButton.OK, MessageBoxImage.Information, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool? ShowWindow(string message, string title)
        {
            return ShowWindow(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">要显示的内容</param>
        /// <param name="title">（任务栏显示）窗口标题</param>
        /// <param name="button"></param>
        /// <param name="image">消息框所显示的图标</param>
        /// <returns></returns>
        public static bool? ShowWindow(string message, string title, MessageBoxButton button, MessageBoxImage image, bool topmost = false)
        {
            #region Notes
            /* MessageBoxButton枚举：指定显示在消息框上的按钮; 用作 Show 方法的参数。
               https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.messageboxbutton?redirectedfrom=MSDN&view=windowsdesktop-6.0 */

            /* MessageBoxImage枚举：指定消息框所显示的图标。
               https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.messageboximage?redirectedfrom=MSDN&view=windowsdesktop-6.0 */

            /* Application: 封装 Windows Presentation Foundation (WPF) 应用程序
               Application.Current属性: 获取当前 AppDomain 的 Application 对象
               https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.application?redirectedfrom=MSDN&view=windowsdesktop-6.0 */

            /* CheckAccess() 方法确定调用线程是否为与此 Dispatcher 关联的线程
               https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.threading.dispatcher?redirectedfrom=MSDN&view=windowsdesktop-6.0 */

            #endregion
            if (Application.Current.Dispatcher.CheckAccess())
            {
                var window = new MessageWindow()
                {
                    Message = message,
                    Title = title,
                    MessageBoxImage = image,
                    MessageBoxButton = button,
                    Topmost = topmost
                };
                PlaySound(image);

                return window.ShowDialog();
            }
            bool? result = null;

            Application.Current.Dispatcher.Invoke(() =>
            {
                var window = new MessageWindow()
                {
                    Message = message,
                    Title = title,
                    MessageBoxImage = image,
                    MessageBoxButton = button
                };
                PlaySound(image);

                result = window.ShowDialog();
            });

            return result;
        }

        /// <summary>
        /// 播放相关声音
        /// </summary>
        /// <param name="image"></param>
        public static void PlaySound(MessageBoxImage image)
        {
            switch (image)
            {
                case MessageBoxImage.None:
                    break;
                case MessageBoxImage.Hand:
                    System.Media.SystemSounds.Hand.Play();
                    break;
                case MessageBoxImage.Question:
                    System.Media.SystemSounds.Question.Play();
                    break;
                case MessageBoxImage.Exclamation:
                    System.Media.SystemSounds.Exclamation.Play();
                    break;
                case MessageBoxImage.Asterisk:
                    System.Media.SystemSounds.Asterisk.Play();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="button"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static bool? ShowWindow(Window owner, string message, string title, MessageBoxButton button, MessageBoxImage image)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                var window = new MessageWindow()
                {
                    Owner = owner,
                    Message = message,
                    Title = title,
                    MessageBoxImage = image,
                    MessageBoxButton = button,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                return window.ShowDialog();
            }

            bool? result = null;

            Application.Current.Dispatcher.Invoke(() =>
            {
                var window = new MessageWindow()
                {
                    Owner = owner,
                    Message = message,
                    Title = title,
                    MessageBoxImage = image,
                    MessageBoxButton = button
                };

                result = window.ShowDialog();
            });

            return result;
        }

        #region Notes
        /* Window.DialogResult 属性：获取或设置对话框结果值，此值是从 ShowDialog 方法返回的值。
           https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.window.dialogresult?redirectedfrom=MSDN&view=windowsdesktop-6.0#System_Windows_Window_DialogResult */

        /* Window.ShowDialog 方法：打开一个窗口，仅在新打开的窗口被关闭时返回。
           https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.window.showdialog?redirectedfrom=MSDN&view=windowsdesktop-6.0#System_Windows_Window_ShowDialog */
        #endregion
        private void YesButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void NoButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
