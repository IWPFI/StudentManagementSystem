using StudentManagementSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Model
{
    /// <summary>
    /// 学生信息类
    /// </summary>
    public class StudentInformation : NotifyBase
    {
        private string _studentId;
        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentID
        {
            get { return _studentId; }
            set
            {
                _studentId = value;
                this.DoNotify();
            }
        }

        private string _studentName;
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName
        {
            get { return _studentName; }
            set
            {
                _studentName = value;
                this.DoNotify();
            }
        }

        private int? _studentSex = 0;
        /// <summary>
        /// 学生性别
        /// </summary>
        public int? StudentSex
        {
            get => _studentSex;
            set
            {
                _studentSex = value;
                this.DoNotify();
            }
        }

        private int _studentAge;
        /// <summary>
        /// 学生年龄
        /// </summary>
        public int StudentAge
        {
            get => _studentAge;
            set
            {
                _studentAge = value;
                this.DoNotify();
            }
        }

        private DateTime? _studentBirthday;
        /// <summary>
        /// 学生生日
        /// </summary>
        public DateTime? StudentBirthday
        {
            get { return _studentBirthday; }
            set
            {
                _studentBirthday = value;
                this.DoNotify();
            }
        }

        private string _studentPhone;
        /// <summary>
        /// 学生电话
        /// </summary>
        public string StudentPhone
        {
            get { return _studentPhone; }
            set
            {
                _studentPhone = value;
                this.DoNotify();
            }
        }

        private string _studentGrade;
        /// <summary>
        /// 学生班级
        /// </summary>
        public string StudentGrade
        {
            get { return _studentGrade; }
            set
            {
                _studentGrade = value;
                this.DoNotify();
            }
        }

        private string _studentSite;
        /// <summary>
        /// 学生地址
        /// </summary>
        public string StudentSite
        {
            get { return _studentSite; }
            set
            {
                _studentSite = value;
                this.DoNotify();
            }
        }

        private int _id;
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                this.DoNotify();
            }
        }


        private string _nationsName;
        /// <summary>
        /// 民族
        /// </summary>
        public string NationsName
        {
            get { return _nationsName; }
            set
            {
                _nationsName = value;
                this.DoNotify();
            }
        }

        private string _politicsStatus;
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticsStatus
        {
            get { return _politicsStatus; }
            set
            {
                _politicsStatus = value;
                this.DoNotify();
            }
        }

    }
}
