using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Model
{
    public class SeriesModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string SeriesName { get; set; }
        /// <summary>
        /// 当前值
        /// </summary>
        public decimal CurrentValue { get; set; }
        //public int CurrentValue { get; set; }
        /// <summary>
        /// 是否增长
        /// </summary>
        public bool IsGrowing { get; set; }
        /// <summary>
        /// 增长率
        /// </summary>
        public int ChangeRate { get; set; }
    }
}
