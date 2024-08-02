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
            ConnectionString = $"PORT=5432;DATABASE=SqlSugar4xTe111st;HOST=localhost;PASSWORD=postgres;USER ID=postgres",
            DbType = SqlSugar.DbType.PostgreSQL,
            InitKeyType = InitKeyType.Attribute,
            IsAutoCloseConnection = true,
            AopEvents = new AopEvents
            {
                OnLogExecuting = (sql, p) =>
                {
                    Console.WriteLine(sql);
                    Console.WriteLine(string.Join(",", p?.Select(it => it.ParameterName + ":" + it.Value)));
                }
            }
        });
    }
}
