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
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; this.DoNotify(); }
        }

        private Visibility _showProgress = Visibility.Collapsed;
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
                  (o as Window).Close();//接收到window参数以后
              });
            this.CloseWindowCommand.DoCanExecute = new Func<object, bool>((o) =>
              {
                  return true;//关闭按钮不需要判断，直接返回true让按钮一直可用
              });

            this.LoginCommand = new CommandBase();
            this.LoginCommand.DoExecute = new Action<object>(DoLogin);
            this.LoginCommand.DoCanExecute = new Func<object, bool>((o) =>
            {
                return ShowProgress == Visibility.Collapsed;
            });

            Stochastic();

            LoginModel.UserName = "1";
            LoginModel.Password = "1";
        }

        public void Stochastic()//用于验证码随机数
        {
            //获取随机数
            string i = new Random().Next(1000, 9999).ToString();
            //LoginModel.RandomField = i;
            LoginModel.RandomField = "1";
        }

        private void DoLogin(object o)//主要的登录逻辑在DoLogin这个方法里面
        {
            #region 学习笔记[6]
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
                this.ErrorMessage = "请输入用户名！";//绑定到Login View的文本框里面
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (string.IsNullOrEmpty(LoginModel.Password))//判断输入密码是否为空
            {
                this.ErrorMessage = "请输入密码！";//绑定到Login View的文本框里面
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (string.IsNullOrEmpty(LoginModel.ValidationCode))//判断输入验证码是否为空
            {
                this.ErrorMessage = "请输入验证码！";//绑定到Login View的文本框里面
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (LoginModel.ValidationCode.ToLower() != LoginModel.RandomField)/*验证码*/
            {
                this.ErrorMessage = "验证码输入不正确！"; Stochastic();
                this.ShowProgress = Visibility.Collapsed;
                return;
            }

            Task.Run(new Action(/*async*//*[₃]*/() =>/*[₂]|线程*/
            {
                //await Task.Delay(2000);//登录时设置2秒延迟,如不需要就将async[₃]删除(注意:需保留括号)在将本行代码注释

                try//如果是本地数据库处理很快，但不是的话这里会被卡住，所以需要写一个[₂]线程
                {
                    var user = LocalDataAccess.GetInstance().CheckUserInfo(LoginModel.UserName, LoginModel.Password);
                    if (user == null)
                    {
                        throw new Exception("登录失败！用户名或密码错误！");
                    }

                    GlobalValues.UserInfo = user;//如果↑上面的代码没有报错，这里已经取得用户的基本信息了，需要新建一个类保存信息
                    

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        /*Window.DialogResult 属性：获取或设置对话框结果值，此值是从 ShowDialog 方法返回的值  默认值为 false*/
                        (o as Window).DialogResult = true; /* 《App.xaml.cs_₁》    |执行时跳转到这里，切换主窗口*/


                        //View.MainView main = new View.MainView();
                        //main.ShowDialog();
                        //login = (View.LoginView)o;//传递一个参数给login，否则login为null会报错
                        //login.Close();
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
