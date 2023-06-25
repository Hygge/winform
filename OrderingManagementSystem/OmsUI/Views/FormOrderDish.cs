using domain.Model;
using OmsBll.Bll;
using OmsModel.Dto;
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
    public partial class FormOrderDish : Form
    {
        private DishInfoBll dishInfoBll = new DishInfoBll();
        private DishTypeInfoBll dishTypeInfoBll = new DishTypeInfoBll();
        private OrderInfoBll orderInfoBll = new OrderInfoBll();

        public event Action RefreshHall;

        public FormOrderDish()
        {
            InitializeComponent();
        }

        private void FormOrderDish_Load(object sender, EventArgs e)
        {
            // 刷新外面父组件 ListView中item 的Tag标签代表桌子id
            RefreshHall();

            LoadDishList();
            LoadDishType();
            LoadOrderDetailList();

        }

        // 加载菜单和分类
        private void LoadDishList()
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(txtTitle.Text))
            {
                map.Add("dchar", txtTitle.Text);
            }
            if (ddlType.SelectedIndex > 0)
            {
                map.Add("dtypeId", ddlType.SelectedValue.ToString());
            }
            List<DishInfo> list = dishInfoBll.GetList(map);
            dgvAllDish.AutoGenerateColumns = false;
            dgvAllDish.DataSource = list;

        }

        // 加载已下单菜品
        private void LoadOrderDetailList()
        {
            List<TakeOrderDishInfoDTO> takeOrderDishInfos = orderInfoBll.TakeList(Convert.ToInt32(this.Tag));
            dgvOrderDetail.AutoGenerateColumns=false;
            dgvOrderDetail.DataSource = takeOrderDishInfos;

            // 计算消费金额   lblMoney
            decimal money = new decimal(0);
            takeOrderDishInfos.ForEach(item =>
            {
                decimal total = item.DPrice * item.Count;
                money += total;
            });
            lblMoney.Text = money.ToString();

        }


        // 加载分类
        private void LoadDishType()
        {           

            List<DishTypeInfo> list1 = dishTypeInfoBll.GetList();
            list1.Insert(0, new DishTypeInfo
            {
                DId = 0,
                DTitle = "请选择"
            });

            ddlType.ValueMember = "DId";
            ddlType.DisplayMember = "DTitle";
            ddlType.DataSource = list1;

        }

 
        private void FormOrderDish_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        // 下拉框动态查询
        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDishList();
        }

        // 双击点菜
        private void dgvAllDish_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int orderId = Convert.ToInt32(this.Tag);
            DataGridViewRow row =  dgvAllDish.Rows[e.RowIndex];
            int dishId = Convert.ToInt32(row.Cells[0].Value);
            // 加菜保存数据库
            int res = orderInfoBll.TakeDishByOrderId(orderId, dishId);
            if (res > 0)
            {
                MessageBox.Show("添加成功");
                LoadOrderDetailList();
            }
            else{
                MessageBox.Show("添加失败");
            }

        }

        // 删除菜品按钮
        private void btnRemove_Click(object sender, EventArgs e)
        {
          
            if (dgvOrderDetail.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("是否删除该菜品？", "提示", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    int oId = Convert.ToInt32(dgvOrderDetail.SelectedRows[0].Cells[0].Value);
                    orderInfoBll.DeleteDetailById(oId);
                    LoadOrderDetailList();
                }                
            }

        }

        // 输入单元格后失去焦点触发事件
        private void dgvOrderDetail_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
     
 

        }

        // 列编辑后操作 
        private void dgvOrderDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var row = dgvOrderDetail.Rows[e.RowIndex];
                int oId = Convert.ToInt32(row.Cells[0].Value);
                int count = Convert.ToInt32(row.Cells[2].Value);
                orderInfoBll.UpdateCountByOId(oId, count);

                // 计算消费金额   lblMoney
                decimal money = new decimal(0);
                (dgvOrderDetail.DataSource as List<TakeOrderDishInfoDTO>).ForEach(item =>
                {
                    decimal total = item.DPrice * item.Count;
                    money += total;
                });
                lblMoney.Text = money.ToString();
            }

        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
           if( dgvOrderDetail.Rows.Count > 0)
            {
                orderInfoBll.TakeOrderDetail(Convert.ToInt32(this.Tag), Convert.ToDecimal(lblMoney.Text));
                MessageBox.Show("下单成功");
            }

        }
    }
}
