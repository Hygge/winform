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
    public partial class FormTableInfo : Form
    {
        private TableInfoBll tableInfoBll = new TableInfoBll();
        private HallInfoBll _HallInfoBll = new HallInfoBll();


        private static FormTableInfo formTableInfo;

        public static FormTableInfo CreatedFormTableInfo()
        {
            if (formTableInfo == null)
            {
                formTableInfo = new FormTableInfo();
            }
            return formTableInfo;
        }

        public FormTableInfo()
        {
            InitializeComponent();
        }

        private void FormTableInfo_Load(object sender, EventArgs e)
        {
            LoadList();
            LoadTypeList();
        }
        // 表格数据
        private void LoadList()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            if (ddlHallSearch.SelectedIndex > 0)
            {
                keyValuePairs.Add("tHallId", ddlHallSearch.SelectedValue.ToString());
            }
            if (ddlFreeSearch.SelectedIndex > 0)
            {
                keyValuePairs.Add("tIsFree", ddlFreeSearch.SelectedValue.ToString());
            }

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = tableInfoBll.List(keyValuePairs);
         
        }

        // 下拉框数据
        private void LoadTypeList()
        {
            // 厅包
            List<HallInfo> list = _HallInfoBll.GetList();
            list.Insert(0, new HallInfo()
            {
                HId = -1,
                HTitle = "请选择",

            });
            ddlHallSearch.DataSource = list;
            ddlHallSearch.ValueMember = "HId";
            ddlHallSearch.DisplayMember = "HTitle";
            // 厅包
            List<HallInfo> list2 = _HallInfoBll.GetList();
            list2.Insert(0, new HallInfo()
            {
                HId = -1,
                HTitle = "请选择",

            });
            ddlHallAdd.DataSource = list2;
            ddlHallAdd.ValueMember = "HId";
            ddlHallAdd.DisplayMember = "HTitle";

            // 空闲

            List<DdlModel> listDdl = new List<DdlModel>()
            {
                new DdlModel("-1","全部"),
                new DdlModel("1","空闲"),
                new DdlModel("0","使用中")
            };
            ddlFreeSearch.DataSource = listDdl;
            ddlFreeSearch.ValueMember = "id";
            ddlFreeSearch.DisplayMember = "title";

        }


        // 添加或修改
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("名称不能为空！");
                return ;
            }
            
            TableInfo tableInfo = new TableInfo();
            tableInfo.TTitle = txtTitle.Text;
            if (ddlHallSearch.SelectedIndex > 0)
            {
                MessageBox.Show("请选择包房或大厅");
                return;
            }
            tableInfo.THallId = Convert.ToInt32(ddlHallAdd.SelectedValue.ToString());

            tableInfo.TIsFree = rbUnFree.Checked ? false : true;
            int res;
            if ("添加时无编号".Equals(txtId.Text))
            {                
                res = tableInfoBll.Save(tableInfo);
            }
            else
            {
                tableInfo.TId = Convert.ToInt32(txtId.Text);
                res = tableInfoBll.UpdateTableInfo(tableInfo);
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

        // 清空
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = string.Empty;

            LoadTypeList();
        }

        // 删除
        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("是否确认删除", "提示", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {

                int res = tableInfoBll.DeleteTableInfo(Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value));
                if(res > 0)
                {
                    MessageBox.Show("删除成功");
                    LoadList();
                }
            }
            
        }

        // 查询
        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            LoadList();
        }

        // 双击修改
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 修改数据填充
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgvList.Rows[rowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            ddlHallAdd.Text = row.Cells[2].Value.ToString();
            if (Convert.ToBoolean(row.Cells[3].Value))
            {
                rbFree.Checked = true;
            }
            else
            {
                rbUnFree.Checked = true;
            }
         

        }

        // 弹出对包厅管理
        private void btnAddHall_Click(object sender, EventArgs e)
        {
            FormHallInfo _FormHallInfo = FormHallInfo.CreatedFormHallInfo();
            DialogResult result = _FormHallInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadTypeList();
            }
        }

        private void FormTableInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            formTableInfo = null;
        }

        // 列显示格式化
        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Value = Convert.ToBoolean(e.Value) ? "空闲" : "使用中";
            }
        }
    }
}
