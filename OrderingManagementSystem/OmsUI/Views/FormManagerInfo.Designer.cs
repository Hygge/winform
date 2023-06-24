using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System;

namespace OmsUI
{
    partial class FormManagerInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1ShAsTable = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1ShAsOp = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button1Delma = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1Savema = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton1MType = new System.Windows.Forms.RadioButton();
            this.label1MType = new System.Windows.Forms.Label();
            this.label1Tip = new System.Windows.Forms.Label();
            this.textBox3MPwd = new System.Windows.Forms.TextBox();
            this.label2Mpwd = new System.Windows.Forms.Label();
            this.textBox2MName = new System.Windows.Forms.TextBox();
            this.label1MName = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1OpMId = new System.Windows.Forms.Label();
            this.groupBox1ShAsTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1ShAsOp.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1ShAsTable
            // 
            this.groupBox1ShAsTable.Controls.Add(this.dataGridView1);
            this.groupBox1ShAsTable.Font = new System.Drawing.Font("方正舒体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1ShAsTable.Location = new System.Drawing.Point(12, 12);
            this.groupBox1ShAsTable.Name = "groupBox1ShAsTable";
            this.groupBox1ShAsTable.Size = new System.Drawing.Size(483, 388);
            this.groupBox1ShAsTable.TabIndex = 0;
            this.groupBox1ShAsTable.TabStop = false;
            this.groupBox1ShAsTable.Text = "列表";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MId,
            this.MName,
            this.MType});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(477, 365);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // MId
            // 
            this.MId.DataPropertyName = "MId";
            this.MId.HeaderText = "编号";
            this.MId.Name = "MId";
            this.MId.ReadOnly = true;
            // 
            // MName
            // 
            this.MName.DataPropertyName = "MName";
            this.MName.HeaderText = "用户名";
            this.MName.Name = "MName";
            this.MName.ReadOnly = true;
            this.MName.Width = 200;
            // 
            // MType
            // 
            this.MType.DataPropertyName = "MType";
            this.MType.HeaderText = "类型";
            this.MType.Name = "MType";
            this.MType.ReadOnly = true;
            this.MType.Width = 120;
            // 
            // groupBox1ShAsOp
            // 
            this.groupBox1ShAsOp.Controls.Add(this.button1);
            this.groupBox1ShAsOp.Controls.Add(this.button1Delma);
            this.groupBox1ShAsOp.Controls.Add(this.label1);
            this.groupBox1ShAsOp.Controls.Add(this.button1Savema);
            this.groupBox1ShAsOp.Controls.Add(this.radioButton1);
            this.groupBox1ShAsOp.Controls.Add(this.radioButton1MType);
            this.groupBox1ShAsOp.Controls.Add(this.label1MType);
            this.groupBox1ShAsOp.Controls.Add(this.label1Tip);
            this.groupBox1ShAsOp.Controls.Add(this.textBox3MPwd);
            this.groupBox1ShAsOp.Controls.Add(this.label2Mpwd);
            this.groupBox1ShAsOp.Controls.Add(this.textBox2MName);
            this.groupBox1ShAsOp.Controls.Add(this.label1MName);
            this.groupBox1ShAsOp.Controls.Add(this.textBox1);
            this.groupBox1ShAsOp.Controls.Add(this.label1OpMId);
            this.groupBox1ShAsOp.Font = new System.Drawing.Font("方正舒体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1ShAsOp.Location = new System.Drawing.Point(501, 12);
            this.groupBox1ShAsOp.Name = "groupBox1ShAsOp";
            this.groupBox1ShAsOp.Size = new System.Drawing.Size(341, 388);
            this.groupBox1ShAsOp.TabIndex = 1;
            this.groupBox1ShAsOp.TabStop = false;
            this.groupBox1ShAsOp.Text = "修改/添加信息";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(260, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 34);
            this.button1.TabIndex = 13;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1Delma
            // 
            this.button1Delma.Location = new System.Drawing.Point(76, 338);
            this.button1Delma.Name = "button1Delma";
            this.button1Delma.Size = new System.Drawing.Size(167, 34);
            this.button1Delma.TabIndex = 12;
            this.button1Delma.Text = "删除选中的店员";
            this.button1Delma.UseVisualStyleBackColor = true;
            this.button1Delma.Click += new System.EventHandler(this.button1Delma_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(22, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "提示：双击表格行数据可以对此处修改";
            // 
            // button1Savema
            // 
            this.button1Savema.Location = new System.Drawing.Point(76, 251);
            this.button1Savema.Name = "button1Savema";
            this.button1Savema.Size = new System.Drawing.Size(167, 34);
            this.button1Savema.TabIndex = 10;
            this.button1Savema.Text = "保存";
            this.button1Savema.UseVisualStyleBackColor = true;
            this.button1Savema.Click += new System.EventHandler(this.button1Savema_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(240, 201);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(60, 21);
            this.radioButton1.TabIndex = 9;
            this.radioButton1.Text = "经理";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton1MType
            // 
            this.radioButton1MType.AutoSize = true;
            this.radioButton1MType.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.radioButton1MType.Checked = true;
            this.radioButton1MType.Location = new System.Drawing.Point(119, 201);
            this.radioButton1MType.Name = "radioButton1MType";
            this.radioButton1MType.Size = new System.Drawing.Size(60, 21);
            this.radioButton1MType.TabIndex = 8;
            this.radioButton1MType.TabStop = true;
            this.radioButton1MType.Text = "店员";
            this.radioButton1MType.UseVisualStyleBackColor = false;
            // 
            // label1MType
            // 
            this.label1MType.AutoSize = true;
            this.label1MType.Location = new System.Drawing.Point(20, 201);
            this.label1MType.Name = "label1MType";
            this.label1MType.Size = new System.Drawing.Size(59, 17);
            this.label1MType.TabIndex = 7;
            this.label1MType.Text = "类型：";
            // 
            // label1Tip
            // 
            this.label1Tip.AutoSize = true;
            this.label1Tip.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1Tip.Location = new System.Drawing.Point(73, 166);
            this.label1Tip.Name = "label1Tip";
            this.label1Tip.Size = new System.Drawing.Size(246, 17);
            this.label1Tip.TabIndex = 6;
            this.label1Tip.Text = "这里只是提示，与密码位数无关";
            // 
            // textBox3MPwd
            // 
            this.textBox3MPwd.Font = new System.Drawing.Font("方正舒体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox3MPwd.Location = new System.Drawing.Point(119, 139);
            this.textBox3MPwd.Name = "textBox3MPwd";
            this.textBox3MPwd.Size = new System.Drawing.Size(200, 24);
            this.textBox3MPwd.TabIndex = 5;
            // 
            // label2Mpwd
            // 
            this.label2Mpwd.AutoSize = true;
            this.label2Mpwd.Location = new System.Drawing.Point(20, 142);
            this.label2Mpwd.Name = "label2Mpwd";
            this.label2Mpwd.Size = new System.Drawing.Size(59, 17);
            this.label2Mpwd.TabIndex = 4;
            this.label2Mpwd.Text = "密码：";
            // 
            // textBox2MName
            // 
            this.textBox2MName.Font = new System.Drawing.Font("方正舒体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2MName.Location = new System.Drawing.Point(119, 95);
            this.textBox2MName.Name = "textBox2MName";
            this.textBox2MName.Size = new System.Drawing.Size(200, 24);
            this.textBox2MName.TabIndex = 3;
            // 
            // label1MName
            // 
            this.label1MName.AutoSize = true;
            this.label1MName.Location = new System.Drawing.Point(20, 98);
            this.label1MName.Name = "label1MName";
            this.label1MName.Size = new System.Drawing.Size(76, 17);
            this.label1MName.TabIndex = 2;
            this.label1MName.Text = "用户名：";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(119, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 24);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "自动生成无须填写";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1OpMId
            // 
            this.label1OpMId.AutoSize = true;
            this.label1OpMId.Location = new System.Drawing.Point(20, 48);
            this.label1OpMId.Name = "label1OpMId";
            this.label1OpMId.Size = new System.Drawing.Size(93, 17);
            this.label1OpMId.TabIndex = 0;
            this.label1OpMId.Text = "用户编号：";
            // 
            // FormManagerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 415);
            this.Controls.Add(this.groupBox1ShAsOp);
            this.Controls.Add(this.groupBox1ShAsTable);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "FormManagerInfo";
            this.RightToLeftLayout = true;
            this.Text = "店员管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormManagerInfo_FormClosed);
            this.Load += new System.EventHandler(this.FormManagerInfo_Load);
            this.groupBox1ShAsTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1ShAsOp.ResumeLayout(false);
            this.groupBox1ShAsOp.PerformLayout();
            this.ResumeLayout(false);

        }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1ShAsTable;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1ShAsOp;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1OpMId;
        private System.Windows.Forms.TextBox textBox3MPwd;
        private System.Windows.Forms.Label label2Mpwd;
        private System.Windows.Forms.TextBox textBox2MName;
        private System.Windows.Forms.Label label1MName;
        private System.Windows.Forms.RadioButton radioButton1MType;
        private System.Windows.Forms.Label label1MType;
        private System.Windows.Forms.Label label1Tip;
        private System.Windows.Forms.Button button1Delma;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1Savema;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MId;
        private System.Windows.Forms.DataGridViewTextBoxColumn MName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MType;
        private System.Windows.Forms.Button button1;
    }
}