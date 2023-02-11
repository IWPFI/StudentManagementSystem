namespace StudentManagementSystem.ViewModel
{
    public class TextbookViewModel : ViewModelBase
    {
        public TextbookViewModel()
        {
            DateList = new List<string>();
            GetDate();
        }

        private List<string> _dateList;

        public List<string> DateList
        {
            get { return _dateList; }
            set { _dateList = value;DoNotify(); }
        }

        public void GetDate()
        {
            DateList.Clear();
            int year = 2023;//年
            int month = 2;//月
            //if (textBox.Text != null && textBox1.Text != null)
            //{
            //    year = int.Parse(textBox.Text);
            //    month = int.Parse(textBox1.Text);
            //}
            //计算1900-year-1年经过的天数
            int crossDaysOfYear = 0;
            for (int i = 1900; i < year; i++)
            {
                if (i % 4 == 0 && i % 100 != 0 || i % 400 == 0)
                {
                    crossDaysOfYear += 366;
                }
                else
                {
                    crossDaysOfYear += 365;
                }
            }
            //在year这一年从1月到month-1月经过的天数
            int crossDaysOfMonth = 0;
            for (int i = 1; i < month; i++)
            {
                if (i == 2)
                {
                    if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                    {
                        crossDaysOfMonth += 29;
                    }
                    else
                    {
                        crossDaysOfMonth += 28;
                    }
                }
                else if (i <= 7 && i % 2 != 0 || i % 2 == 0)
                {
                    crossDaysOfMonth += 31;
                }
                else
                {
                    crossDaysOfMonth += 30;
                }
            }
            int crossDays = crossDaysOfYear + crossDaysOfMonth;
            int dayOfWeek = crossDays % 7 + 1;
            int space = dayOfWeek - 1;
            for (int i = 0; i < space; i++)
            {
                DateList.Add(null);
            }

            //将日期数字
            int? days;
            if (month == 2)
            {
                if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                {
                    days = 29;
                }
                else
                {
                    days = 28;
                }
            }
            else if (month <= 7 && month % 2 != 0 || month % 2 == 0)
            {
                days = 31;
            }
            else
            {
                days = 30;
            }
            for (int i = 1; i <= days; i++)
            {
                DateList.Add(i.ToString());
            }
            for (int i = DateList.Count; i < 42; i++)
            {
                DateList.Add(null);
            }
        }
    }
}
