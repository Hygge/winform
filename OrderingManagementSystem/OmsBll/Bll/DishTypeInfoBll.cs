using CaterDal;
using domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsBll.Bll
{
    public partial class DishTypeInfoBll
    {
        private DishTypeInfoDal _dic = new DishTypeInfoDal();

        public List<DishTypeInfo> GetList()
        {
            return _dic.GetList();
        }

        public int AddDisTypeInfo(DishTypeInfo dishTypeInfo)
        {
            return _dic.Insert(dishTypeInfo);
        }

        public int UpdateDisTypeInfo(DishTypeInfo dishTypeInfo)
        {
            return _dic.Update(dishTypeInfo);
        }

        public int DeleteDisTypeInfo(int id)
        {
            return _dic.Delete(id);
        }

    }
}
