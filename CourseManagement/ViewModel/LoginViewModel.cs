using System;
using System.Text;
using System.Linq;
using System.Windows;
using StudentManagementSystem.Model;
using System.Threading.Tasks;
using StudentManagementSystem.Common;
using System.Collections.Generic;
using StudentManagementSystem.DataAccess;

namespace StudentManagementSystem.ViewModel
{
    public class LoginViewModel:NotifyBase
    {
        public View.LoginView login;

        public LoginModel LoginModel { get; set; } = new LoginModel();

        public CommandBase CloseWindowCommand { get; set; }

        public CommandBase LoginCommand { get; set; }

        private string _errorMessage;

        private Visibility _showProgress = Visibility.Collapsed;

        /// <summary>
        /// 错误提示信息
        /// </summary>
        /// <remarks>绑定到LoginView的文本框</remarks>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; this.DoNotify(); }
        }

        public Visibility ShowProgress
        {
            get { return _showProgress; }
            set
            {
                _showProgress = value; this.DoNotify();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public LoginViewModel()
        {
            this.CloseWindowCommand = new CommandBase();
            this.CloseWindowCommand.DoExecute = new Action<object>((o) =>
              {
                  (o as Window).Close();
              });
            this.CloseWindowCommand.DoCanExecute = new Func<object, bool>((o) =>
              {
                  return true;
              });

            this.LoginCommand = new CommandBase();
            this.LoginCommand.DoExecute = new Action<object>(DoLogin);
            this.LoginCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return ShowProgress == Visibility.Collapsed;
            });

            Stochastic();
        }

        /// <summary>
        /// 用于验证码随机数
        /// </summary>
        public void Stochastic()
        {
            //获取随机数
            string i = new Random().Next(1000, 9999).ToString();
            //LoginModel.RandomField = i;
            LoginModel.RandomField = "1234";
        }

        /// <summary>
        /// 登录逻辑
        /// </summary>
        private void DoLogin(object o)
        {
            #region Notes
            /* Visibility 枚举：指定元素的显示状态
             
               Visibility.Collapsed 不显示元素，且不为其保留布局空间
               Visibility.Hidden 不显示元素，但为元素保留布局空间。
               Visibility.Visible 显示元素*/

            /* String.IsNullOrEmpty 方法：指示指定的字符串是 null 还是 Empty 字符串。*/
            #endregion

            this.ShowProgress = Visibility.Visible;

            this.ErrorMessage = "";
            if (string.IsNullOrEmpty(LoginModel.UserName))//判断输入账号是否为空
            {
                this.ErrorMessage = "请输入用户名！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (string.IsNullOrEmpty(LoginModel.Password))//判断输入密码是否为空
            {
                this.ErrorMessage = "请输入密码！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (string.IsNullOrEmpty(LoginModel.ValidationCode))//判断输入验证码是否为空
            {
                this.ErrorMessage = "请输入验证码！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (LoginModel.ValidationCode.ToLower() != LoginModel.RandomField)//验证码
            {
                this.ErrorMessage = "验证码输入不正确！"; Stochastic();
                this.ShowProgress = Visibility.Collapsed;
                return;
            }

            Task.Run(new Action(async/*₃*/() =>/*₂Action线程*/
            {
                await Task.Delay(1000);//登录时设置1秒延迟,如不需要就将async₃删除(注意:需保留括号)

                try//如果是本地数据库处理很快，但不是的话这里会被卡住，所以需要写一个₂Action线程
                {
                    var user = LocalDataAccess.GetInstance().CheckUserInfo(LoginModel.UserName, LoginModel.Password);
                    if (user == null)
                    {
                        throw new Exception("登录失败！用户名或密码错误！");
                    }

                    GlobalValues.UserInfo = user;//If ↑ the above code does not report an error,
                                                 //the basic information of the user has been obtained here.
                                                 //You need to create a new class to save the information


                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        //Window.DialogResult 属性：获取或设置对话框结果值，此值是从 ShowDialog 方法返回的值 默认值为 false
                        (o as Window).DialogResult = true; // App.xaml.cs_₁ Jump here during execution, Switch main window
                    }));
                }
                catch (Exception ex)
                {
                    this.ErrorMessage = ex.Message;
                }
                finally
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        this.ShowProgress = Visibility.Collapsed;
                    }));
                }
            }));
        }
    }
}
