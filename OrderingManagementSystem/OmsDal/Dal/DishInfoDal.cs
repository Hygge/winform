using domain.Model;
using OmsDal.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CaterDal
{
    public partial class DishInfoDal
    {
        public List<DishInfo> GetList(Dictionary<string, object> dic)
        {
            string sql = @"select di.*,dti.dtitle as dTypeTitle 
                from dishinfo as di
                left join dishtypeinfo as dti
                on di.dtypeid=dti.did
                where di.dIsDelete=0 ";

            List<SQLiteParameter> listP=new List<SQLiteParameter>();
            //接收筛选条件
            if (dic.ContainsKey("dtitle"))
            {
                sql += " and di.DTitle like @dtitle";
                listP.Add(new SQLiteParameter("@dtitle", "%" + dic["dtitle"] + "%"));
            }
            if (dic.ContainsKey("dtypeId"))
            {
                sql += " and di.DTypeId = @dtypeId";
                listP.Add(new SQLiteParameter("@dtypeId",  dic["dtypeId"]));
            }


            DataTable dt = SqliteHelper.ExecuteDataTable(sql,listP.ToArray());

            List<DishInfo> list=new List<DishInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DishInfo()
                {
                    DId = Convert.ToInt32(row["did"]),
                    DTitle = row["dtitle"].ToString(),
                    DTypeTitle = row["dtypeTitle"].ToString(),
                    DChar = row["dchar"].ToString(),
                    DPrice = Convert.ToDecimal(row["dprice"])
                });
            }

            return list;
        }

        public int Insert(DishInfo di)
        {
            string sql = "insert into dishinfo(dtitle,dtypeid,dprice,dchar,dIsDelete) values(@title,@tid,@price,@dchar,0)";
            SQLiteParameter[] p =
            {
                new SQLiteParameter("@title",di.DTitle), 
                new SQLiteParameter("@tid",di.DTypeId), 
                new SQLiteParameter("@price",di.DPrice), 
                new SQLiteParameter("@dchar",di.DChar)
            };

            return SqliteHelper.ExecuteNoQuery(sql, p);
        }

        public int Update(DishInfo di)
        {
            string sql = "update dishinfo set dtitle=@title,dtypeid=@tid,dprice=@price,dchar=@dchar where did=@id";
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@title",di.DTitle), 
                new SQLiteParameter("@tid",di.DTypeId), 
                new SQLiteParameter("@price",di.DPrice), 
                new SQLiteParameter("@dchar",di.DChar), 
                new SQLiteParameter("@id",di.DId)
            };

            return SqliteHelper.ExecuteNoQuery(sql, ps);
        }

        public int Delete(int id)
        {
            string sql = "update dishinfo set dIsDelete=1 where did=@id";
            SQLiteParameter p = new SQLiteParameter("@id", id);

            return SqliteHelper.ExecuteNoQuery(sql, p);
        }
    }
}
