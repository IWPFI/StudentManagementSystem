using StudentManagementSystem.Common;

namespace StudentManagementSystem.Model
{
    public class DataTransmission : NotifyBase
    {
        private string _radioButtonText;
        private string _nation;
        private string _politicsStatus;

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

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation
        {
            get { return _nation; }
            set { _nation = value; DoNotify(); }
        }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticsStatus
        {
            get { return _politicsStatus; }
            set { _politicsStatus = value; DoNotify(); }
        }
    }
}
