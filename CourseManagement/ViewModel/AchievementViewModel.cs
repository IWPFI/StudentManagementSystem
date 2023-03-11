using StudentManagementSystem.Controls;
using StudentManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModel
{
    /// <summary>
    /// 成绩管理模块
    /// </summary>
    public class AchievementViewModel : ViewModelBase
    {
        public AchievementViewModel() { }

        private FrameworkElement _achievementContent;

        public FrameworkElement AchievementContent
        {
            get { return _achievementContent; }
            set { _achievementContent = value; this.DoNotify(); }
        }

        private List<AchievementModels> achievementModels = new List<AchievementModels>();

        public List<AchievementModels> AchievementModels
        {
            get { return achievementModels; }
            set { achievementModels = value; }
        }

        /// <summary>
        /// 成绩查询
        /// </summary>
        public ICommand CmdScoreQuery => new CommandBase()
        {
            DoCanExecute = new Func<object, bool>((o) => { return true; }),
            DoExecute = new Action<object>(async (o) =>
            {
                AchievementModels = await APIDataAccess.GetReportInfo();
            })
        };

        /// <summary>
        /// 成绩录入
        /// </summary>
        public ICommand CmdScoreEntry => new RelayCommand(() =>
        {
            DoNavChanged("ScoreQueryControl");
        });

        /// <summary>
        /// 成绩统计
        /// </summary>
        public ICommand CmdScoreStatistics => new RelayCommand(async () =>
        {
            AchievementModels = await APIDataAccess.GetReportInfo();
        });

        /// <summary>
        /// 成绩分析
        /// </summary>
        public ICommand CmdScoreAnalyse => new RelayCommand(() =>
        {

        });

        /// <summary>
        /// 报表管理
        /// </summary>
        public ICommand CmdReportManager => new RelayCommand(() =>
        {

        });


        private void DoNavChanged(object obj)
        {
            Type type = Type.GetType("StudentManagementSystem.Controls." + obj.ToString());
            ConstructorInfo cti = type.GetConstructor(System.Type.EmptyTypes);
            this.AchievementContent = (FrameworkElement)cti.Invoke(null);
        }
    }
}
