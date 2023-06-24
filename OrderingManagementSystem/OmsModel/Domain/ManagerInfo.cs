using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsModel.Domain
{
    public class ManagerInfo
    {

        // 编号主键自动增长
        public int MId { get; set; }
        // 用户名
        public string MName { get; set; }
        // 密码 md5加密 utf-8
        public string MPwd { get; set; }
        // 类型  0普通店员 1经理
        public int MType { get; set; }


    }
}
