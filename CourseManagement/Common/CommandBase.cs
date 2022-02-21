using System;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StudentManagementSystem.Common
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)//逻辑判断  通用的，不可能命令都写一个类，所以需要传递委托来进行判断
        {
            return DoCanExecute?.Invoke(parameter) == true;//b|如果DoCanExecute是空的时候会返回Null所以需要转换一下类型 == true
        }

        public void Execute(object parameter)//执行  传递委托来进行处理相应的事件
        {
            DoExecute?.Invoke(parameter);/*parameters|step2:调用DoExecute委托,判断是否为空,传递一个parameter参数,如果不为空就执行委托*/
        }
        public Action<object> DoExecute { get; set; }/*parameters|step1:定义一个执行Execute的委托    Action<T> 委:封装一个方法，该方法只有一个参数并且不返回值*/
        public Func<object,bool> DoCanExecute { get; set; }//b|step1:定义判断的委托

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
