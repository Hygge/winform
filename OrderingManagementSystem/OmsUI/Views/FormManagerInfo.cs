using OmsBll.Service;
using OmsModel.Domain;
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

namespace OmsUI
{
    public partial class FormManagerInfo : Form
    {
        private ManagerInfoBll managerInfoBll = new ManagerInfoBll();

        private static FormManagerInfo FormManager;
        public static FormManagerInfo CreatedFormManagerInfo()
        {
            if (null == FormManager)
            {
                FormManager = new FormManagerInfo();
            }
            return FormManager;
        }
        public FormManagerInfo()
        {
            InitializeComponent();
            // 重写标题样式 标题居中
            SetTitleCenter();
        }
        private void SetTitleCenter()
        {
            string titleMsg = "店员管理";
            Graphics g = this.CreateGraphics();
            Double startingPoint = (this.Width / 2) - (g.MeasureString(titleMsg, this.Font).Width / 2);
            Double widthOfASpace = g.MeasureString(" ", this.Font).Width;
            String tmp = " ";
            Double tmpWidth = 0;

            while ((tmpWidth + widthOfASpace) < startingPoint)
            {
                tmp += " ";
                tmpWidth += widthOfASpace;
            }
            this.Text = tmp + titleMsg;
            //MessageBox.Show(this.Text);
        }

        private void FormManagerInfo_Load(object sender, EventArgs e)
        {
            // 加载表格数据=查询数据库
            LoadList();

        }
        /// <summary>
        /// 查询所有店员信息
        /// </summary>
        private void LoadList()
        {
  
            dataGridView1.DataSource = managerInfoBll.List();
            this.dataGridView1.Columns["MPwd"].Visible = false;


        }

        /// <summary>
        /// 添加店员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1Savema_Click(object sender, EventArgs e)
        {
            string MName = textBox2MName.Text;
            string MPwd = textBox3MPwd.Text;
            // radioButton1MType.Checked; 没选经理就是店员
            int MType = radioButton1.Checked ? 1 : 0;

            if (string.IsNullOrEmpty(MName) || MName.Trim().Length == 0)
            {
                MessageBox.Show("用户名不能为空");
                return;
            }
            if (string.IsNullOrEmpty(MPwd) || MPwd.Trim().Length == 0)
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            int result;
            ManagerInfo managerInfo = new ManagerInfo();
            managerInfo.MName = MName;
            managerInfo.MPwd = MPwd;
            managerInfo.MType = MType;
            if (!string.IsNullOrEmpty(textBox1.Text) && !textBox1.Text.Equals("自动生成无须填写"))
            {
                managerInfo.MId = Convert.ToInt32(textBox1.Text);
                result = managerInfoBll.UpdateManagerInfo(managerInfo);
            }
            else
            {
                result = managerInfoBll.AddManagerInfo(managerInfo);
            }
        
            button1_Click(null, null);

            if (result > 0)
            {
                MessageBox.Show("保存成功");
         
            }
            else
            {
                MessageBox.Show("保存失败");
            }
            LoadList();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 对表格第二列数据转换
            if (e.ColumnIndex == 3)
            {
                e.Value = Convert.ToInt32(e.Value) == 1 ? "经理" : "店员";
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 修改数据填充
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            textBox1.Text = row.Cells[0].Value.ToString();
            textBox2MName.Text = row.Cells[1].Value.ToString();
            textBox3MPwd.Text = row.Cells[2].Value.ToString();
            if (row.Cells[3].Value.ToString().Equals("1"))
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton1MType.Checked = true;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 情况编辑框里的数据
            textBox1.Text = "自动生成无须填写";
            textBox2MName.Text = null;
            textBox3MPwd.Text = null;
            radioButton1.Checked = false;
            radioButton1MType.Checked = false;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 选中当前列是，设置选中当前行
            int rowIndex = e.RowIndex;
            if (rowIndex > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                   // row.DefaultCellStyle.BackColor = Color.White;
                   // row.DefaultCellStyle.ForeColor = Color.Black;
                }
                //dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(0, 168, 255);
                //dataGridView1.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.White;
                dataGridView1.Rows[rowIndex].Selected = true;
            }
         


        }

        private void button1Delma_Click(object sender, EventArgs e)
        {
            // 删除选中行
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                ManagerInfo m = new ManagerInfo();
                m.MId = Convert.ToInt32(row.Cells[0].Value);
                managerInfoBll.DeleteManagerInfo(m);                
            }        
            MessageBox.Show("删除成功");       
            LoadList();
        }

        private void FormManagerInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 保持单例 目的是为了关闭窗口后释放窗口地址
            FormManager = null;
        }
    }
}
