using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentManagementSystem.Common
{
    /*INotifyPropertyChanged接口 向客户端发出某一属性值已更改的通知。*/
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void DoNotify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));//达到通知效果
        }
    }
}
