using StudentManagementSystem.Common;

namespace StudentManagementSystem.Model
{
    public class DataTransmission : NotifyBase
    {
        private string _radioButtonText;

        public DataTransmission()
        {

        }

        /// <summary>
        /// 性别
        /// </summary>
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
