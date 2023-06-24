using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmsUI.Views
{
    public partial class FormMain : Form
    {
        // 用户类型：权限代表
        public int MType{ get; set; }

  

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void menuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // 校验是否为经理，经理显示管理员菜单，普通店员不显示
            if (MType != 1)
            {
                menuManagerInfo.Visible = false;
            }

        }

        private void menuManagerInfo_Click(object sender, EventArgs e)
        {
            //展示店员管理窗口         
            FormManagerInfo FormManager = FormManagerInfo.CreatedFormManagerInfo();            
            FormManager.Show();
           

        }

        private void menuMemberInfo_Click(object sender, EventArgs e)
        {
            FormMemberList formMemberList = FormMemberList.CreatedFormMemberList();
            formMemberList.Show();
        }

        private void menuDishInfo_Click(object sender, EventArgs e)
        {
            FormDishInfo formDishInfo = FormDishInfo.CreatedFormDishInfo();
            formDishInfo.Show();
        }

        private void menuTableInfo_Click(object sender, EventArgs e)
        {
            FormTableInfo formTableInfo = FormTableInfo.CreatedFormTableInfo();
            formTableInfo.Show();
        }
    }
}
