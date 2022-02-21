using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Model
{
    public class SeriesModel
    {
        public string SeriesName { get; set; }/*名称*/

        //public int CurrentValue { get; set; }
        public decimal CurrentValue { get; set; }/*当前值*/

        public bool IsGrowing { get; set; }

        public int ChangeRate { get; set; }
    }
}
