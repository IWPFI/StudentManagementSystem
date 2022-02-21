using LiveCharts;
using System.Collections.ObjectModel;

namespace StudentManagementSystem.Model
{
    /// <summary>
    /// 首页课程总览列表
    /// </summary>
    public class CourseSeriesModel
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 首页课程总览中间列--序列
        /// </summary>
        public SeriesCollection SeriesColection { get; set; }

        /// <summary>
        /// 右边列--集合  因为这里面一个项就是一个对象，需要单独一个类进行承载
        /// </summary>
        public ObservableCollection<SeriesModel> SeriesList { get; set; }
    }
}