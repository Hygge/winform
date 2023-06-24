using CaterDal;
using domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsBll.Bll
{
    public partial class TableInfoBll
    {
        private  TableInfoDal _tableInfoDal = new TableInfoDal();


        public int Save(TableInfo info)
        {
            return _tableInfoDal.Insert(info);
        }

        public int UpdateTableInfo(TableInfo info)
        {
            return _tableInfoDal.Update(info);
        }

        public List<TableInfo> List(Dictionary<string, string> dic)
        {
            return _tableInfoDal.GetList(dic);
        }

        public int DeleteTableInfo(int id)
        {
            return _tableInfoDal.Delete(id);
        }


    }
}
