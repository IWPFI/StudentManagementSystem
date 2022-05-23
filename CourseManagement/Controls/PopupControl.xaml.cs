using StudentManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentManagementSystem.Controls
{
    /// <summary>
    /// PopupControl.xaml 的交互逻辑
    /// </summary>
    public partial class PopupControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PopupControl()
        {
            InitializeComponent();
            Controls(PopupStr);
        }

        public string PopupStr
        {
            get { return (string)GetValue(PopupStrProperty); }
            set { SetValue(PopupStrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupStr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupStrProperty =
            DependencyProperty.Register("PopupStr", typeof(string), typeof(PopupControl), new PropertyMetadata(""/*,new PropertyChangedCallback(GetPopupStr)*/));

        private static void GetPopupStr(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<string> PopupContentlList { get; set; }
        public void Controls(string str)
        {
            Style st = FindResource("RadioButtonStyle") as Style;

            switch (str)
            {
                case "1":
                    PopupContentlList = new ObservableCollection<string>(LocalDataAccess.GetInstance().GeiNation());
                    break;
                case "2":
                    PopupContentlList = new ObservableCollection<string>(LocalDataAccess.GetInstance().GetPoliticalOutlook());
                    break;
            }
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < PopupContentlList.Count; ++i)
                {
                    RadioButton radio = new RadioButton();
                    radio.Style = st;
                    radio.Content = PopupContentlList[i];

                    popupContentStackPanel.Children.Add(radio);
                    radio.Click += Radio_Click;
                }
            }
        }

        private void Radio_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((sender as RadioButton).Content.ToString());
        }
    }
}
