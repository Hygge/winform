using OmsModel.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsDal.Utils
{
    public static class SqliteHelper
    {
        // 从配置文件获取
        private static string connectstr = ConfigurationManager.ConnectionStrings["sqliteUrl"].ConnectionString.ToString();

        public static List<ManagerInfo> ExecuteReader()
        {
            // 创建存储集合
            List<ManagerInfo> list = new List<ManagerInfo>();
            // 创建连接对象
            using (SQLiteConnection sqlConn = new SQLiteConnection(connectstr))
            {
                // 创建command对象
                SQLiteCommand cmd = new SQLiteCommand("select * from ManagerInfo ", sqlConn);
                // 打开连接
                sqlConn.Open();
                // 执行命令
                SQLiteDataReader reader = cmd.ExecuteReader();
                // 读取
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        list.Add(new ManagerInfo()
                        {
                            MId = Convert.ToInt32(reader["mid"]),
                            MName = reader["mname"].ToString(),
                            MPwd = reader["mpwd"].ToString(),
                            MType = Convert.ToInt32(reader["mtype"])
                        });
                    }
                }
            }
            return list;
        }

        // 获取结果集
        public static DataTable ExecuteDataTable(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(connectstr))
            {
                // 构造适配器对象
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, sqlConn);
                // 构造数据表格对象，用于接收结果集
                DataTable dt = new DataTable();
                adapter.SelectCommand.Parameters.AddRange(parameters);
                // 执行结果
                adapter.Fill(dt);
                return dt;                
            }
        }


        // 执行 insert update delete语句
        public static int ExecuteNoQuery(string sql, params SQLiteParameter[] parameter)
        {
            int result;
            using(SQLiteConnection sqlConn = new SQLiteConnection(connectstr))
            {
                sqlConn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, sqlConn))
                {
                    if (parameter.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameter);
                    }
                    result = cmd.ExecuteNonQuery();
                }
       
                sqlConn.Close();
            }
            return result;
        }

        // 获取首行首列的值
        public static object ExecuteScalar(string sql, params object[] parameter)
        {
            object result;
            using(SQLiteConnection sqlConn = new SQLiteConnection(connectstr))
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, sqlConn);
                sqlConn.Open();
                if (parameter.Length > 0)
                {
                    cmd.Parameters.AddRange(parameter);
                }
                result = cmd.ExecuteScalar();
                sqlConn.Close();
            }
            return result;
        }
       
        // 通过反射将行数据转换实体类对象
        public static ToModel DataRowToModel<ToModel>(this DataRow dr)
        {
            Type type = typeof(ToModel);
            ToModel md = (ToModel)Activator.CreateInstance(type);
            foreach (var prop in type.GetProperties())
            {
                object value = dr[prop.Name];
                if (prop.GetMethod.ReturnParameter.ParameterType.Name == "Int32")
                {
                    value = Convert.ToInt32(value);
                }
                prop.SetValue(md, value);
            }
            return md;
        }

    }
}
