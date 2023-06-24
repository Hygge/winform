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
    public partial class FormDishTypeInfo : Form
    {
        private DishTypeInfoBll dishTypeInfoBll = new DishTypeInfoBll();

        private  DialogResult result = DialogResult.Cancel;

        private static FormDishTypeInfo formDishTypeInfo;

        public static FormDishTypeInfo CreatedFormDishTypeInfo()
        {
            if(formDishTypeInfo == null)
            {
                formDishTypeInfo = new FormDishTypeInfo(); 
            }
            return formDishTypeInfo;
        }

        public FormDishTypeInfo()
        {
            InitializeComponent();

            LoadList();
        }

        // 加载表格列表数据
        private void LoadList()
        {
            dgvList.DataSource = dishTypeInfoBll.GetList();
            //隐藏字段
            this.dgvList.Columns["DIsDelete"].Visible = false;
            
        }

        // 添加或修改按钮
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("标题不能为空");
                return;
            }
            DishTypeInfo dishTypeInfo = new DishTypeInfo();
            dishTypeInfo.DTitle = txtTitle.Text;
            int result1;
            // 添加时无编号
            if ("添加时无编号".Equals(txtId.Text))
            {
                result1 = dishTypeInfoBll.AddDisTypeInfo(dishTypeInfo);
            }
            else
            {
                dishTypeInfo.DId = Convert.ToInt32(txtId.Text);
                result1 = dishTypeInfoBll.UpdateDisTypeInfo(dishTypeInfo);
            }

            if (result1 > 0)
            {
                MessageBox.Show(btnSave.Text + "成功");
                btnCancel_Click(null, null);
            }
            else
            {
                MessageBox.Show(btnSave.Text + "失败");
            }
           
            LoadList();

            this.result = DialogResult.OK;

        }

        // 取消按钮重置输入框
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = string.Empty;

            btnSave.Text = "添加";
        }

        // 删除所选中行
        private void btnRemove_Click(object sender, EventArgs e)
        {
            // 删除选中行
            DataGridViewSelectedRowCollection rows = dgvList.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                dishTypeInfoBll.DeleteDisTypeInfo(Convert.ToInt32(row.Cells[0].Value));
            }
            MessageBox.Show("删除成功");
            LoadList();

            // 设置窗口返回值让其他除非此窗口接收到是否需要刷新状态
            this.result = DialogResult.OK;

        }

        // 窗口关闭是执行
        private void FormDishTypeInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.result = DialogResult.OK;
            formDishTypeInfo = null;
        }

        // 双击表格选中行数据填充右边编辑进行修改
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 修改数据填充
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgvList.Rows[rowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();

            btnSave.Text = "修改";

         
        }

        private void FormDishTypeInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 关闭时将当前状态传递
            this.DialogResult = this.result;
        }
    }
}
