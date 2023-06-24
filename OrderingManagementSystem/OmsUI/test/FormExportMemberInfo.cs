using domain.Model;
using OmsBll.Bll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmsUI.test
{
    public partial class FormExportMemberInfo : Form
    {
        private MemberInfoBll memberInfoBll = new MemberInfoBll();

     
        public FormExportMemberInfo()
        {
            InitializeComponent();
        }



        private void btnExportMemberInfo_Click(object sender, EventArgs e)
        {
            /*       Dictionary<string, string> dic =  new Dictionary<string, string>();
                   List<MemberInfo> list = memberInfoBll.List(dic);
       */
            // 导出到execl
            string fileName = @"E:\code\C#\winform\OrderingManagementSystem\OmsUI\test\00_new.xls";
            memberInfoBll.ExportExecl(fileName);


        }

        private void FormExportMemberInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
