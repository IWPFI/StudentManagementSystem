using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Npgsql;
using StudentManagementSystem.DataAccess.DataEntity;
using StudentManagementSystem.Model;
using StudentManagementSystem.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace StudentManagementSystem.DataAccess
{
    public class LocalDataAccess
    {
        private static LocalDataAccess instance;

        private LocalDataAccess() { }
        public static LocalDataAccess GetInstance()
        {
            return instance ?? (instance = new LocalDataAccess());
        }
        /*?? 运算符称作 null 合并运算符。如果此运算符的左操作数不为 null，则此运算符将返回左操作数；否则返回右操作数 */
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataAdapter adapter;

        private void Dispose()
        {
            if (adapter != null)
            {
                adapter.Dispose();
                adapter = null;
            }
            if (comm != null)
            {
                comm.Dispose();
                comm = null;
            }
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        private bool DBConnection()/*₁₃连接初始化*/
        {
            string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;/*配置管理器*/
            if (conn == null)
                conn = new NpgsqlConnection(connStr);
            try
            {

                conn.Open();/*打开数据库*/
                return true;
            }
            catch
            {
                return false;/*如果不能打开 报异常*/
            }
        }

        public UserEntity CheckUserInfo(string userName, string pwd/*通过用户名和密码把数据库一整条数据读取出来*/)
        {
            try
            {
                if (DBConnection()/*如果₁₃连接成功的话进行*/)
                {
                    string userSql = "select * from sms_member where user_name=@user_name and password=@pwd and is_validation=1";
                    adapter = new NpgsqlDataAdapter(userSql, conn);
                    adapter.SelectCommand.Parameters.Add(new NpgsqlParameter("@user_name"/*名称*/, SqlDbType.VarChar) { Value = userName });
                    adapter.SelectCommand.Parameters.Add(new NpgsqlParameter("@pwd", SqlDbType.VarChar) { Value = pwd });
                    //adapter.SelectCommand.Parameters.Add(new SqlParameter("@pwd", SqlDbType.VarChar) { Value = MD5Provider.GetMD5String(pwd + "@" + userName) });//调用[MD5Procider.cs]类进行加密处理

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);

                    if (count <= 0)
                        throw new Exception("用户名或密码不正确！");

                    DataRow dr = table.Rows[0];
                    if (dr.Field<Int64>("is_can_login") == 0) { throw new Exception("当前用户没有权限使用此平台！"); }

                    UserEntity userInfo = new UserEntity();
                    userInfo.UserName = dr.Field<string>("user_name");
                    userInfo.RealName = dr.Field<string>("real_name");
                    userInfo.Password = dr.Field<string>("password");
                    userInfo.Avatar = dr.Field<string>("avatar");
                    userInfo.Gender = dr.Field<long>("gender");
                    return userInfo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return null;
        }

        public List<string> GetTeachers()
        {
            try
            {
                List<string> result = new List<string>();
                if (this.DBConnection())
                {
                    string sql = "select real_name from sms_member where is_teacher=1";
                    adapter = new NpgsqlDataAdapter(sql, conn);

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);
                    if (count > 0)
                    {
                        result = table.AsEnumerable().Select(c => c.Field<string>("real_name")).ToList();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        public List<CourseSeriesModel> GetCoursePlayRecord()
        {
            try
            {
                List<CourseSeriesModel> cModelList = new List<CourseSeriesModel>();
                if (DBConnection())
                {
                    //@：回车换行不报错
                    string userSql = @"SELECT a.course_name, a.course_id, b.play_count, b.is_growing, b.growing_rate , c.platform_name
FROM sms_course a
LEFT JOIN sms_play_record b
ON a.course_id = b.course_id
LEFT JOIN sms_platforms c
ON b.platform_id = c.platform_id
ORDER BY a.course_id,c.platform_id";
                    adapter = new NpgsqlDataAdapter(userSql, conn);

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);

                    string courseId = "";
                    CourseSeriesModel cModel = null;

                    foreach (DataRow dr in table.AsEnumerable())
                    {
                        string tempId = dr.Field<string>("course_id");
                        if (courseId != tempId)
                        {
                            courseId = tempId;
                            cModel = new CourseSeriesModel();
                            cModelList.Add(cModel);

                            cModel.CourseName = dr.Field<string>("course_name");
                            cModel.SeriesColection = new LiveCharts.SeriesCollection();
                            cModel.SeriesList = new System.Collections.ObjectModel.ObservableCollection<SeriesModel>();
                        }
                        if (cModel != null)
                        {
                            cModel.SeriesColection.Add(new PieSeries
                            {
                                Title = dr.Field<string>("platform_name"),
                                Values = new ChartValues<ObservableValue> { new ObservableValue((double)dr.Field<long>("play_count")) },
                                DataContext = false
                            });

                            cModel.SeriesList.Add(new SeriesModel
                            {
                                SeriesName = dr.Field<string>("platform_name"),
                                CurrentValue = dr.Field<long>("play_count"),
                                IsGrowing = dr.Field<Int64>("is_growing") == 1,
                                ChangeRate = (short)dr.Field<Int64>("growing_rate")
                            });
                        }
                    }
                }
                return cModelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        public List<CourseModel> GetCourses()
        {
            try
            {
                List<CourseModel> result = new List<CourseModel>();
                if (this.DBConnection())
                {
                    string sql = @"SELECT parameters.course_id,parameters.course_name,parameters.course_cover,parameters.course_url,parameters.description,c.real_name FROM sms_course parameters
                                   LEFT JOIN sms_course_teacher_relation b
                                   ON parameters.course_id=b.course_id
                                   LEFT JOIN sms_member c
                                   ON b.teacher_id=c.user_id
                                   ORDER BY parameters.course_id";
                    adapter = new NpgsqlDataAdapter(sql, conn);

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);
                    if (count > 0)//大于零说明有数据
                    {
                        string courseId = "";
                        CourseModel model = null;

                        foreach (DataRow dr in table.AsEnumerable())
                        {
                            string tempId = dr.Field<string>("course_id");
                            if (courseId != tempId)
                            {
                                courseId = tempId;

                                model = new CourseModel();
                                model.CourseName = dr.Field<string>("course_name");
                                model.Cover = dr.Field<string>("course_cover");
                                model.Url = dr.Field<string>("course_url");
                                model.Description = dr.Field<string>("description");

                                model.Teachers = new List<string>();

                                result.Add(model);
                            }
                            if (model != null)
                            {
                                model.Teachers.Add(dr.Field<string>("real_name"));
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// 学生信息
        /// </summary>
        /// <returns>学生Id、姓名、班级、性别</returns>
        public List<StudentInformation> GetStudents()
        {
            try
            {
                List<StudentInformation> result = new List<StudentInformation>();
                if (this.DBConnection())
                {
                    string sql = @"SELECT
	id,
	name,
	number,
	grade 
FROM
	sms_students
WHERE
    is_delete = 0;";
                    adapter = new NpgsqlDataAdapter(sql, conn);

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);
                    if (count > 0)//大于零说明有数据
                    {
                        int courseId = 0;
                        StudentInformation model = null;

                        foreach (DataRow dr in table.AsEnumerable())
                        {
                            int tempId = (int)dr.Field<Int64>("id");
                            if (courseId != tempId)
                            {
                                courseId = tempId;
                                model = new StudentInformation();
                                model.Id = (int)dr.Field<Int64>("id");
                                model.StudentID = dr.Field<string>("number");
                                model.StudentName = dr.Field<string>("name");
                                model.StudentGrade = dr.Field<string>("grade");
                                result.Add(model);
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// 学生详细资料
        /// </summary>
        /// <returns>全部</returns>
        public StudentInformation? StudentsDetails(string str)
        {
            try
            {
                if (this.DBConnection())
                {
                    string sql = @"SELECT s.id, s.number, s.name, s.sex, s.birthday, s.grade, s.site, s.phone, n.nations_name, p.politics_status 
FROM
	sms_students AS s
	INNER JOIN sms_nations AS n ON s.nation_id = n.id
	INNER JOIN sms_politics_status AS p ON s.politics_status_id = p.id 
WHERE
	s.number = @number AND is_delete = 0;";
                    adapter = new NpgsqlDataAdapter(sql, conn);
                    adapter.SelectCommand.Parameters.Add(new NpgsqlParameter("@number", str));

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);

                    if (count <= 0)
                        throw new Exception("数据不存在！");

                    DataRow dr = table.Rows[0];
                    StudentInformation model = new StudentInformation();
                    model.Id = (int)dr.Field<Int64>("id");
                    model.StudentID = dr.Field<string>("number");//学号
                    model.StudentName = dr.Field<string>("name");//姓名
                    model.StudentSex = (int)dr.Field<Int64>("sex");//性别
                    model.StudentPhone = dr.Field<string>("phone");//电话
                    model.StudentGrade = dr.Field<string>("grade");//班级
                    model.StudentSite = dr.Field<string>("site");//地址
                    model.StudentBirthday = dr.Field<DateTime>("birthday");//生日
                    model.NationsName = dr.Field<string>("nations_name");//民族
                    model.PoliticsStatus = dr.Field<string>("politics_status");//政治面貌
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <returns></returns>
        public void StudentsAmend(string[] GetVs)
        {
            try
            {
                if (this.DBConnection())
                {
                    string sql = "UPDATE sms_students SET number = ( '" + GetVs[0] + "' ),name = ('" + GetVs[1] + "')," +
                        "sex = (" + GetVs[2] + "),birthday = '" + GetVs[3] + "',phone = ('" + GetVs[5] + "'),grade = ('" + GetVs[4] + "')," +
                        "site = ('" + GetVs[6] + "'),gmt_modified = now() WHERE number = ('" + GetVs[0] + "')";
                    adapter = new NpgsqlDataAdapter(sql, conn);

                    DataSet table = new DataSet();
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// 删除学生信息
        /// </summary>
        /// <returns></returns>
        public int StudentsDelete(string str)
        {
            try
            {
                int result = 0;
                if (this.DBConnection())
                {
                    string sql = @"UPDATE sms_students 
SET is_delete = 1, gmt_modified = now()
WHERE
	number = ( '" + str + "' )";
                    adapter = new NpgsqlDataAdapter(sql, conn);

                    DataTable table = new DataTable();
                    result = adapter.Fill(table);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <returns></returns>
        public List<StudentInformation> AddStudents(string[] GetAdd)
        {
            try
            {
                List<StudentInformation> result = new List<StudentInformation>();
                if (this.DBConnection())
                {
                    #region Notes
                    /* SqlConnection.CreateCommand 方法：创建并返回一个与 SqlConnection 关联的 SqlCommand 对象；
                       https://docs.microsoft.com/zh-cn/dotnet/api/system.data.sqlclient.sqlconnection.createcommand?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0#System_Data_SqlClient_SqlConnection_CreateCommand */


                    /* SqlCommand.CommandText 属性：获取或设置要在数据源中执行的 Transact-SQL 语句、表名或存储过程。[要执行的 SQL 语句或存储，默认值为一个空字符串。]
                       https://docs.microsoft.com/zh-cn/dotnet/api/system.data.sqlclient.sqlcommand.commandtext?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0#System_Data_SqlClient_SqlCommand_CommandText */


                    /* SqlCommand.ExecuteScalar 方法：执行查询，并返回由查询返回的结果集中的第一行的第一列。 其他列或行将被忽略。
                       返回值类型： System.Object
                       为结果集中的第一行的第一列，如果结果集为空，则为 null 引用（Visual Basic 中 Nothing）。
                       [一般用来执行只有一行一列放回值的SQL查询]
                       https://docs.microsoft.com/zh-cn/dotnet/api/system.data.sqlclient.sqlcommand.executescalar?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0#System_Data_SqlClient_SqlCommand_ExecuteScalar */


                    /* SqlCommand.ExecuteNonQuery 方法：对连接执行 Transact-SQL 语句并返回受影响的行数。
                     * 对于 UPDATE、INSERT 和 DELETE 语句，返回值为该命令所影响的行数。 对于其他所有类型的语句，返回值为 -1。
                       [通常用于执行增加、删除、修改等SQL语句]
                       https://docs.microsoft.com/zh-cn/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0#System_Data_SqlClient_SqlCommand_ExecuteNonQuery */
                    #endregion

                    comm = conn.CreateCommand();
                    comm.CommandText = @"SELECT id FROM sms_students WHERE number = ('" + GetAdd[0] + "')AND is_delete = 0;";

                    //ExecuteScalar()一般用来执行只有一行一列放回值的SQL查询
                    if (comm.ExecuteScalar() != null)
                    {
                        MessageWindow.ShowWindow("已存在该学生或输入有误！添加失败！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        string sql = @"INSERT INTO sms_students ( number, name, sex, birthday, phone, grade, site, nation_id, politics_status_id, is_delete, gmt_create )
VALUES
	( '" + GetAdd[0] + "', '" + GetAdd[1] + "', " + GetAdd[2] + ", '" + GetAdd[3] + "', '" + GetAdd[4] + "', '" + GetAdd[5] + "', '" + GetAdd[6] + "', " + GetAdd[7] + ", " + GetAdd[8] + ", 0, now() );";
                        adapter = new NpgsqlDataAdapter(sql, conn);

                        DataTable table = new DataTable();
                        int count = adapter.Fill(table);
                        MessageWindow.ShowWindow("添加成功，请手动刷新界面！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public int AddStudents(StudentInformation student)
        {
            try
            {
                int count = -1;
                if (this.DBConnection())
                {
                    #region Notes
                    /* SqlConnection.CreateCommand 方法：创建并返回一个与 SqlConnection 关联的 SqlCommand 对象；
                       https://docs.microsoft.com/zh-cn/dotnet/api/system.data.sqlclient.sqlconnection.createcommand?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0#System_Data_SqlClient_SqlConnection_CreateCommand */


                    /* SqlCommand.CommandText 属性：获取或设置要在数据源中执行的 Transact-SQL 语句、表名或存储过程。[要执行的 SQL 语句或存储，默认值为一个空字符串。]
                       https://docs.microsoft.com/zh-cn/dotnet/api/system.data.sqlclient.sqlcommand.commandtext?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0#System_Data_SqlClient_SqlCommand_CommandText */


                    /* SqlCommand.ExecuteScalar 方法：执行查询，并返回由查询返回的结果集中的第一行的第一列。 其他列或行将被忽略。
                       返回值类型： System.Object
                       为结果集中的第一行的第一列，如果结果集为空，则为 null 引用（Visual Basic 中 Nothing）。
                       [一般用来执行只有一行一列放回值的SQL查询]
                       https://docs.microsoft.com/zh-cn/dotnet/api/system.data.sqlclient.sqlcommand.executescalar?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0#System_Data_SqlClient_SqlCommand_ExecuteScalar */


                    /* SqlCommand.ExecuteNonQuery 方法：对连接执行 Transact-SQL 语句并返回受影响的行数。
                     * 对于 UPDATE、INSERT 和 DELETE 语句，返回值为该命令所影响的行数。 对于其他所有类型的语句，返回值为 -1。
                       [通常用于执行增加、删除、修改等SQL语句]
                       https://docs.microsoft.com/zh-cn/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0#System_Data_SqlClient_SqlCommand_ExecuteNonQuery */
                    #endregion

                    comm = conn.CreateCommand();
                    comm.CommandText = @"SELECT id FROM sms_students WHERE number = '" + student.StudentID + "'AND is_delete = 0;";

                    //ExecuteScalar()一般用来执行只有一行一列放回值的SQL查询
                    if (comm.ExecuteScalar() != null)
                    {
                        MessageWindow.ShowWindow("已存在该学生或输入有误！添加失败！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        string sql = @"INSERT INTO sms_students ( number, name, sex, birthday, phone, grade, site, nation_id, politics_status_id, is_delete, gmt_create )
VALUES 
( @number, @name, @sex, @birthday, @phone, @grade, @site, 
( SELECT id FROM sms_nations WHERE nations_name = @nation ), 
( SELECT id FROM sms_politics_status WHERE politics_status = @politics_status ), 0, now() );";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.SelectCommand.Parameters.Add(new NpgsqlParameter("@number", student.StudentID == null ? DBNull.Value : student.StudentID));
                        adapter.SelectCommand.Parameters.Add(new NpgsqlParameter("@name", student.StudentName == null ? DBNull.Value : student.StudentName));
                        adapter.SelectCommand.Parameters.Add(new NpgsqlParameter("@sex", student.StudentSex == null ? DBNull.Value : student.StudentSex));
                        adapter.SelectCommand.Parameters.Add(new NpgsqlParameter("@birthday", student.StudentBirthday == null ? DBNull.Value : student.StudentBirthday));
                        adapter.SelectCommand.Parameters.Add(new NpgsqlParameter("@phone", student.StudentPhone == null ? DBNull.Value : student.StudentPhone));
                        adapter.SelectCommand.Parameters.Add(new NpgsqlParameter("@grade", student.StudentGrade == null ? DBNull.Value : student.StudentGrade));
                        adapter.SelectCommand.Parameters.Add(new NpgsqlParameter("@site", student.StudentSite == null ? DBNull.Value : student.StudentSite));
                        adapter.SelectCommand.Parameters.Add(new NpgsqlParameter("@nation", student.NationsName == null ? DBNull.Value : student.NationsName));
                        adapter.SelectCommand.Parameters.Add(new NpgsqlParameter("@politics_status", student.PoliticsStatus == null ? DBNull.Value : student.PoliticsStatus));
                        DataTable table = new DataTable();
                        count = adapter.Fill(table);
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// 搜索学生信息
        /// </summary>
        /// <returns></returns>
        public List<StudentInformation> SearchStudents(string seek)
        {
            #region Notes
            //执行多条语句用 ExecuteReader
            //ExecuteReader:返回包含数据的DataReader对象，通常配合DataReader对象用于完成只读、只进的查询操作
            // https://docs.microsoft.com/zh-cn/dotnet/api/system.data.sqlclient.sqlcommand.executereader?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0#System_Data_SqlClient_SqlCommand_ExecuteReader
            #endregion
            try
            {
                List<StudentInformation> result = new List<StudentInformation>();
                if (DBConnection())
                {
                    string sql = string.Format("SELECT id, number, name, grade, phone FROM sms_students WHERE ( number LIKE '%{0}%' OR name LIKE '%{0}%') AND is_delete = 0;", seek);
                    adapter = new NpgsqlDataAdapter(sql, conn);
                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);
                    if (count > 0)
                    {
                        string courseId = "";
                        StudentInformation model = null;

                        foreach (DataRow dr in table.AsEnumerable())
                        {
                            string tempId = dr.Field<string>("number");
                            if (courseId != tempId)
                            {
                                courseId = tempId;
                                model = new StudentInformation();
                                model.Id = (int)dr.Field<Int64>("id");//Id
                                model.StudentID = dr.Field<string>("number");//学号
                                model.StudentName = dr.Field<string>("name");//姓名
                                model.StudentGrade = dr.Field<string>("grade");//班级
                                model.StudentPhone = dr.Field<string>("phone");//电话
                                result.Add(model);
                            }
                        }
                    }
                    else
                    {
                        bool? unused = MessageWindow.ShowWindow("无符合条件的学生信息！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return result;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageWindow.ShowWindow(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                this.Dispose();
            }
            //throw new Exception();
        }

        /// <summary>
        /// 获取民族信息
        /// </summary>
        public List<string> GeiNation()
        {
            try
            {
                List<string> result = new List<string>();
                if (this.DBConnection())
                {
                    string sql = @"SELECT nations_name FROM sms_nations";
                    adapter = new NpgsqlDataAdapter(sql, conn);

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);
                    if (count > 0)//大于零说明有数据
                    {
                        result = table.AsEnumerable().Select(c => c.Field<string>("nations_name")).ToList();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// 获取政治面貌
        /// </summary>
        /// <returns></returns>
        public List<string> GetPoliticalOutlook()
        {
            try
            {
                List<string> result = new List<string>();
                if (this.DBConnection())
                {
                    string sql = @"SELECT politics_status FROM sms_politics_status";
                    adapter = new NpgsqlDataAdapter(sql, conn);

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);

                    if (count > 0)
                    {
                        result = table.AsEnumerable().Select(c => c.Field<string>("politics_status")).ToList();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
    }
}