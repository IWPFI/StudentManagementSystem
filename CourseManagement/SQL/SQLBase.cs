using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.SQL
{
    public class SQLBase
    {
        /// <summary>
        /// 初始化客户端
        /// </summary>
        public static SqlSugarClient db => new SqlSugarClient(new ConnectionConfig()
        {
            IsAutoCloseConnection = true,
            DbType = SqlSugar.DbType.PostgreSQL,
            ConnectionString = "PORT=5433;DATABASE=db632d2042e3384749accf96b125b4cda0XIAMU;HOST=139.196.89.94;PASSWORD=WlVkc2FHSllWVDA9;USER ID=lxiamul",
            LanguageType = LanguageType.Default//Set language

        }, it =>
        {
            // Logging SQL statements and parameters before execution
            // 在执行前记录 SQL 语句和参数
            it.Aop.OnLogExecuting = (sql, para) =>
            {
                Console.WriteLine(UtilMethods.GetNativeSql(sql, para));
            };
        });
    }
}
