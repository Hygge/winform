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


namespace OmsUI.Views
{
    public partial class FormMain : Form
    {
        // 用户类型：权限代表
        public int MType{ get; set; }

        private HallInfoBll hallInfoBll = new HallInfoBll();
        private TableInfoBll tableInfoBll = new TableInfoBll();
        private OrderInfoBll orderInfoBll = new OrderInfoBll();

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
            // 清空原容器信息
            tabControl1.TabPages.Clear();
            var list = hallInfoBll.GetList();
            foreach(var item in list)
            {
                // 创建标签页对象
                TabPage _tabPage = new TabPage(item.HTitle);
            
                // 动态添加元素到父容器
                ListView listView = new ListView();
         
                // 停靠父容器
                listView.Dock = DockStyle.Fill;
                listView.LargeImageList = imageList1;
                _tabPage.Controls.Add(listView);

                // 添加双击事件完成开单功能
                listView.DoubleClick += ListView_DoubleClick;


                // 获取当前厅包对象的所有餐桌
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("thallid", item.HId.ToString());
                var tableInfoList = tableInfoBll.List(dic);
            

                // 向列表添加餐桌信息
                ListViewItemShow(tableInfoList, listView);

                // 将标签页放入父容器
                tabControl1.TabPages.Add(_tabPage);
            }

        }

        // 向列表添加餐桌信息
        private void ListViewItemShow(List<TableInfo> tableInfos, ListView listView)
        {           
            foreach (var item1 in tableInfos)
            {
                ListViewItem lvi = new ListViewItem(item1.TTitle, item1.TIsFree ? 0 : 1);
                // 保存餐桌id
                lvi.Tag = item1.TId;
                listView.Items.Add(lvi);  // 名称，图片索引

            }
        }

        // 餐桌双击事件
        private void ListView_DoubleClick(object sender, EventArgs e)
        {
  
            
            // 获取餐桌号，获取点菜，创建订单，保存
            ListView lv = sender as ListView;
            ListViewItem lvi = lv.SelectedItems[0];
            int tableId = Convert.ToInt32(lvi.Tag);
            // 判断是否下订单
            if (lvi.ImageIndex == 0)
            {
           
                int orderId = orderInfoBll.TokeOrder(tableId);
                if (orderId < 0)
                {
                    MessageBox.Show("开单失败");
                    return;
                }
                lvi.ImageIndex = 1;
                lvi.Tag = orderId;

            }
            else
            {
                //已下单
                lvi.Tag = orderInfoBll.GetOrderId(tableId);
            }

       

            //2.打开点菜窗体
            FormOrderDish formOrderDish = new FormOrderDish();
            formOrderDish.Tag = lvi.Tag;
            // 保存餐桌id    lvi.Tag = item1.TId; 打开窗体刷新外面餐桌保存的id
            formOrderDish.RefreshHall += LoadListHall;
            formOrderDish.Show();

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
            formTableInfo.Refresh += LoadListHall;
            formTableInfo.Show();
        }

        // 结账
        private void menuOrder_Click(object sender, EventArgs e)
        {
            // 找到选中的桌位，获取桌位的id，根据桌位id获取订单id，进行结账
            ListView lv = tabControl1.SelectedTab.Controls[0] as ListView;
            if(lv == null || lv.SelectedItems.Count < 1)
            {
                MessageBox.Show("请选中桌位，再进行结账");
                return;
            }
            int tableId = Convert.ToInt32(lv.SelectedItems[0].Tag);

            // 找到桌位id如果空闲或获取不到订单id则提示，请选中桌位进行结账
            int _orderId = orderInfoBll.GetOrderId(tableId);
            if(_orderId == 0)
            {
                MessageBox.Show("暂无需要结账订单");
                return;
            }

            // 获取到订单id，弹出结账弹窗
            FormOrderPay formOrderPay = new FormOrderPay();
            formOrderPay.Tag = _orderId;
            formOrderPay.RefreshHall += LoadListHall;
            formOrderPay.Show();
        }
    }
}
