using StudentManagementSystem.Common;

namespace StudentManagementSystem.Model
{
    public class DataTransmission : NotifyBase
    {
        private string _radioButtonText ="<请选择性别>";
        public string RadioButtonText
        {
            get { return _radioButtonText; }
            set
            {
                _radioButtonText = value;
                DoNotify();
            }
        }
    }
}
