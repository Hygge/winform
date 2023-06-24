using OmsCommon;
using OmsDal.Dal;
using OmsDal.Utils;
using OmsModel.Domain;
using OmsModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsBll.Service
{
    public partial class ManagerInfoBll
    {
        private ManagerInfoDal managerInfoDal = new ManagerInfoDal();

        // 获取所有餐厅人员信息
        public  List<ManagerInfo> List()
        {
          //  return SqliteHelper.ExecuteReader();
            return managerInfoDal.selectList();
        }

        public int AddManagerInfo(ManagerInfo managerInfo)
        {
            // 密码加密
            managerInfo.MPwd = Md5Util.EncryptString(managerInfo.MPwd);
            return managerInfoDal.insert(managerInfo);
        }

        public int UpdateManagerInfo(ManagerInfo managerInfo)
        {      
            return managerInfoDal.update(managerInfo);
        }
        public int DeleteManagerInfo(ManagerInfo managerInfo)
        {
            return managerInfoDal.delete(managerInfo);
        }

        public LoginState Login(ManagerInfo managerInfo, out int type)
        {
            ManagerInfo info = managerInfoDal.selectOneByName(managerInfo.MName);
            type = 0;
            if (info == null)
            {                
                return LoginState.NameError;
            }
            if(info.MPwd.Equals(Md5Util.EncryptString(managerInfo.MPwd)))
            {
                type = info.MType;
                return LoginState.Ok;
            }
            return LoginState.PwdError;
        }
    }
}
