using CaterDal;
using domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsBll.Bll
{
    public partial class DishInfoBll
    {
        private DishInfoDal _dic = new DishInfoDal();

        public List<DishInfo> GetList(Dictionary<string, object> dic)
        {
            return _dic.GetList(dic);
        }

        public int AddDishInfo(DishInfo dishInfo)
        {
            return _dic.Insert(dishInfo);
        }

        public int UpdateDishInfo(DishInfo dishInfo)
        {
            return _dic.Update(dishInfo);
        }

        public int DeleteDishInfo(int id)
        {
            return _dic.Delete(id);
        }

    }
}
