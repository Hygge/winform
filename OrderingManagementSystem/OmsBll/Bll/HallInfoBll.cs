using CaterDal;
using domain.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsBll.Bll
{
    public partial class HallInfoBll
    {
        private HallInfoDal _dic = new HallInfoDal();

        public List<HallInfo> GetList()
        {
            return _dic.GetList();
        }

        public int AddHallInfo(HallInfo hallInfo)
        {
            return _dic.Insert(hallInfo);
        }

        public int UpdateHallInfo(HallInfo hallInfo)
        {
            return _dic.Update(hallInfo);
        }

        public int DeleteHallInfo(int id)
        {
            return _dic.Delete(id);
        }

       
    }
}
