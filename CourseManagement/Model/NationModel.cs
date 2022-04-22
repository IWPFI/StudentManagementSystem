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
        private int _id;
        private string _nationName;

        public NationModel()
        {
        }

        public NationModel(int id,string nation)
        {
            _id = id;
            _nationName = nation;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; DoNotify(); }
        }

        public string NationName
        {
            get { return _nationName; }
            set { _nationName = value; DoNotify(); }
        }
    }
}
