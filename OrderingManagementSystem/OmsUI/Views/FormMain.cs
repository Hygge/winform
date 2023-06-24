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

namespace OmsUI.Views
{
    public partial class FormMain : Form
    {
        // 用户类型：权限代表
        public int MType{ get; set; }

        private HallInfoBll hallInfoBll = new HallInfoBll();
        private TableInfoBll tableInfoBll = new TableInfoBll();

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
            LoadListHall();

        }
        // 加载包厅
        private void LoadListHall()
        {
            var list = hallInfoBll.GetList();
            foreach(var item in list)
            {
                // 创建标签页对象
                TabPage _tabPage = new TabPage(item.HTitle);

                // 获取当前厅包对象的所有餐桌
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("thallid", item.HId.ToString());
                var tableInfoList = tableInfoBll.List(dic);
                // 动态添加元素到父容器
                ListView listView = new ListView();
                // 停靠父容器
                listView.Dock = DockStyle.Fill;
                listView.LargeImageList = imageList1;
                _tabPage.Controls.Add(listView);

                // 向列表添加餐桌信息
                foreach(var item1 in tableInfoList)
                {
                   
                    listView.Items.Add(new ListViewItem(item1.TTitle, item1.TIsFree ? 0 : 1));  // 名称，图片索引

                }


                // 将标签页放入父容器
                tabControl1.TabPages.Add(_tabPage);
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
