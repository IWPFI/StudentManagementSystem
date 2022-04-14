using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StudentManagementSystem.Common
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 通知属性
        /// </summary>
        public void DoNotify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));//达到通知效果
        }
    }
}
