using OmsBll.Service;
using OmsModel.Domain;
using OmsModel.Enum;
using OmsUI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmsUI
{
    public partial class FormLogin : Form
    {
        private ManagerInfoBll managerInfoBll = new ManagerInfoBll();
        public FormLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // 获取用户名和密码进行调用登录方法
            string MName = textBox1MName.Text;
            string MPwd = textBox1MPwd.Text;

            if (string.IsNullOrEmpty(MName) || string.IsNullOrEmpty(MPwd))
            {
                MessageBox.Show("用户名和密码为空");
                return;
            }

            ManagerInfo manager = new ManagerInfo();
            manager.MName = MName;
            manager.MPwd = MPwd;

            // 标记登录账号类型 
            int MType;
            LoginState login = managerInfoBll.Login(manager, out MType);
            switch (login)
            {
                case LoginState.Ok:
                    MessageBox.Show("登录成功");
                    this.Hide();
                    FormMain form = new FormMain();
                    form.MType = MType;
                    form.ShowDialog();
                    break;
                case LoginState.PwdError:
                    MessageBox.Show("密码错误");
                    break;
                case LoginState.NameError:
                    MessageBox.Show("用户名错误");
                    break;
                default:
                    MessageBox.Show("用户名或密码错误");
                    break;
            }
     
        }

        // 退出
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
