using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StudentManagementSystem.Converter
{
    public class GenderConverter : IValueConverter/*IValueConverter接口:提供一种将自定义逻辑应用于绑定的方式*/
    {
        /// <summary>
        /// 从 Model → View 转换
        /// </summary>
        /// <param name="value">默认绑定的原始值</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">通过前端(View)传进来的值</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;
            return value.ToString() == parameter.ToString();
        }
        /// <summary>
        /// 从 View → Model 转换
        /// </summary>
        /// <param name="value">默认绑定的原始值</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">通过前端(View)传进来的值</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;//这里UI点击男(女/[未知])直接返回去.
        }
    }
}
