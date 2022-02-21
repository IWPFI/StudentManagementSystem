using StudentManagementSystem.Common;
using StudentManagementSystem.DataAccess;
using StudentManagementSystem.Model;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModel
{
    public class FirstPageViewModel : NotifyBase
    {
        private int _instrumentValue = 0;

        public int InstrumentValue
        {
            get { return _instrumentValue; }
            set { _instrumentValue = value; this.DoNotify(); }
        }

        private int _itemCount;

        public int ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; this.DoNotify(); }
        }

        public ObservableCollection<CourseSeriesModel> CourseSeriesList { get; set; } = new ObservableCollection<CourseSeriesModel>();//绑定到课程总览列表里面


        Random random = new Random();
        bool taskSwitch = true;
        List<Task> taskList = new List<Task>();
        public FirstPageViewModel()
        {
            this.RefreshInstrumentValue();

            this.InitCourseSeries();
        }

        private void InitCourseSeries()
        {
            //CourseSeriesList.Add(new CourseSeriesModel
            //{
            //    CourseName = "WPF编程",
            //    SeriesColection = new LiveCharts.SeriesCollection { new PieSeries {
            //        Title="云课堂",
            //        Values=new ChartValues<ObservableValue>{ new ObservableValue(123)},
            //        DataContext=false},new PieSeries {
            //        Title="B站",
            //        Values=new ChartValues<ObservableValue>{ new ObservableValue(123)},
            //        DataContext=false} },
            //    SeriesList = new ObservableCollection<SeriesModel>
            //    {
            //        new SeriesModel{SeriesName="抖音",CurrentValue=764,IsGrowing=false,ChangeRate=-75},
            //        new SeriesModel{SeriesName="抖音",CurrentValue=764,IsGrowing=true,ChangeRate=-75},
            //        new SeriesModel{SeriesName="抖音",CurrentValue=764,IsGrowing=false,ChangeRate=-75},
            //        new SeriesModel{SeriesName="抖音",CurrentValue=764,IsGrowing=true,ChangeRate=-75},
            //        new SeriesModel{SeriesName="抖音",CurrentValue=764,IsGrowing=false,ChangeRate=-75}
            //    }
            //});
            //CourseSeriesList.Add(new CourseSeriesModel
            //{
            //    CourseName = "WPF编程",
            //    SeriesColection = new LiveCharts.SeriesCollection { new PieSeries {
            //        Title="云课堂",
            //        Values=new ChartValues<ObservableValue>{ new ObservableValue(123)},
            //        DataContext=false},new PieSeries {
            //        Title="B站",
            //        Values=new ChartValues<ObservableValue>{ new ObservableValue(123)},
            //        DataContext=false} },
            //    SeriesList = new ObservableCollection<SeriesModel>
            //    {
            //        new SeriesModel{SeriesName="抖音",CurrentValue=764,IsGrowing=false,ChangeRate=-75},
            //        new SeriesModel{SeriesName="抖音",CurrentValue=764,IsGrowing=true,ChangeRate=-75},
            //        new SeriesModel{SeriesName="抖音",CurrentValue=764,IsGrowing=false,ChangeRate=-75},
            //        new SeriesModel{SeriesName="抖音",CurrentValue=764,IsGrowing=true,ChangeRate=-75},
            //        new SeriesModel{SeriesName="抖音",CurrentValue=764,IsGrowing=false,ChangeRate=-75}
            //    }
            //});

            var cList = LocalDataAccess.GetInstance().GetCoursePlayRecord();
            this.ItemCount = cList.Max(c => c.SeriesList.Count);//动态分列
            foreach (var item in cList)
                this.CourseSeriesList.Add(item);       
        }
        private void RefreshInstrumentValue()
        {
            var task = Task.Factory.StartNew(new Action(async () =>
            {
                while (taskSwitch)
                {
                    InstrumentValue = random.Next(Math.Max(this.InstrumentValue - 5, -10), Math.Min(this.InstrumentValue + 5, 90));

                    await Task.Delay(1000);
                }
            }));
            taskList.Add(task);
        }

        public void Dispose()
        {
            try
            {
                taskSwitch = false;
                Task.WaitAll(this.taskList.ToArray());
            }
            catch { }
        }
    }
}