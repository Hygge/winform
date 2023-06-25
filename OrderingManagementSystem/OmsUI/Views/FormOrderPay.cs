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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmsUI.Views
{
    public partial class FormOrderPay : Form
    {
        private OrderInfoBll orderInfoBll = new OrderInfoBll();
        private MemberInfoBll memberInfoBll = new MemberInfoBll();
        private MemberTypeInfoBll memberTypeInfoBll = new MemberTypeInfoBll();

        public event Action RefreshHall;

        public FormOrderPay()
        {
            InitializeComponent();
        }

        private void FormOrderPay_Load(object sender, EventArgs e)
        {
            // 默认不是会员不让填写
            gbMember.Enabled = false;
            // 加载订单信息
            LoadOrderInfo();


        }

        // 加载订单金额
        private void LoadOrderInfo()
        {
            decimal money = orderInfoBll.GetTotalMoneyByOrderId(Convert.ToInt32(this.Tag));
            lblPayMoney.Text = money.ToString();
            lblPayMoneyDiscount.Text = money.ToString();

        }
      
        // 是否会员复选框根据状态触发
        private void cbkMember_CheckStateChanged(object sender, EventArgs e)
        {
            gbMember.Enabled = cbkMember.Checked;
            if (!cbkMember.Checked)
            {
                // 不是会员
                ClearMemberInfo();
            }
        }

        // 关闭窗口按钮
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 存在会员计算应收金额 
        private void txtId_TextChanged(object sender, EventArgs e)
        {
            bool b = new Regex(@"\d").IsMatch(txtId.Text);
            if (!b)
            {
                MessageBox.Show("请输入数字格式编号");
                ClearMemberInfo();
                return;
            }
            int id = Convert.ToInt32(txtId.Text);
            MemberInfo user = memberInfoBll.GetOneById(id);
            if (user != null)
            {
                // 偷个懒不去查数据库
                MemberTypeInfo memberTypeInfo = memberTypeInfoBll.List().Find( item => item.MId == user.MTypeId);
                lblMoney.Text = user.MMoney.ToString();
                lblTypeTitle.Text = user.MTypeTitle;    
                // 折扣
                lblDiscount.Text = Convert.ToString(memberTypeInfo.MDiscount *  10);
                // 应收金额  消费金额 * 折扣
                lblPayMoneyDiscount.Text = Convert.ToString(Convert.ToDecimal(lblPayMoney.Text) * Convert.ToDecimal(lblDiscount.Text) / 10);
            }
            else
            {
                ClearMemberInfo();
            }
        

        }

        // 清理会员信息
        private void ClearMemberInfo()
        {
            lblMoney.Text = "0";
            lblTypeTitle.Text = "无";
            lblDiscount.Text = Convert.ToString(0);
            lblPayMoneyDiscount.Text = Convert.ToString(lblPayMoney.Text);
        }

        // 是否使用余额
        private void cbkMoney_CheckStateChanged(object sender, EventArgs e)
        {
            // 选中使用余额扣除
            if (cbkMoney.Checked && !"0".Equals(lblMoney.Text))
            {
                // 应收金额 =  应收金额 - 余额  为负数不用付钱，正数余额不够需付钱
                decimal b =   Convert.ToDecimal(lblPayMoneyDiscount.Text) - Convert.ToDecimal(lblMoney.Text);               
                lblPayMoneyDiscount.Text = Convert.ToString(b);         
           
            }
            else
            {
                // 未选择重置应收金额
                ClearMemberInfo();
            }

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show("暂未实现，偷个懒，就一个查询");
        }

        private void btnOrderPay_Click(object sender, EventArgs e)
        {
            OrderInfo o = orderInfoBll.GetOrderInfoByOId(Convert.ToInt32(this.Tag));
            OrderPayMoneyDTO orderPayMoneyDTO = new OrderPayMoneyDTO();
              
            orderPayMoneyDTO.tid = Convert.ToInt32(o.TableId);
            orderPayMoneyDTO.money = Convert.ToDecimal(lblPayMoney.Text);
            orderPayMoneyDTO.oid = Convert.ToInt32(Convert.ToInt32(this.Tag));

            orderPayMoneyDTO.isBal = 0;
            if (string.IsNullOrEmpty(txtId.Text) || "无".Equals(Convert.ToString(lblTypeTitle.Text)))
            {
                // 不是会员                
                orderPayMoneyDTO.memberInfoId = 0;
                orderPayMoneyDTO.discount = 0;
                orderPayMoneyDTO.balance = 0;
            }
            else
            {
                orderPayMoneyDTO.memberInfoId = Convert.ToInt32(txtId.Text);
                orderPayMoneyDTO.discount = Convert.ToDecimal(lblDiscount.Text) / 10;

                // 应收折扣钱
                decimal pay = Convert.ToDecimal(lblPayMoneyDiscount.Text);
                // 使用了余额, 余额有钱足够支付则更新余额，余额不够则余额为0
                if (cbkMoney.Checked)
                {
                    orderPayMoneyDTO.isBal = 1;
                    orderPayMoneyDTO.balance = pay < 0 ? Math.Abs((decimal)pay) : 0;

                }                
            }

            int res = orderInfoBll.payOrder(orderPayMoneyDTO);

            if (res > 0)
            {
                MessageBox.Show("结账成功");
                RefreshHall();
                this.Close();
            }     
            
            

        }
    }
}
