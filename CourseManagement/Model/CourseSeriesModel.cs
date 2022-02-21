using LiveCharts;
using System.Collections.ObjectModel;

namespace StudentManagementSystem.Model
{
    public class CourseSeriesModel/*₉首页课程总览列表*/
    {
        public string CourseName { get; set; }//课程名称,如果需要调整就要带通知属性

        public SeriesCollection SeriesColection { get; set; }//首页课程总览中间列  这里是一个序列

        public ObservableCollection<SeriesModel> SeriesList { get; set; }//右边列  集合  因为这里面一个项就是一个对象，需要单独一个类进行承载
    }
}