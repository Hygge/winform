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
    public partial class FormHallInfo : Form
    {
        private HallInfoBll _HallInfoBll = new HallInfoBll();
        private DialogResult _DialogResult = DialogResult.Cancel;

        private static FormHallInfo formHallInfo;

        public static FormHallInfo CreatedFormHallInfo()
        {
            if (formHallInfo == null)
            {
                formHallInfo = new FormHallInfo();
            }
            return formHallInfo;
        }



        public FormHallInfo()
        {
            InitializeComponent();
        }

        private void FormHallInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        // 加载表格列表数据
        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = _HallInfoBll.GetList();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 添加时无编号
     
            string title = txtTitle.Text.ToString();   

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("标题输不规范");
                return;
            }
            HallInfo _HallInfo = new HallInfo();
            _HallInfo.HTitle = title;

            int result;
            // id存在更新，不存在添加
            if (txtId.Text.ToString().Equals("添加时无编号"))
            {
                result = _HallInfoBll.AddHallInfo(_HallInfo);
            }
            else
            {
                _HallInfo.HId = Convert.ToInt32(txtId.Text.ToString());
                result = _HallInfoBll.UpdateHallInfo(_HallInfo);
            }

            if (result > 0)
            {
                MessageBox.Show("保存成功");
                btnCancel_Click(null, null);

            }
            else
            {
                MessageBox.Show("保存失败");
            }
            LoadList();

            _DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = string.Empty;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            // 删除选中行
            DataGridViewSelectedRowCollection rows = dgvList.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                _HallInfoBll.DeleteHallInfo(Convert.ToInt32(row.Cells[0].Value));
            }
            MessageBox.Show("删除成功");
            btnCancel_Click(null, null);
            LoadList();

            // 设置窗口返回值让其他除非此窗口接收到是否需要刷新状态
            _DialogResult = DialogResult.OK;
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 修改数据填充
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgvList.Rows[rowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
        }

        private void FormHallInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            formHallInfo = null;
        }

        private void FormHallInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = _DialogResult;
        }
    }
}
