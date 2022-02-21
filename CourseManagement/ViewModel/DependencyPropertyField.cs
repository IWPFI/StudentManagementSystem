using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//未使用
namespace StudentManagementSystem.ViewModel
{
    public class DependencyPropertyField: DependencyObject
    {
        public string SexShow
        {
            get { return (string)GetValue(RandomDigitProperty); }
            set { SetValue(RandomDigitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RandomDigit.  This enables animation, styling, binding, etc...
        public static DependencyProperty RandomDigitProperty =
            DependencyProperty.Register("SexShow", typeof(string), typeof(DependencyPropertyField), new PropertyMetadata(""));

    }
}
