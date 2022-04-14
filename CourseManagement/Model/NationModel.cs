using StudentManagementSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Model
{
    /// <summary>
    /// 民族
    /// </summary>
    public class NationModel : NotifyBase
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; DoNotify(); }
        }

        private string _nationName;

        public string NationName
        {
            get { return _nationName; }
            set { _nationName = value; DoNotify(); }
        }
    }
}
