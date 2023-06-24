using CaterDal;
using domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsBll.Bll
{
    public partial class MemberTypeInfoBll
    {
        private  MemberTypeInfoDal memberTypeInfoDal = new MemberTypeInfoDal();


        public int Save(MemberTypeInfo info)
        {
            return memberTypeInfoDal.Insert(info);
        }

        public int UpdateMemberTypeInfo(MemberTypeInfo info)
        {
            return memberTypeInfoDal.Update(info);
        }

        public List<MemberTypeInfo> List()
        {
            return memberTypeInfoDal.GetList();
        }

        public int DeleteMemberTypeInfo(int id)
        {
            return memberTypeInfoDal.Delete(id);
        }


    }
}
