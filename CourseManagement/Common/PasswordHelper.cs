using System.Collections.Generic;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Text;
using System;

namespace StudentManagementSystem.Common
{
    public class PasswordHelper
    {
        #region Notes
        //DependencyProperty:表示可通过方法设置的属性，如样式、数据绑定、动画和继承
        //附加的依赖属性，与界面进行交互和Common进行绑定
        #endregion
        public static readonly DependencyProperty PassWordProperty = DependencyProperty.RegisterAttached("Password"/*名称*/, typeof(string)/*类型*/, typeof(PasswordHelper)/*所有者类型*/, new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnPropertyChanged)));/*step1：a1|对依赖属性判断的时候会触发OnPropertyChanged委托*/

        public static string GetPassword(DependencyObject d)/*step3：封装两个依赖属性的方法*/
        {
            return d.GetValue(PassWordProperty).ToString();/*返回依赖属性的值*/
        }

        public static void SetPassword(DependencyObject d,string value)
        {
            d.SetValue(PassWordProperty, value);
        }

        public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordHelper), new FrameworkPropertyMetadata(default(bool), new PropertyChangedCallback(OnAttached)));

        public static bool GetAttached(DependencyObject d)
        {
            return (bool)d.GetValue(AttachProperty);
        }

        public static void SetAttached(DependencyObject d, bool value)
        {
            d.SetValue(AttachProperty, value);
        }

        static bool _isUpdating = false;

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)/*step2：parameters|定义OnPropertyChanged委托*/
        {
            PasswordBox password = d as PasswordBox;
            password.PasswordChanged -= Password_PasswordChanged;
            if (!_isUpdating) { password.Password = e.NewValue?.ToString(); /*依赖属性绑定以后，值变化后推送给被附加的控件的属性上*/}
            password.PasswordChanged += Password_PasswordChanged;
        }
        private static void OnAttached(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox password = d as PasswordBox;
            password.PasswordChanged += Password_PasswordChanged;
        }

        private static void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            _isUpdating = true;/*更新之前*/
            SetPassword(passwordBox, passwordBox.Password);
            _isUpdating = false;/*更新之后*/
        }
    }
}
