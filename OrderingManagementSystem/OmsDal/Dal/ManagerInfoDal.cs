using OmsDal.Utils;
using OmsModel.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsDal.Dal
{
    public partial class ManagerInfoDal
    {
        

        public List<ManagerInfo> selectList()
        {
            string sql = "select * from ManagerInfo";
            DataTable table = SqliteHelper.ExecuteDataTable(sql);
            var _ = new List<ManagerInfo>();
            foreach (DataRow dr in table.Rows)
            {
                _.Add(SqliteHelper.DataRowToModel<ManagerInfo>(dr));
            }
            return _;
        }

        /// <summary>
        /// 插入一个店员信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int insert(ManagerInfo info)
        {
            string sql = "insert into ManagerInfo (MName, MPwd, MType) values (@name, @pwd, @type)";
            return SqliteHelper.ExecuteNoQuery(sql,
                new SQLiteParameter("@name", info.MName),
                new SQLiteParameter("@pwd", info.MPwd),
                new SQLiteParameter("@type", info.MType)
                );
        }

        public int update(ManagerInfo info)
        {
            string sql = "update ManagerInfo set MName=@name, MPwd=@pwd, MType=@type where MId=@id";
            return SqliteHelper.ExecuteNoQuery(sql,
                new SQLiteParameter("@name", info.MName),
                new SQLiteParameter("@pwd", info.MPwd),
                new SQLiteParameter("@type", info.MType),
                new SQLiteParameter("@id", info.MId)
                );
        }
        public int delete(ManagerInfo info)
        {
            string sql = "delete from ManagerInfo where MId=@id";
            return SqliteHelper.ExecuteNoQuery(sql,
                          new SQLiteParameter("@id", info.MId)
                );
        }

  
        public ManagerInfo selectOneByName(string name)
        {
            string sql = "select * from ManagerInfo where MName = @name";
            DataTable table = SqliteHelper.ExecuteDataTable(sql, new SQLiteParameter("@name", name));
            if (table.Rows.Count > 0 )
            {
                ManagerInfo info = new ManagerInfo();
                info.MName = name;
                info.MId = Convert.ToInt32(table.Rows[0][0]);
                info.MType = Convert.ToInt32(table.Rows[0][3]);
                info.MPwd = table.Rows[0][2].ToString();
         
                return info;
            }
          
            return null;
        }
    }
}
