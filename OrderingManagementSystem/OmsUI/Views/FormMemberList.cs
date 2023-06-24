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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OmsUI.Views
{
    public partial class FormMemberList : Form
    {
        private MemberInfoBll memberInfoBll =  new MemberInfoBll();
        private MemberTypeInfoBll memberTypeInfoBll =  new MemberTypeInfoBll();

        private static FormMemberList formMemberList;
        public static FormMemberList CreatedFormMemberList()
        {
            if (null == formMemberList)
            {
                formMemberList = new FormMemberList();
            }
            return formMemberList;
        }

        public FormMemberList()
        {
            InitializeComponent();
            // 加载列表数据
            LoadList();
            // 加载类型
            LoadTypeInfo();
        }

        private void LoadTypeInfo()
        {
            List<MemberTypeInfo> memberTypeInfos = memberTypeInfoBll.List();
            memberTypeInfos.Insert(0, new MemberTypeInfo()
            {
                MId = 0,
                MTitle = "请选择",
            });
            ddlType.DataSource = memberTypeInfos;
            ddlType.ValueMember = "MId";
            ddlType.DisplayMember = "MTitle";
        }

        private void LoadList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();  

            if (!string.IsNullOrEmpty(txtNameSearch.Text))
            {
                dic.Add("MName", txtNameSearch.Text);
            }

            if (!string.IsNullOrEmpty(txtPhoneSearch.Text))
            {
                dic.Add("MPhone", txtPhoneSearch.Text);
            }

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = memberInfoBll.List(dic);
        }

        // 内容改变事件
        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //取消
            txtNameAdd.Text = string.Empty;
            txtPhoneAdd.Text = string.Empty;
            txtMoney.Text = string.Empty;
         
            LoadList();

        }

        // 失去焦点事件
        private void txtPhoneSearch_Leave(object sender, EventArgs e)
        {
            LoadList();
        }

        // 添加修改会员
        private void btnSave_Click(object sender, EventArgs e)
        {
     
            // 下拉框选中的类型 会员类型
            // todo未处理逻辑需要根据会员类型id去查是否存在不存在返回错误，并刷新会员类型下拉选项
            int typeId = (int)ddlType.SelectedValue;
            //

            MemberInfo memberInfo = new MemberInfo();
            memberInfo.MName = txtNameAdd.Text;
            memberInfo.MPhone = txtPhoneAdd.Text;
            try
            {
                memberInfo.MMoney = Convert.ToDecimal(txtMoney.Text);
                memberInfo.MTypeId = typeId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("金额不对");
                return;
            }
      

            int result;
            //添加时无编号
            if (txtId.Text.Equals("添加时无编号"))
            {
               result = memberInfoBll.SaveMemberInfo(memberInfo);
            }
            else
            {
                memberInfo.MId = Convert.ToInt32(txtId.Text);
                result = memberInfoBll.UpdateMemberInfo(memberInfo);

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
        }

        // 打开类型管理
        private void btnAddType_Click(object sender, EventArgs e)
        {
           
            FormMemberTypeInfo formMemberTypeInfo = FormMemberTypeInfo.CreatedFormMemberTypeInfo();
            DialogResult result = formMemberTypeInfo.ShowDialog();
            if(result == DialogResult.OK)
            {
                // 加载类型
                LoadTypeInfo();
            }        
       
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dgvList.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                memberInfoBll.DeleteMemberInfo(Convert.ToInt32(row.Cells[0].Value));
            }
            MessageBox.Show("删除成功");
            LoadList();
        }

        private void FormMemberList_FormClosed(object sender, FormClosedEventArgs e)
        {
            formMemberList = null; 
        }
    }
}
