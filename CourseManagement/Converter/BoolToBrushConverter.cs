using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace StudentManagementSystem.Converter
{
    public class BoolToBrushConverter : IValueConverter
    {
        //首页课程预览列表最右侧列表字体颜色
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && bool.Parse(value.ToString()))
            {
                return Brushes.LightGreen;
            }
            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
