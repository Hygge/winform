using domain.Model;
using OmsBll.Bll;
using OmsBll.Service;
using OmsModel.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace OmsUI.Views
{
    public partial class FormMemberTypeInfo : Form
    {
        private  MemberTypeInfoBll memberTypeInfoBll = new MemberTypeInfoBll();          

        private static FormMemberTypeInfo formMemberTypeInfo;

        public static FormMemberTypeInfo CreatedFormMemberTypeInfo()
        {
            if (null == formMemberTypeInfo)
            {
                formMemberTypeInfo = new FormMemberTypeInfo();
            }
            return formMemberTypeInfo;
        }

        public FormMemberTypeInfo()
        {
            InitializeComponent();
        }

        private void FormMemberTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 添加时无编号
           // if (!Regex.IsMatch(txtDiscount.Text.ToString(), @"^0?(\\.\\d+)?$"))
           // {
           //     MessageBox.Show("折扣，请输入数字");
             //   return;
           // }
            string title = txtTitle.Text.ToString();
            Decimal discount = Convert.ToDecimal(txtDiscount.Text.ToString());
     
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("标题输不规范");
                return;
            }
            MemberTypeInfo memberTypeInfo = new MemberTypeInfo();
            memberTypeInfo.MTitle = title;          
            memberTypeInfo.MIsDelete = false;
            memberTypeInfo.MDiscount = discount;

            int result;
            // id存在更新，不存在添加
            if(txtId.Text.ToString().Equals("添加时无编号"))
            {
                result = memberTypeInfoBll.Save(memberTypeInfo);
            }
            else
            {
                memberTypeInfo.MId = Convert.ToInt32(txtId.Text.ToString());
                result = memberTypeInfoBll.UpdateMemberTypeInfo(memberTypeInfo);
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
            // 设置窗口返回值让其他除非此窗口接收到是否需要刷新状态
            this.DialogResult = DialogResult.OK;
        }

        private void LoadList()
        {
            dgvList.DataSource = memberTypeInfoBll.List();
            //隐藏字段
            this.dgvList.Columns["MIsDelete"].Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = string.Empty;
            txtDiscount.Text = 0.00.ToString();

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            // 删除选中行
            DataGridViewSelectedRowCollection rows = dgvList.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                memberTypeInfoBll.DeleteMemberTypeInfo(Convert.ToInt32(row.Cells[0].Value));
            }
            MessageBox.Show("删除成功");
            btnCancel_Click(null, null);
            LoadList();

            // 设置窗口返回值让其他除非此窗口接收到是否需要刷新状态
            this.DialogResult = DialogResult.OK;
        }

        // 双击列把当前列的行数据显示右边
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 修改数据填充
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgvList.Rows[rowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            txtDiscount.Text = row.Cells[2].Value.ToString();
        
        }

        private void FormMemberTypeInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            formMemberTypeInfo = null;
        }
    }
}
