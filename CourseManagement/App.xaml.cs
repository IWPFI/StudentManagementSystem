using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using StudentManagementSystem.View;

namespace StudentManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (new LoginView().ShowDialog() == true)//窗口切换
            {
                //₁
                new MainView().ShowDialog();
            }
            Application.Current.Shutdown();//关闭窗口
        }
    }
}
