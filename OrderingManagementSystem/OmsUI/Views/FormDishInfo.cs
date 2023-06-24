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
    public partial class FormDishInfo : Form
    {

        private DishInfoBll dishInfoBll = new DishInfoBll();
        private DishTypeInfoBll dishTypeInfoBll = new DishTypeInfoBll();

     

        public FormDishInfo()
        {
            InitializeComponent();
        }

        private static FormDishInfo formDishInfo;

        public static FormDishInfo CreatedFormDishInfo()
        {
            if (formDishInfo == null)
            {
                formDishInfo = new FormDishInfo();
            }
            return formDishInfo;
        }


        // 加载表格列表数据
        private void LoadList()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(txtTitleSearch.Text) )
            {
                dic.Add("dtitle", txtTitleSearch.Text);

            }
            if (null != ddlTypeSearch.SelectedValue && !string.IsNullOrEmpty(ddlTypeSearch.SelectedValue.ToString())
                && !"0".Equals(ddlTypeSearch.SelectedValue.ToString()))
            {
                dic.Add("dtypeId", Convert.ToInt32(ddlTypeSearch.SelectedValue.ToString()));

            }

            // 取消自动创建 dtypeId
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = dishInfoBll.GetList(dic);         
           
        }

        // 添加菜单
        private void btnAddType_Click(object sender, EventArgs e)
        {
            FormDishTypeInfo formDishTypeInfo = FormDishTypeInfo.CreatedFormDishTypeInfo();
            DialogResult result = formDishTypeInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                // 刷新下拉框菜品分类
                LoadTypeList();
            }

        }

        // 关闭菜单窗口
        private void FormDishInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            formDishInfo = null;
        }

        private void FormDishInfo_Load(object sender, EventArgs e)
        {
            LoadList();
            // 加载分类
            LoadTypeList();
        }

        
        private void LoadTypeList()
        {
            List<DishTypeInfo> list = dishTypeInfoBll.GetList();
            list.Insert(0, new DishTypeInfo()
            {
                DId = 0,
                DTitle = "请选择",
            });
            ddlTypeSearch.DataSource = list;
            ddlTypeSearch.ValueMember = "DId";
            ddlTypeSearch.DisplayMember = "DTitle";
            List<DishTypeInfo> listAdd = dishTypeInfoBll.GetList();
            listAdd.Insert(0, new DishTypeInfo()
            {
                DId = 0,
                DTitle = "请选择",
            });
            ddlTypeAdd.DataSource = listAdd;
            ddlTypeAdd.ValueMember = "DId";
            ddlTypeAdd.DisplayMember = "DTitle";
        }


        // 显示全部
        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            LoadList();
        }

        // 添加/修改
        private void btnSave_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string title = txtTitleSave.Text;
            string chars = txtChar.Text;
            string price = txtPrice.Text;
     
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("名称不能为空");
                return;
            }
            if (string.IsNullOrEmpty(price))
            {
                MessageBox.Show("价格不能为空");
                return;
            }
            if (string.IsNullOrEmpty(chars))
            {
                MessageBox.Show("拼音不能为空");
                return;
            }
            DishInfo dishInfo = new DishInfo();
            dishInfo.DChar = chars;
            dishInfo.DTitle = title;
            try
            {
                dishInfo.DPrice = Convert.ToDecimal(price);
            }
            catch (Exception ex)
            {                
                MessageBox.Show("价格格式错误请输入数值类型，0.00");
                return;
            }
         

            if (!string.IsNullOrEmpty(ddlTypeAdd.SelectedValue.ToString()))
            {
                //MessageBox.Show("名称不能为空");
                dishInfo.DTypeId = Convert.ToInt32(ddlTypeAdd.SelectedValue);
            }
            int res;
            if (!"添加时无编号".Equals(id))
            {
                dishInfo.DId = Convert.ToInt32(id);
                res = dishInfoBll.UpdateDishInfo(dishInfo);
            }
            else
            {
                res = dishInfoBll.AddDishInfo(dishInfo);
            }

            if (res > 0)
            {
                MessageBox.Show("保存成功");
                btnCancel_Click(null, null);
            }
            else
            {
                MessageBox.Show("保存失败");
            }
            LoadList();

        }

        // 取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitleSave.Text = string.Empty;
            txtChar.Text = string.Empty;
            txtPrice.Text = string.Empty;

            LoadTypeList();
        }

        // 删除
        private void btnRemove_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dgvList.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                dishInfoBll.DeleteDishInfo(Convert.ToInt32(row.Cells[0].Value));
            }
            MessageBox.Show("删除成功");
            LoadList();           
        }

  

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 修改数据填充
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgvList.Rows[rowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitleSave.Text = row.Cells[1].Value.ToString();
            ddlTypeAdd.Text = row.Cells[2].Value.ToString();
            txtPrice.Text = row.Cells[3].Value.ToString();
            txtChar.Text = row.Cells[4].Value.ToString();

            btnSave.Text = "修改";

        }
    }
}
