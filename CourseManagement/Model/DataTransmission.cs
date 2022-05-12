using StudentManagementSystem.Common;
using StudentManagementSystem.DataAccess;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

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
