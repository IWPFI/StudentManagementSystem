using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModel
{
    public class AddDataViewModel : BaseViewModel
    {
        public AddDataViewModel()
        {
            NationModelList = new ObservableCollection<string>(LocalDataAccess.GetInstance().GeiNation());
            PoliticalOutlookList =  new ObservableCollection<string>(LocalDataAccess.GetInstance().GetPoliticalOutlook());
        }
        public ObservableCollection<string> NationModelList { get; set; }
        public ObservableCollection<string> PoliticalOutlookList { get; set; }
    }

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
